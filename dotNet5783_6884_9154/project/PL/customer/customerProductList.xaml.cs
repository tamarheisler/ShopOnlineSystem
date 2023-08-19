using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
using BLApi;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DalFacade.DO;
using System.ComponentModel;
using System.Linq;

/// <summary>
/// Interaction logic for BOListWindow.xaml
/// </summary>
public partial class CustomerProductList : Window
{
    BLApi.IBL? bl = BLApi.Factory.get();
    private BO.Product p = new BO.Product();
    BO.Cart cart = new BO.Cart();
    PO.Cart c = new PO.Cart();
    //IEnumerable<BO.ProductForList> list1;
    IEnumerable<BO.ProductItem> list1;
    //ObservableCollection<PO.ProductForList> List_p = new();
    ObservableCollection<PO.ProductItem> List_p = new();
    Window prevWindow;
    Tuple<ObservableCollection<PO.ProductItem>, Array> dcT;
    public CustomerProductList(BLApi.IBL bl, PO.Cart _c, Window _prevWindow)
    {
        try
        {
            InitializeComponent();
            this.bl = bl;
            this.c = _c;
            this.prevWindow = _prevWindow;
            //list1 = bl.product.GetProductList();
            list1 = bl.product.GetCatalog();
            Array i = Enum.GetValues(typeof(BO.Category));
            Common.convertList(List_p, list1);
            this.dcT = new Tuple<ObservableCollection<PO.ProductItem>, Array>(this.List_p, i);
            this.DataContext = this.dcT;
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BO.BlExceptionFailedToRead ex)
        { MessageBox.Show(ex.Message); }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }

    }
    private void categorySelectorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var allProducts = bl?.product.GetCatalog();
            BO.Category cat = (BO.Category)categorySelectorBox.SelectedItem;
            var list = bl?.product.GetListByCategory(cat);
            if (cat != BO.Category.All)
            {
                var tmp = from product in allProducts
                          group product by product.Category into newGroup
                          where newGroup.Key == cat
                          select newGroup.ToList();
                this.List_p = Common.ConvertToPoProList(tmp, this.List_p);
            }
            else
                List_p = Common.ConvertToPoProList(allProducts, this.List_p);
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BO.BlExceptionFailedToRead ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.BlDefaultException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void addProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), true, this.c, this);
            window.Show();
            InitializeComponent();
            this.List_p = (ObservableCollection<PO.ProductItem>)bl.product.GetProductList();
            this.Hide();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch(BO.BlExceptionFailedToRead ex)
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
            p = bl.product.GetProductManager((ProductsListview.SelectedItem as PO.ProductItem).ID);
            Window window = new ProductWindow(bl, Common.ConvertToPoPro(p), true, this.c, this);
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
        catch(BO.BlInvalidIntegerException ex)
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

    private void showCart(object sender, RoutedEventArgs e)
    {
        Window w = new CartWindow(bl, this.c, this);
        w.Show();
        this.Hide();
    }

    private void goBack(object sender, RoutedEventArgs e)
    {
        this.prevWindow.Show();
        this.Close();
    }
}

