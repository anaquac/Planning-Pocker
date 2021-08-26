using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Planning_Poker.Models
{
    public class User : BaseEntity
    {

        [Required(ErrorMessage = "Value is required")]
        public string Name { get; set; }

    }
}
