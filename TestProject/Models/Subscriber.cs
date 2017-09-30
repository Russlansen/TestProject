using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public bool Autos { get; set; }
        public bool Immovables { get; set; }
        public bool Pets { get; set; }
        public bool Pictures { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string E_mail { get; set; }
    }
}