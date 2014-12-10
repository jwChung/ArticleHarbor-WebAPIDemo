﻿namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompositeModelCommand<TReturn> : IModelCommand<TReturn>
    {
        private readonly IEnumerable<IModelCommand<TReturn>> commands;

        public CompositeModelCommand(params IModelCommand<TReturn>[] commands)
            : this((IEnumerable<IModelCommand<TReturn>>)commands)
        {
        }

        public CompositeModelCommand(IEnumerable<IModelCommand<TReturn>> commands)
        {
            if (commands == null)
                throw new ArgumentNullException("commands");

            this.commands = commands;
        }
        
        public IEnumerable<IModelCommand<TReturn>> Commands
        {
            get { return this.commands; }
        }

        public async virtual Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            var values = Enumerable.Empty<TReturn>();
            foreach (var command in this.commands)
                values = values.Concat(await command.ExecuteAsync(user));

            return values;
        }
        
        public async virtual Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            var values = Enumerable.Empty<TReturn>();
            foreach (var command in this.commands)
                values = values.Concat(await command.ExecuteAsync(article));

            return values;
        }

        public async virtual Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            var values = Enumerable.Empty<TReturn>();
            foreach (var command in this.commands)
                values = values.Concat(await command.ExecuteAsync(keyword));

            return values;
        }

        public async virtual Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            var values = Enumerable.Empty<TReturn>();
            foreach (var command in this.commands)
                values = values.Concat(await command.ExecuteAsync(bookmark));

            return values;
        }
    }
}