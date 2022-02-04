using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework;
using _01_Framework.Application;
using _01_Framework.Domain;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        EditProductCategory Get(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
