using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_ErfanShopQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slide = _slideQuery.GetSlides();
            return View(slide);
        }
    }
}
