using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class OrderForList : DependencyObject
    {
        public int ID
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("ID", typeof(int), typeof(OrderForList), new UIPropertyMetadata(0));
        public string CustomerName
        {
            get { return (string)GetValue(CustomerNameProperty); }
            set { SetValue(CustomerNameProperty, value); }
        }
        public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(OrderForList), new UIPropertyMetadata(""));
        
         public OrderStatus? Status
        {
            get { return (OrderStatus?)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(OrderStatus?), typeof(OrderForList), new UIPropertyMetadata(OrderStatus.Payed));

    
        public int AmountOfItems
        {
            get { return (int)GetValue(AmountOfItemsProperty); }
            set { SetValue(AmountOfItemsProperty, value); }
        }
        public static readonly DependencyProperty AmountOfItemsProperty = DependencyProperty.Register("AmountOfItems", typeof(int), typeof(OrderForList), new UIPropertyMetadata(default(int)));

        public double TotalPrice
        {
            get { return (double)GetValue(TotalPriceProperty); }
            set { SetValue(TotalPriceProperty, value); }
        }
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(Product), new UIPropertyMetadata(default(double)));


    }
}
