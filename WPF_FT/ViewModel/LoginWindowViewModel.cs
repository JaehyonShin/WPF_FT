using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_FT.Class;
using WPF_FT.View;
using WPF_FT.View.Join;

namespace WPF_FT.ViewModel
{
    class LoginWindowViewModel : BindableBase
    { 
        private LoginWindow loginWindow;

        public ICommand ButtonCommand { get; set; }
        public ICommand KeyDowmCommand { get; set; }
        public ICommand JoinCommand { get; set; }
        


        public LoginWindowViewModel(LoginWindow loginWindow)
        {
            //초기화
            this.loginWindow = loginWindow;
            Application.Current.Resources["id"] = null;
            loginWindow.tbID.Focus();

            //이벤트 설정
            ButtonCommand = new DelegateCommand(MainButtonClick);
            KeyDowmCommand = new DelegateCommand(KeyDownEvent);
            JoinCommand = new DelegateCommand(JoinTextBlockClick);
        }

        private void JoinTextBlockClick()
        {
            JoinWindow joinWindow = new JoinWindow(); //create your new form.
            joinWindow.ShowDialog(); //show the new form.
        }

        private void KeyDownEvent()
        {
            MainButtonClick();
        }

        private void MainButtonClick()
        {
            string id = loginWindow.tbID.Text;
            string pw = loginWindow.tbPW.Password;

            if (LoginConfirmation(id, pw))
            {
                MainWindow mainWindow = new MainWindow(); //create your new form.
                mainWindow.ShowDialog(); //show the new form.
                //loginWindow.Close(); //only if you want to close the current form.
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.");

                loginWindow.tbPW.Password = string.Empty;
            }
        }

        private bool LoginConfirmation(string userID,string userPassWord)
        {
            string query = "SELECT * from TB_MEMBERS WHERE ID = '"+ userID+"' AND PASSWORD = '"+userPassWord+"'";

            using (DataTable dt = DBSqlite.GetSelectData(query))
            {
                if (dt.Rows.Count > 0)
                {
                    Application.Current.Resources["id"] = userID;

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
