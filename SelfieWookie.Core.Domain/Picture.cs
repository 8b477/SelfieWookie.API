﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieWookie.Core.Domain
{
    public class Picture
    {
        public int Id { get; set; }

        public string? Url { get; set; }

        public List<Selfie>? Selfies { get; set; }
    }
}
