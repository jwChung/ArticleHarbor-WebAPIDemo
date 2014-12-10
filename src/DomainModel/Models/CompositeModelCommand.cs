namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CompositeModelCommand<TReturn> : IModelCommand<TReturn>
    {
        private readonly TReturn value;
        private readonly Func<TReturn, TReturn, TReturn> concat;
        private readonly IEnumerable<IModelCommand<TReturn>> commands;

        public CompositeModelCommand(
            TReturn value,
            Func<TReturn, TReturn, TReturn> concat,
            params IModelCommand<TReturn>[] commands)
            : this(value, concat, (IEnumerable<IModelCommand<TReturn>>)commands)
        {
        }

        public CompositeModelCommand(
            TReturn value,
            Func<TReturn, TReturn, TReturn> concat,
            IEnumerable<IModelCommand<TReturn>> commands)
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

        public Func<TReturn, TReturn, TReturn> Concat
        {
            get { return this.concat; }
        }

        public IEnumerable<IModelCommand<TReturn>> Commands
        {
            get { return this.commands; }
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            ////TReturn value = this.value;
            ////foreach (var command in this.commands)
            ////    value = this.concat(value, (await command.ExecuteAsync(user)).Value);

            ////return new CompositeModelCommand<TReturn>(value, this.concat, this.commands);
            return null;
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            ////TReturn value = this.value;
            ////foreach (var command in this.commands)
            ////    value = this.concat(value, (await command.ExecuteAsync(article)).Value);

            ////return new CompositeModelCommand<TReturn>(value, this.concat, this.commands);
            return null;
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            ////TReturn value = this.value;
            ////foreach (var command in this.commands)
            ////    value = this.concat(value, (await command.ExecuteAsync(keyword)).Value);

            ////return new CompositeModelCommand<TReturn>(value, this.concat, this.commands);
            return null;
        }

        public virtual Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            ////TReturn value = this.value;
            ////foreach (var command in this.commands)
            ////    value = this.concat(value, (await command.ExecuteAsync(bookmark)).Value);

            ////return new CompositeModelCommand<TReturn>(value, this.concat, this.commands);
            return null;
        }
    }
}