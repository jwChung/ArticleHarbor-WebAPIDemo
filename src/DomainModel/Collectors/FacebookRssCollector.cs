﻿namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public class FacebookRssCollector : IArticleCollector
    {
        public Task<IEnumerable<Article>> CollectAsync()
        {
            throw new NotImplementedException();
        }
    }
}