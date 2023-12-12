﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.CategoryResponses
{
    public class CreatedCategoryResponse
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
