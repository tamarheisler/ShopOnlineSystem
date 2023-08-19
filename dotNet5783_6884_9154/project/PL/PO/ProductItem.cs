﻿using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PL.PO;
/// <summary>
/// A PO product item entity for the catalog screen of the customer
/// </summary>
public class ProductItem : DependencyObject
{

    public int ID//product id
    {
        get { return (int)GetValue(IDProperty); }
        set { SetValue(IDProperty, value); }
    }
    public string? Name
    {
        get { return (string)GetValue(nameProperty); }
        set { SetValue(nameProperty, value); }
    }



    public double Price//productPrice
    {
        get { return (double)GetValue(priceProperty); }
        set { SetValue(priceProperty, value); }
    }
    public BO.Category Category
    {
        get { return (BO.Category)GetValue(categoryProperty); }
        set { SetValue(categoryProperty, value); }
    }

    public int Amount//The quantity of this product in the customer's BOcart
    {
        get { return (int)GetValue(amountProperty); }
        set { SetValue(amountProperty, value); }
    }

    public bool InStock//Availability (whether in stock)
    {
        get { return (bool)GetValue(inStockProperty); }
        set { SetValue(inStockProperty, value); }
    }

    public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(ProductItem), new UIPropertyMetadata(0));
    public static readonly DependencyProperty nameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ProductItem), new UIPropertyMetadata(""));
    public static readonly DependencyProperty priceProperty = DependencyProperty.Register("Price", typeof(double), typeof(ProductItem), new UIPropertyMetadata(0.0));
    public static readonly DependencyProperty categoryProperty = DependencyProperty.Register("Category", typeof(BO.Category), typeof(ProductItem), new UIPropertyMetadata(BO.Category.All));
    public static readonly DependencyProperty amountProperty = DependencyProperty.Register("Amount", typeof(int), typeof(ProductItem), new UIPropertyMetadata(0));
    public static readonly DependencyProperty inStockProperty = DependencyProperty.Register("InStock", typeof(bool), typeof(ProductItem), new UIPropertyMetadata(false));

}









