namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class InsertCommand : ModelCommand<Task<IModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEnumerable<Task<IModel>> result;

        public InsertCommand(IUnitOfWork unitOfWork)
            : this(unitOfWork, Enumerable.Empty<Task<IModel>>())
        {
        }

        public InsertCommand(IUnitOfWork unitOfWork, IEnumerable<Task<IModel>> result)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            if (result == null)
                throw new ArgumentNullException("result");

            this.unitOfWork = unitOfWork;
            this.result = result;
        }

        public override IEnumerable<Task<IModel>> Result
        {
            get { return this.result; }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public override IModelCommand<Task<IModel>> Execute(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var task = Task.Run<IModel>(async () =>
            {
                return await this.unitOfWork.Users.InsertAsync(user);
            });

            return new InsertCommand(
                this.unitOfWork, this.result.Concat(new Task<IModel>[] { task }));
        }

        public override IModelCommand<Task<IModel>> Execute(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var task = Task.Run<IModel>(async () =>
            {
                return await this.unitOfWork.Articles.InsertAsync(article);
            });
            
            return new InsertCommand(
                this.unitOfWork, this.result.Concat(new Task<IModel>[] { task }));
        }

        public override IModelCommand<Task<IModel>> Execute(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            var task = Task.Run<IModel>(async () =>
            {
                return await this.unitOfWork.Keywords.InsertAsync(keyword);
            });

            return new InsertCommand(
                this.unitOfWork, this.result.Concat(new Task<IModel>[] { task }));
        }
    }
}