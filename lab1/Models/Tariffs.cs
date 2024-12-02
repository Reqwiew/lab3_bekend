using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Tariffs
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal CostTariff { get; set; }

    }
}
