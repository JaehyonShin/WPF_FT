using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_FT.Class;
using WPF_FT.Model;
using WPF_FT.View.Play;

namespace WPF_FT.ViewModel.Play
{
    class SelectUserWindowViewModel
    {
        private SelectUserWindow selectUserWindow;

        //private ObservableCollection<members> _members;

        public ObservableCollection<members> Members { get; set; }
        //{
        //    get { return _members; }
        //    set { _members = value; }
        //}

        public Command buttonClick { get; set; }

        public SelectUserWindowViewModel(SelectUserWindow selectUserWindow)
        {
            this.selectUserWindow = selectUserWindow;

            ComboBoxBinding();

            buttonClick = new Command(executeMethod,canexecuteMethod);
        }

        private void ComboBoxBinding()
        {
            Members = new ObservableCollection<members>();

            string qeury = "SELECT  ID, NAME FROM TB_MEMBERS";

            using (DataTable dt = DBSqlite.GetSelectData(qeury))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Members.Add(new members() { ID = dr["ID"].ToString(), NAME = dr["NAME"].ToString() });
                }
            }

        }

        private bool canexecuteMethod(object arg)
        {
            return true;
        }

        private void executeMethod(object obj)
        {
            if (obj.ToString() == "cancel")
            {
                selectUserWindow.Close();
            }
            else if (obj.ToString() == "confirm")
            {
                string userID = selectUserWindow.cbUserName.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(userID))
                {
                    MessageBox.Show(userID);
                }
                
            }
        }
    }
}
