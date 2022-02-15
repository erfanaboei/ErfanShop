using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetDefine()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Define", command);
        }
        public JsonResult OnPostDefine(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var discount = _customerDiscountApplication.GetDetails(id);
            discount.Products = _productApplication.GetProducts();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
