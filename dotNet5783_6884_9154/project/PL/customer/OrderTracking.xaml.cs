using BlImplementation;
using System;
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

namespace PL.customer
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        int orderID;
        BO.Order o;
        PO.Order order = new();
        BO.OrderTracking ot;
        BLApi.IBL bl;
        PO.Cart c = new PO.Cart();
        Window prevWindow;
        public OrderTracking(BLApi.IBL _bl ,int _orderID, PO.Cart _c, Window _prevWindow)
        {
            this.c = _c;  
            this.bl = _bl;
            this.orderID = _orderID;
            this.prevWindow = _prevWindow;
            this.ot = bl.order.OrderTrack(this.orderID);
            InitializeComponent();
            DataContext= this.ot;
            orderDates.DataContext = new { mylist = new ObservableCollection<Tuple<DateTime?, BO.OrderStatus?>>(ot?.dateAndTrack) 
            };
        }
    

        private void show_order_details(object sender, RoutedEventArgs e)
        {
            try
            {

                this.o = bl.order.GetOrderDetails(this.orderID);
                this.order = Common.ConvertToPoOrder(this.o,this.order);
                Window w = new PL.OrderWindow(this.bl, this.order, true, this.c,this);
                w.Show();
                this.Hide();
            }
            catch(BO.BlEntityNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.BlExceptionFailedToRead ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "orderTracking: 95");
            }
        }


        private void go_Back(object sender, RoutedEventArgs e)
        {
            this.prevWindow.Show();
            this.Close(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window home = new MainWindow();
            home.Show();
            this.Close();
        }
    }
}
