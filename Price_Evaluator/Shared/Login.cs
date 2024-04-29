using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Evaluator.Shared
{
    public class Login
    {
        [Required (ErrorMessage ="Email cannot be null or does not exist")]
        public string? Email {  get; set; }
        [Required(ErrorMessage = "Password cannot be null or is not correct ")]
        public string? Password { get; set; }
    }
}
