using ActivityManager.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActivityReport.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
            btnNew.Click += BtnNew_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEditActivities.Click += BtnEditActivities_Click;
        }

        private void BtnEditActivities_Click(object sender, RoutedEventArgs e)
        {
            if(lvEmployees.SelectedItem !=null)
            {
                EditActivitiesWindow window = new EditActivitiesWindow((Employee)lvEmployees.SelectedItem);
                window.ShowDialog();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem != null)
            {
                Repository.GetInstance().DeleteEmployee((Employee)lvEmployees.SelectedItem);
                LoadEmployees();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lvEmployees.SelectedItem != null)
            {
                EditEmployeeWindow window = new EditEmployeeWindow((Employee)lvEmployees.SelectedItem);
                window.ShowDialog();
                LoadEmployees();
            }
        }

        
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow window = new AddEmployeeWindow();
            window.ShowDialog();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            lvEmployees.ItemsSource = Repository.GetInstance().GetAllEmployees();
        }
    }
}
