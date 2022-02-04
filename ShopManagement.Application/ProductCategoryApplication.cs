using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication:IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed("مقدار وارد شده تکراری است.");

            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, 
                command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            return operation.Success("عملیات با موفقیت انجام شد");

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed("فیلدی با مشخصات وارد شده یافت نشد");

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("مقدار وارد شده تکراری است.");

            var slug = command.Slug.Slugify();

            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Save();
            return operation.Success("عملیات با موفقیت انجام شد");
        }

        public EditProductCategory Get(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
