using System;
using System.Collections.Generic;
using Prism.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using System.Windows;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using WPF_FT.Model;
using WPF_FT.Class;
using System.Windows.Controls;
using WPF_FT.View.Admin;
using WPF_FT.View.Play;
using WPF_FT.View.Setting;

namespace WPF_FT.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private MainWindow mainWindowView;

        public ICommand ButtonCommand { get; set; }

        public ObservableCollection<members> UserImageModel { get; set; }

        string id = Application.Current.Resources["id"].ToString();

        public MainWindowViewModel(MainWindow MainWindowView)
        {
            this.mainWindowView = MainWindowView;

            UserImageModel = new ObservableCollection<members>();

            mainWindowView.Title = id;

            //이벤트 설정
            ButtonCommand = new Command(ButtonExecuteMethod, CanExecuteMethod);

        }

        private bool CanExecuteMethod(object arg)
        {
            return false;
        }

        private void ButtonExecuteMethod(object obj)
        {
            if (obj.ToString() == "play")
            {
                SelectUserWindow selectWindow = new SelectUserWindow();
                selectWindow.ShowDialog();
            }
            else if (obj.ToString() == "setting")
            {
                SettingWindow settingWindow = new SettingWindow();
                settingWindow.ShowDialog();
            }
            else if (obj.ToString() == "admin")
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.ShowDialog();
            }
        }


    }
}
