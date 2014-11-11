namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using ArticleHarbor.DomainModel;
    using DomainModel.Repositories;

    public class LazyUnitOfWork
    {
        private readonly Func<IUnitOfWork> unitOfWorkFactory;
        private IUnitOfWork unitOfWork;

        public LazyUnitOfWork(Func<IUnitOfWork> unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory");

            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public Func<IUnitOfWork> UnitOfWorkFactory
        {
            get { return this.unitOfWorkFactory; }
        }

        public IUnitOfWork Value
        {
            get
            {
                if (this.unitOfWork == null)
                    this.unitOfWork = this.unitOfWorkFactory();

                return this.unitOfWork;
            }
        }

        public IUnitOfWork Optional
        {
            get { return this.unitOfWork; }
        }
    }
}