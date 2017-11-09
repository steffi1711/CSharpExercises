using CdManager.Model;
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

namespace CdManager.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Cd> cds;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);

            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvCds.SelectedItem != null)
            {
                Edit trackWindow = new Edit((Cd)lvCds.SelectedItem);
                trackWindow.ShowDialog();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCds.SelectedItem != null)
            {
                Repository.GetInstance().DeleteCd((Cd)lvCds.SelectedItem);
                lvCds.ItemsSource = Repository.GetInstance().GetAllCds();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Add addCdWindow = new Add();
            addCdWindow.ShowDialog();
            cds = Repository.GetInstance().GetAllCds();
            lvCds.ItemsSource = cds;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Repository rep = Repository.GetInstance();
            cds = rep.GetAllCds();
            lvCds.ItemsSource = cds;
        }
    }
}
