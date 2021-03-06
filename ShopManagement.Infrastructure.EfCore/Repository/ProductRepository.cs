using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository:RepositoryBase<long,Product> , IProductRepository
    {
        private readonly ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                CategoryId = x.CategoryId,
                UnitPrice = x.UnitPrice,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x=>x.ProductCategory).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                UnitPrice = x.UnitPrice,
                Category = x.ProductCategory.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                CategoryId = x.CategoryId , 
                IsInStock = x.IsInStock
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if(searchModel.CategoryId != 0)
                query = query.Where(x=>x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x=>x.Id).ToList();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
