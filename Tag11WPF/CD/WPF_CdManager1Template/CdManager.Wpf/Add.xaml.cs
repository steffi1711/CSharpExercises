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
using System.Windows.Shapes;

namespace CdManager.Wpf
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Add_Loaded);
        }

        private void Add_Loaded(object sender, RoutedEventArgs e)
        {
            btSave.Click += new RoutedEventHandler(BtSave_Click);
            btCancel.Click += new RoutedEventHandler(BtCancel_Click);
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            Cd newCd = new Cd();
            newCd.AlbumTitle = tbTitle.Text;
            newCd.Artist = tbArtist.Text;
            Repository.GetInstance().AddCd(newCd);
            Close();
        }
    }
}
