using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Person
    {
        [Key]
        public int PersonsId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
