using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Evaluator.Shared
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName cannot be empty")]
        public string? FirstName { get; set;}
        [Required(ErrorMessage = "LasstName cannot be empty")]
        public string? LastName { get; set;}
        [Required(ErrorMessage = "Email cannot be empty")]
        public string? Email { get; set;}    
        public string? Password { get; set;}
        [Required(ErrorMessage = "Name cannot be empty")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(MAX)")]
        public byte[]? StoredSalt { get; set; }

        public string? Role { get; set; }

        public List<Cart>? Carts { get; set; }
        public int CartsId { get; set; }


    }
}
