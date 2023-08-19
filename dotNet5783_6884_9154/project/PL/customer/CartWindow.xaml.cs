using BlImplementation;
using BO;
using DalFacade.DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{

    BLApi.IBL bl;
    BO.Cart c;
    Window prevWindow;
    public PO.Cart cart { get; set; }

    private PO.Cart p { get; set; }
    public CartWindow(BLApi.IBL _bl, PO.Cart _cart, Window _prevWindow)
    {
        InitializeComponent();
        this.bl = _bl;
        this.cart = _cart;
        this.prevWindow = _prevWindow;
        this.DataContext = this;
    }
    public void BackToList(object sender, RoutedEventArgs e)
    {
        this.prevWindow.Show();
        this.Hide();
    }
    private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            this.c = Common.ConvertToBoCart(this.cart);
            bl.cart.Update(this.c, ((PO.OrderItem)(sender as Button).DataContext).ProductID, ((PO.OrderItem)(sender as Button).DataContext).Amount -1);
            this.cart = Common.ConvertToPoCart(this.c, this.cart);
          }
        catch (BlOutOfStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            this.c = Common.ConvertToBoCart(this.cart);
            bl.cart.Update(this.c, ((PO.OrderItem)(sender as Button).DataContext).ProductID, ((PO.OrderItem)(sender as Button).DataContext).Amount + 1);
            this.cart = Common.ConvertToPoCart(this.c, this.cart);
            
        }
        catch (BlOutOfStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }



    private void cartConfirmation(object sender, RoutedEventArgs e)
    {
        if (this.cart.Items.Count() == 0)
        {
            MessageBox.Show("you cannot confirm an empty cart.");
            return;
        }
        new customer.ConfirmCart(bl, this.cart, this).Show();
        this.Hide();
    }

}
