using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProject.ViewModels
{
    public class IndexHomeViewModel
    {
        public bool Autos { get; set; }
        public bool Immovables { get; set; }
        public bool Pets { get; set; }
        public bool Pictures { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string E_mail { get; set; }
    }
}