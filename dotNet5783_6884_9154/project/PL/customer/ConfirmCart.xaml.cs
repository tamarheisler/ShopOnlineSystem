using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

namespace PL.customer
{
    /// <summary>
    /// Interaction logic for ConfirmCart.xaml
    /// </summary>
    public partial class ConfirmCart : Window
    {
        BLApi.IBL bl;
        BO.Cart cart;
        PO.Cart c;
        Window prevWindow;
        public ConfirmCart(BLApi.IBL _bl, PO.Cart _c, Window _prevWindow)
        {
            InitializeComponent();
            this.bl = _bl;
            this.c = _c;
            this.prevWindow = _prevWindow;
            DataContext = this.c;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = bl.cart.CartConfirmation(Common.ConvertToBoCart(this.c), this.c.CustomerName, this.c.CustomerEmail, this.c.CustomerAddress);
                MessageBox.Show("The order was successfully created");
                Window w = new OrderTracking(bl, id, this.c,this);
                w.Show();
                this.Hide();
            }
            catch(CustomerDetailsAreInValid ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(PlInvalidValueExeption ex){
                MessageBox.Show(ex.Message);
            }
            catch (BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BlDefaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void goBack(object sender, RoutedEventArgs e)
        {
            this.prevWindow.Show();
            this.Close();
        }
    }
}
