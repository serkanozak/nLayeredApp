﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests
{
    public class PageRequest
    {
        public int Index { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
