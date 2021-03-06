﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class ArticleService : IArticleService
    {
        private IUserStore userStore;
        private IArticleStore articleStore;
        public ArticleService(IUserStore userStore, IArticleStore articleStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        public void Register(Article article)
        {
            if (article.UserName != null)
            {
                if (!userStore.Users.Exists(x => article.UserName == x.Name))
                {
                    userStore.Users.Add(new User(article.UserName));
                }

                articleStore.Articles.Add(article);
            }
        }

        public Article FoundArticleById(Guid id)
        {
            var foundArticle = articleStore.Articles.FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }

        public List<Article> GetArticleList()
        {
            return articleStore.Articles.ToList();
        }
    }
}
