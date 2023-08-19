using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PL.PO

{
   public class Order : DependencyObject
    {
        public int ID
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("ID", typeof(int), typeof(Order), new UIPropertyMetadata(0));
        public string CustomerName
        {
            get { return (string)GetValue(CustomerNameProperty); }
            set { SetValue(CustomerNameProperty, value); }
        }
        public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(Order), new UIPropertyMetadata(""));
        public string CustomerEmail
        {
            get { return (string)GetValue(CustomerEmailProperty); }
            set { SetValue(CustomerEmailProperty, value); }
        }
        public static readonly DependencyProperty CustomerEmailProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(Order), new UIPropertyMetadata(""));
        public string CustomerAddress
        {
            get { return (string)GetValue(CustomerAddressProperty); }
            set { SetValue(CustomerAddressProperty, value); }
        }
        public static readonly DependencyProperty CustomerAddressProperty = DependencyProperty.Register("CustomerAddress", typeof(string), typeof(Order), new UIPropertyMetadata(""));
        public DateTime? OrderDate
        {
            get { return (DateTime?)GetValue(OrderDateProperty); }
            set { SetValue(OrderDateProperty, value); }
        }
        public static readonly DependencyProperty OrderDateProperty = DependencyProperty.Register("OrderDate", typeof(DateTime?), typeof(Order), new UIPropertyMetadata(null));
        public OrderStatus? Status
        {
            get { return (OrderStatus?)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(OrderStatus?), typeof(Order), new UIPropertyMetadata(OrderStatus
            .Payed));

        public DateTime? ShipDate
        {
            get { return (DateTime?)GetValue(ShipDateProperty); }
            set { SetValue(ShipDateProperty, value); }
        }
        public static readonly DependencyProperty ShipDateProperty = DependencyProperty.Register("ShipDate", typeof(DateTime?), typeof(Order), new UIPropertyMetadata(null));
        public DateTime? DeiveryDate
        {
            get { return (DateTime?)GetValue(DeiveryDateProperty); }
            set { SetValue(DeiveryDateProperty, value); }
        }
        public static readonly DependencyProperty DeiveryDateProperty = DependencyProperty.Register("DeiveryDate", typeof(DateTime?), typeof(Order), new UIPropertyMetadata(null));

        public List<OrderItem> Items
        {
            get { return (List<OrderItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<OrderItem>), typeof(Order), new UIPropertyMetadata(new List<OrderItem>()));

        public double TotalPrice
        {
            get { return (double)GetValue(TotalPriceProperty); }
            set { SetValue(TotalPriceProperty, value); }
        }
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(Order), new UIPropertyMetadata(default(double)));


    }
}
