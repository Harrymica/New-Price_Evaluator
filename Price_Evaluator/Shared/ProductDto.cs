using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Evaluator.Shared
{
    public class ProductDto
    {
        public int Id { get; set; }
        public Root[]? root { get; set; }
    }
   
    public class Root
    {
        [Key]
        public string title { get; set; }
        public string price { get; set; }
        public string shop { get; set; }
        public string shipping { get; set; }
        public string reviews { get; set; }
        public string link { get; set; }
        public string img { get; set; }

    }

}

