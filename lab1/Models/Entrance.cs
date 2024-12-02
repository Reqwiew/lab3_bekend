using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Entrance
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int BarchSize { get; set; }
        public DateTime BarchDate { get; set; }
        public decimal BarchPrice { get; set; }




    }
}
