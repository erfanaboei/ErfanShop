using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long , ProductPicture> , IProductPictureRepository
    {
        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(x=>x.Product).Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture ,
                CreationDate = x.CreationDate.ToFarsi(),
                Product = x.Product.Name,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
            }).ToList();

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId).ToList();
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
