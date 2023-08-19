using BLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeiveryDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public double TotalPrice { get; set; }

        public Order ChooseOrder { get; set; }

        public override string ToString()
        {
            string order = $@"
        Order id: {ID}
        Customer Name: {CustomerName}
        Customer Adress : {CustomerAddress}
        Customer E-mail : {CustomerEmail}
        Order Date : {OrderDate}
    	Status : {Status}
        Ship Date : {ShipDate}
        Deivery Date : {DeiveryDate}
        total price: {TotalPrice}
        Items :";
            Items.ForEach(i => order += "\n \t " + i);
            return order;
        }

    }
}
