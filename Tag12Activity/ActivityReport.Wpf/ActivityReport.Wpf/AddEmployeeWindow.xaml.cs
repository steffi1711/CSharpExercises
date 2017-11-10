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
using System.Windows.Shapes;

namespace ActivityReport.Wpf
{
    /// <summary>
    /// Interaction logic for AddEditEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        Employee newEmp;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            newEmp = new Employee();
            Loaded += AddEmployeeWindow_Loaded;
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.Click += BtnSave_Click;
            btnAbourt.Click += BtnAbourt_Click;
            grdEmp.DataContext = newEmp;
        }

        private void BtnAbourt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Repository.GetInstance().AddEmployee(newEmp);
            Close();
        }
    }
}
