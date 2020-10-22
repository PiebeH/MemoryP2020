using MemoryGame.ViewModels;
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

namespace MemoryGame.Views
{

    // Voor StartMenuView.xaml

    public partial class StartMenuView : UserControl
    {
        public StartMenuView()
        {
            InitializeComponent();
        }

        private void Play_Clicked(object sender, RoutedEventArgs e)
        {
            var startMenu = DataContext as ViewModels.StartMenuView;
            startMenu.StartNewGame(categoryBox.SelectedIndex);
        }
    }
}
