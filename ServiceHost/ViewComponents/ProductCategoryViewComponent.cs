﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_ErfanShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategory = _productCategoryQuery.GetProductCategories();
            return View(productCategory);
        }
    }
}
