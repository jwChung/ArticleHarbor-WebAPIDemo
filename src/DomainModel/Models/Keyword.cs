﻿namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Threading.Tasks;

    public class Keyword : IModel
    {
        private readonly string word;
        private readonly int articleId;

        public Keyword(int articleId, string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");

            this.word = word;
            this.articleId = articleId;
        }

        public int ArticleId
        {
            get { return this.articleId; }
        }

        public string Word
        {
            get { return this.word; }
        }

        public IKeys GetKeys()
        {
            return new Keys<int, string>(this.articleId, this.word);
        }

        public IModelCommand<TResult> Execute<TResult>(IModelCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.Execute(this);
        }

        public Task<IModelCommand<TValue>> ExecuteAsync<TValue>(IModelCommand<TValue> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.ExecuteAsync(this);
        }
    }
}