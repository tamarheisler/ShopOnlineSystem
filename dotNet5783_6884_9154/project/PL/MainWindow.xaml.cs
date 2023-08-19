using System.ComponentModel;
using System;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BLApi.IBL bl = BLApi.Factory.get();
        BO.Cart cart = new BO.Cart();
        PO.Cart c = new PO.Cart();
        private int ID;
        private bool isDataSaved;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void showProductBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerProductList w = new CustomerProductList(bl,this.c, this);
            w.Show();
            this.Hide();
        }

        private void displayAdminWindow(object sender, RoutedEventArgs e)
        {
            AdminWindow w = new AdminWindow(bl, this.c, this);
            w.Show();
            this.Hide();
        }

        

        private void OrderTracking_clicked(object sender, RoutedEventArgs e)
        {
            this.ID = int.Parse(id_.Text);
            Window w = new customer.OrderTracking(bl ,this.ID, this.c, this);
            w.Show();
            this.Hide();
        }

        private void simulation(object sender, RoutedEventArgs e)
        {
            Window sim = new SimulatorWindow(bl);
            sim.Show();
        }
    }
}
