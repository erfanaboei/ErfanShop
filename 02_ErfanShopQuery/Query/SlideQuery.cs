using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_ErfanShopQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EfCore;

namespace _02_ErfanShopQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Where(x => x.IsRemoved == false).Select(x => new SlideQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Title = x.Title,
                Link =x.Link,
                BtnText = x.BtnText,
                Text = x.Text
            }).ToList();
        }
    }
}
