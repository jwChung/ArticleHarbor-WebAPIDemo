namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Repositories;

    public class InsertCommand : ModelCommand<Task<IModel>>
    {
        private readonly IUnitOfWork unitOfWork;

        public InsertCommand(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<Task<IModel>> Result
        {
            get { yield break; }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }
    }
}