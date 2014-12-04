namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CompositeModelCommand<TValue> : IModelCommand<TValue>
    {
        private readonly TValue value;
        private readonly Func<TValue, TValue, TValue> composer;
        private readonly IEnumerable<IModelCommand<TValue>> commands;

        public CompositeModelCommand(
            TValue value,
            Func<TValue, TValue, TValue> composer,
            params IModelCommand<TValue>[] commands)
            : this(value, composer, (IEnumerable<IModelCommand<TValue>>)commands)
        {
        }

        public CompositeModelCommand(
            TValue value,
            Func<TValue, TValue, TValue> composer,
            IEnumerable<IModelCommand<TValue>> commands)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (composer == null)
                throw new ArgumentNullException("composer");

            if (commands == null)
                throw new ArgumentNullException("commands");

            this.value = value;
            this.composer = composer;
            this.commands = commands;
        }

        public TValue Value
        {
            get { return this.value; }
        }

        public Func<TValue, TValue, TValue> Composer
        {
            get { return this.composer; }
        }

        public IEnumerable<IModelCommand<TValue>> Commands
        {
            get { return this.commands; }
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Article> articles)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(Keyword keyword)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            throw new NotImplementedException();
        }

        public Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }
    }
}