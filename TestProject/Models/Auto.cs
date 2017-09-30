using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Models
{
    [Table("Autos")]
    public class Auto : Goods
    {
        public string Model { get; set; }
    }
}