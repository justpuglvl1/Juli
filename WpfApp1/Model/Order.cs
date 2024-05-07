using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int ServicesId { get; set; }
        public string Status { get; set; }
        public string Salary { get; set; }
        public string Number { get; set; }
        public int UserId { get; set; }
    }
}
