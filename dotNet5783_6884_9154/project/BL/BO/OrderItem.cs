using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString()
        {
            string orderItem = $@"
        Order id: {ID}
        Name: {ProductName}, 
    	Product ID : {ProductID}
        Price : {Price}
        Amount : {Amount}
        Total Price : {TotalPrice}";
            return orderItem;
        }
    }
}
