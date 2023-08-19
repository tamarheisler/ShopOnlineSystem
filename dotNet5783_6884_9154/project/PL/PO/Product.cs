using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class Product : DependencyObject
    {
        public int ID {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("ID", typeof(int), typeof(Product), new UIPropertyMetadata(default(int)));
        public string Name {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Product), new UIPropertyMetadata(""));
        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(double), typeof(Product), new UIPropertyMetadata(default(double)));
        public Category Category
        {
            get { return (Category)GetValue(CategryProperty); }
            set { SetValue(CategryProperty, value); }
        }
        public static readonly DependencyProperty CategryProperty = DependencyProperty.Register("Category", typeof(Category), typeof(Product), new UIPropertyMetadata(Category.SmartWatches));
        public int inStock
        {
            get { return (int)GetValue(inStockProeprty); }
            set { SetValue(inStockProeprty, value); }
        }
        public static readonly DependencyProperty inStockProeprty = DependencyProperty.Register("inStock", typeof(int), typeof(Product), new UIPropertyMetadata(0));
    }
}
