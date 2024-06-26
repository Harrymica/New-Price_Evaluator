﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Evaluator.Shared
{
    public class Cart
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public string? Price { get; set; }
        public string? Shop { get; set; }
        public UserModel? User { get; set; }
        public int UserId { get; set; }
        public int Quantity{ get; set;} 
    }
}
