﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory:EntityBase<long>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        protected ProductCategory()
        {

        }

        public ProductCategory(string name, string description, string picture, string pictureTitle, string pictureAlt,
            string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string description, string picture, string pictureTitle, string pictureAlt, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
