using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository:RepositoryBase<long , Slide> , ISlideRepository
    {
        private readonly ShopContext _context;
        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                Link = x.Link,
                BtnText = x.BtnText
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                IsRemoved = x.IsRemoved,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
