using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
using DalFacade.DO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    private BO.Order o = new BO.Order();
    PO.Cart cart = new PO.Cart();
    Window prevWindow;
    public ObservableCollection<PO.ProductItem> List_p { get; set; } = new();
    IEnumerable<BO.ProductItem> list1;
    IEnumerable<BO.OrderForList> list2;
    public ObservableCollection<PO.OrderForList> List_o { get; set; } = new();
    PO.Order order = new PO.Order();
  
    public AdminWindow(BLApi.IBL bl, PO.Cart c, Window _prevWindow)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            this.cart = c;
            this.prevWindow = _prevWindow;
            list1 = bl.product.GetCatalog();
            List_p = Common.convertList(List_p, list1);
            list2 = bl.order.GetOrderList();
            List_o = Common.convertListOrder(list2, List_o);
            Tuple<ObservableCollection<PO.ProductItem>, ObservableCollection<PO.OrderForList>> dcT =
                new Tuple<ObservableCollection<PO.ProductItem>, ObservableCollection<PO.OrderForList>>(this.List_p, this.List_o);
            this.DataContext = dcT;
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BO.BlExceptionNoMatchingItems ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void itemClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            int pId = (ProductsListview.SelectedItem as PO.ProductItem).ID;
            p = bl.product.GetProductCustomer(pId);
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), false, this.cart, this, this.List_p);
            window.Show();
            this.Hide();

        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BO.BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void addProBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), false, this.cart, this, this.List_p);
            window.Show();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void orderClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            int OId = (OrdersListview.SelectedItem as PO.OrderForList).ID;
            o = bl.order.GetOrderDetails(OId);
            Window window = new OrderWindow(bl, Common.ConvertToPoOrder(o, order), false, this.cart, this, this.List_o);
            window.Show();
            this.Hide();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlExceptionCantUpdateDelivery ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BO.BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    private void go_Back(object sender, RoutedEventArgs e)
    {
        this.prevWindow.Show();
        this.Close();
    }
}

