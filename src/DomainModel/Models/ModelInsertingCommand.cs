namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;

    public class ModelInsertingCommand : ModelCommand<Task<IModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEnumerable<Task<IModel>> result;

        public ModelInsertingCommand(IUnitOfWork unitOfWork)
            : this(unitOfWork, Enumerable.Empty<Task<IModel>>())
        {
        }

        public ModelInsertingCommand(IUnitOfWork unitOfWork, IEnumerable<Task<IModel>> result)
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

            return new ModelInsertingCommand(
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
            
            return new ModelInsertingCommand(
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

            return new ModelInsertingCommand(
                this.unitOfWork, this.result.Concat(new Task<IModel>[] { task }));
        }

        public override IModelCommand<Task<IModel>> Execute(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            var task = Task.Run<IModel>(async () =>
            {
                return await this.unitOfWork.Bookmarks.InsertAsync(bookmark);
            });

            return new ModelInsertingCommand(
                this.unitOfWork, this.result.Concat(new Task<IModel>[] { task }));
        }
    }
}