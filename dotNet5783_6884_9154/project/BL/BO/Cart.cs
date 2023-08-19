using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public List<OrderItem> items { get; set; } = new();
        public double TotalPrice { get; set; }

        public override string ToString()
        {
            string cart = $@"
        Customer Name: {CustomerName}, 
        Customer Adress : {CustomerAddress}
        Customer E-mail : {CustomerEmail}
        total price: {TotalPrice}
        Items: {items}";
            items.ForEach(i => cart += "\n \t " + i);
            return cart;
        }

    }
}
