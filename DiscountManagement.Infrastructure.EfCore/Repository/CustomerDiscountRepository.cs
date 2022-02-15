using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long , CustomerDiscount> , ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext context , ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                ProductId = x.ProductId,
                Reason = x.Reason,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new {x.Id, x.Name}).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                ProductId = x.ProductId,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Reason))
                query = query.Where(x => x.Reason == searchModel.Reason).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime()).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
                query = query.Where(x => x.EndDateGr > searchModel.EndDate.ToGeorgianDateTime()).ToList();
            

            var customerDiscount = query.OrderByDescending(x => x.Id).ToList();

            customerDiscount.ForEach(discount=>discount.Product = products.FirstOrDefault(z=>z.Id == discount.ProductId)?.Name);

            return customerDiscount;
        }
    }
}
