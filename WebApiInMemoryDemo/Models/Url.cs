﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiInMemoryDemo.Models
{
    public class Url
    {
        public int UrlId { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}