﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ErfanShopQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}
