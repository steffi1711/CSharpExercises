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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private Cd cd;

        public Edit(Cd cd)
        {

            InitializeComponent();
            this.cd = cd;
            Loaded += TrackDetailsWindow_Loaded;
        }

        private void TrackDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnClose.Click += BtnClose_Click;
            LoadTracks();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadTracks()
        {
            dgTracks.ItemsSource = Repository.GetInstance().GetAllTracksPerCds(cd);
        }


    }
}
