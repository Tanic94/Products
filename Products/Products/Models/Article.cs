using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Article
    {
        public string ArticleId;
        public string ArticleName;
        public string ArticleDescription;
        public string ArticleCategory;
        public string ArticleManufacturer;
        public string ArticleSupplier;
        public string ArticlePrice;
    }
    public class Articles
    {
        public IList<Article> articles;
    }
}