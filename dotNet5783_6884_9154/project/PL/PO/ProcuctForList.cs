using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BO;
using System.Windows;
using DalFacade.DO;

namespace PL.PO


{
    public class ProductForList : DependencyObject
   
    {

        public int ID
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("ID", typeof(int), typeof(ProductForList), new UIPropertyMetadata(0));
        
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ProductForList), new UIPropertyMetadata(""));

        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(double), typeof(ProductForList), new UIPropertyMetadata(0.00));

        public eCategory Category
        {
            get { return (eCategory)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(eCategory), typeof(ProductForList), new UIPropertyMetadata(eCategory.SmartWatches));

    }
}