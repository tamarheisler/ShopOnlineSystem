﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class OrderItem : DependencyObject
    {
        public int ID
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("ID", typeof(int), typeof(OrderItem), new UIPropertyMetadata(default(int)));

        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }
        public static readonly DependencyProperty ProductNameProperty = DependencyProperty.Register("ProductName", typeof(string), typeof(OrderItem), new UIPropertyMetadata(""));
        public int ProductID
        {
            get { return (int)GetValue(ProductIDProperty); }
            set { SetValue(ProductIDProperty, value); }
        }
        public static readonly DependencyProperty ProductIDProperty = DependencyProperty.Register("ProductID", typeof(int), typeof(OrderItem), new UIPropertyMetadata(default(int)));
        public int Amount
        {
            get { return (int)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(int), typeof(OrderItem), new UIPropertyMetadata(default(int)));

        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(double), typeof(OrderItem), new UIPropertyMetadata(default(double)));

        public double TotalPrice
        {
            get { return (double)GetValue(TotalPriceProperty); }
            set { SetValue(TotalPriceProperty, value); }
        }
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderItem), new UIPropertyMetadata(default(double)));
    }
}
