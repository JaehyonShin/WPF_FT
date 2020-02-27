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
using WPF_FT.ViewModel.Join;

namespace WPF_FT.View.Join
{
    /// <summary>
    /// JoinWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class JoinWindow : Window
    {
        public JoinWindow()
        {
            InitializeComponent();

            this.DataContext = new JoinWindowViewModel(this);
        }
    }
}
