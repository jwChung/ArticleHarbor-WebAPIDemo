namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CompositeModelCommand<TValue> : IModelCommand<TValue>
    {
        private readonly TValue value;
        private readonly Func<TValue, TValue, TValue> concat;
        private readonly IEnumerable<IModelCommand<TValue>> commands;

        public CompositeModelCommand(
            TValue value,
            Func<TValue, TValue, TValue> concat,
            params IModelCommand<TValue>[] commands)
            : this(value, concat, (IEnumerable<IModelCommand<TValue>>)commands)
        {
        }

        public CompositeModelCommand(
            TValue value,
            Func<TValue, TValue, TValue> concat,
            IEnumerable<IModelCommand<TValue>> commands)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (concat == null)
                throw new ArgumentNullException("concat");

            if (commands == null)
                throw new ArgumentNullException("commands");

            this.value = value;
            this.concat = concat;
            this.commands = commands;
        }

        public virtual TValue Value
        {
            get { return this.value; }
        }

        public Func<TValue, TValue, TValue> Concat
        {
            get { return this.concat; }
        }

        public IEnumerable<IModelCommand<TValue>> Commands
        {
            get { return this.commands; }
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<User> users)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(users)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(User user)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(user)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Article> articles)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(articles)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(Article article)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(article)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Keyword> keywords)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(keywords)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(Keyword keyword)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(keyword)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(IEnumerable<Bookmark> bookmarks)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(bookmarks)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }

        public virtual async Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark)
        {
            TValue value = this.value;
            foreach (var command in this.commands)
                value = this.concat(value, (await command.ExecuteAsync(bookmark)).Value);

            return new CompositeModelCommand<TValue>(value, this.concat, this.commands);
        }
    }
}