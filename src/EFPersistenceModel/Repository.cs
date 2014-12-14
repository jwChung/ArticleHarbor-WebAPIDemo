namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Queries;
    using DomainModel.Repositories;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This is suppressed by the design.")]
    public abstract class Repository<TKeys, TModel, TPersistence>
        : IRepository<TKeys, TModel>
        where TKeys : IKeys
        where TModel : IModel
        where TPersistence : class
    {
        private readonly DbContext context;
        private readonly DbSet<TPersistence> dbSet;

        protected Repository(DbContext context, DbSet<TPersistence> dbSet)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (dbSet == null)
                throw new ArgumentNullException("dbSet");

            this.context = context;
            this.dbSet = dbSet;
        }

        public DbContext Context
        {
            get { return this.context; }
        }

        public DbSet<TPersistence> DbSet
        {
            get { return this.dbSet; }
        }

        public virtual Task<TModel> FindAsync(TKeys keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            return this.FindAsyncWith(keys);
        }

        public virtual async Task<IEnumerable<TModel>> SelectAsync()
        {
            var persistences = await this.dbSet.AsNoTracking().ToArrayAsync();
            return await this.ConvertToModels(persistences);
        }

        public virtual Task<TModel> InsertAsync(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            return this.InsertAsyncWith(model);
        }

        public virtual Task UpdateAsync(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            return this.UpdateAsyncWith(model);
        }

        public virtual Task DeleteAsync(TKeys keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            return this.DeleteAsyncWith(keys);
        }

        public virtual Task<IEnumerable<TModel>> SelectAsync(ISqlQuery sqlQuery)
        {
            if (sqlQuery == null)
                throw new ArgumentNullException("sqlQuery");

            return this.ExecuteSelectCommandAsyncWith(sqlQuery);
        }

        public virtual Task<IEnumerable<TModel>> SelectAsync(IPredicate predicate)
        {
            return this.SelectAsync(
                  new SqlQuery(Top.None, OrderByColumns.None, predicate));
        }

        public virtual Task DeleteAsync(IPredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return this.ExecuteDeleteCommandAsyncWith(predicate);
        }

        public abstract Task<TModel> ConvertToModelAsync(TPersistence persistence);

        public abstract Task<TPersistence> ConvertToPersistenceAsync(TModel model);

        private async Task<TModel> FindAsyncWith(TKeys keys)
        {
            var persistence = await this.dbSet.FindAsync(keys.ToArray());
            return await this.ConvertToModelAsync(persistence);
        }

        private async Task<TModel> InsertAsyncWith(TModel model)
        {
            var persistence = this.dbSet.Add(await this.ConvertToPersistenceAsync(model));
            await this.context.SaveChangesAsync();
            return await this.ConvertToModelAsync(persistence);
        }

        private async Task UpdateAsyncWith(TModel model)
        {
            await this.DetachFromObjectContext(model);
            this.context.Entry(await this.ConvertToPersistenceAsync(model)).State = EntityState.Modified;
        }

        private async Task DetachFromObjectContext(TModel model)
        {
            var entity = await this.GetEntity(model.GetKeys().ToArray());
            ((IObjectContextAdapter)this.context).ObjectContext.Detach(entity);
        }

        private async Task DeleteAsyncWith(TKeys keys)
        {
            var entity = await this.GetEntity(keys.ToArray());
            this.dbSet.Remove(entity);
        }

        private async Task<IEnumerable<TModel>> ExecuteSelectCommandAsyncWith(ISqlQuery sqlQuery)
        {
            string sql = new SqlQueryBuilder(this.dbSet).BuildSelect(sqlQuery);

            var sqlParameters = sqlQuery.Predicate.Parameters
               .Select(x => new SqlParameter(x.Name, x.Value))
               .ToArray();

            var persistences = await this.dbSet.SqlQuery(sql, sqlParameters)
                .AsNoTracking().ToArrayAsync();
            return await this.ConvertToModels(persistences);
        }

        private async Task ExecuteDeleteCommandAsyncWith(IPredicate predicate)
        {
            string sql = new SqlQueryBuilder(this.dbSet).BuildDelete(predicate);

            var sqlParameters = predicate.Parameters
                .Select(x => new SqlParameter(x.Name, x.Value))
                .ToArray();

            await this.context.Database.ExecuteSqlCommandAsync(sql, sqlParameters);
        }

        private async Task<TPersistence> GetEntity(object[] keys)
        {
            var entity = await this.dbSet.FindAsync(keys);
            if (entity == null)
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "There is no '{0}' entity matched with identity '{1}'.",
                        typeof(TPersistence).Name,
                        string.Join(", ", keys)),
                    "model");

            return entity;
        }

        private async Task<IEnumerable<TModel>> ConvertToModels(TPersistence[] persistences)
        {
            var models = new TModel[persistences.Length];
            for (int i = 0; i < models.Length; i++)
                models[i] = await this.ConvertToModelAsync(persistences[i]);

            return models;
        }

        private class SqlQueryBuilder
        {
            private readonly DbSet<TPersistence> dbSet;
            
            public SqlQueryBuilder(DbSet<TPersistence> dbSet)
            {
                this.dbSet = dbSet;
            }

            public string BuildSelect(ISqlQuery sqlQuery)
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    "SELECT {0} * FROM {1} {2} {3};",
                    BuildTopClause(sqlQuery.Top),
                    this.GetTableName(),
                    BuildWhereClause(sqlQuery.Predicate),
                    BuildOrderByClause(sqlQuery.OrderByColumns));
            }

            public string BuildDelete(IPredicate predicate)
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    "DELETE FROM {0} {1};",
                    this.GetTableName(),
                    BuildWhereClause(predicate));
            }

            private static string BuildTopClause(ITop top)
            {
                return top.Equals(Top.None) ? string.Empty : "TOP " + top.Count;
            }

            private static string GetOrderDirectionName(OrderDirection direction)
            {
                switch (direction)
                {
                    case OrderDirection.Ascending:
                        return "ASC";
                    case OrderDirection.Descending:
                        return "DESC";
                    default:
                        throw new ArgumentException("Out of range.", "direction");
                }
            }

            private static string BuildWhereClause(IPredicate predicate)
            {
                return predicate.Equals(Predicate.None)
                    ? string.Empty
                    : string.Format(
                        CultureInfo.CurrentCulture,
                        "WHERE {0}",
                        predicate.SqlText);
            }

            private static string BuildOrderByClause(IOrderByColumns columns)
            {
                if (columns.Equals(OrderByColumns.None))
                    return string.Empty;

                return "ORDER BY " + string.Join(
                    ", ",
                    columns.Select(c => string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} {1}",
                        c.Name,
                        GetOrderDirectionName(c.OrderDirection))));
            }

            private string GetTableName()
            {
                string selectSql = this.dbSet.ToString();
                var start = selectSql.IndexOf("FROM", StringComparison.CurrentCulture) + 5;
                var end = selectSql.LastIndexOf("AS", StringComparison.CurrentCulture);
                return selectSql.Substring(start, end - start - 1);
            }
        }
    }
}