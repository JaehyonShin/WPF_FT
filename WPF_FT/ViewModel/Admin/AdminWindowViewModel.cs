using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_FT.Class;
using WPF_FT.Model;
using WPF_FT.View.Admin;
using WPF_FT.View.Join;

namespace WPF_FT.ViewModel.Admin
{
    class AdminWindowViewModel 
    {
        private AdminWindow adminWindow;

        public ObservableCollection<members> MemberCollection { get; set; }

        public ICommand ButtonCommand { get; set; }

        public AdminWindowViewModel(AdminWindow adminWindow)
        {
            this.adminWindow = adminWindow;

            MemberCollection = new ObservableCollection<members>();
            ButtonCommand = new Command(executeMethod,canexecuteMethod);

            RefreshDataGrid();
        }

        private bool canexecuteMethod(object arg)
        {
            return true;
        }

        private void executeMethod(object obj)
        {
            if (obj.ToString() == "refresh") //새로고침
            {
                
            }
            else if (obj.ToString() == "insert")
            {
                ButtonClickInsert();
            }
            else if (obj.ToString() == "delete")
            {
                ButtonClickDelete();
            }
            else if (obj.ToString() == "apply")
            {
                ButtonClickApply();
            }

            RefreshDataGrid();
        }

        //적용 버튼 클릭
        private void ButtonClickApply()
        {
            List<members> users = MemberCollection.ToList();

            string showMassage = users.Count.ToString() + " 건 의 데이터를 업데이트 하시겠습니까?";

            if (MessageBox.Show(showMassage, "적용", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (members user in users)
                {
                    string deleteQuery = "UPDATE TB_MEMBERS " +
                        "SET NAME = '"+ user.NAME+ "', AUTH = '"+ user.AUTH + "', PASSWORD = '"+ user.PASSWORD + "'  " +
                        "WHERE ID = '" + user.ID + "'";

                    string errorMassage;

                    DBSqlite.QueryExecute(deleteQuery, out errorMassage);

                    if (!string.IsNullOrEmpty(errorMassage))
                    {
                        MessageBox.Show(errorMassage);

                        return;
                    }
                }
            }

            MessageBox.Show("업데이트 완료");
        }

        //삭제 버튼 클릭
        private void ButtonClickDelete()
        {
            List<members> selectedItems = MemberCollection.Where(x => x.IsSelect == true).ToList();

            string showMassage = "선택 하신 " + selectedItems.Count.ToString() + " 건 의 데이터를 지우시겠습니까?";

            if (MessageBox.Show(showMassage,"삭제",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (members user in selectedItems)
                {
                    string deleteQuery = "DELETE FROM TB_MEMBERS where ID = '" + user.ID + "'";

                    string errorMassage;

                    DBSqlite.QueryExecute(deleteQuery, out errorMassage);

                    if (!string.IsNullOrEmpty(errorMassage))
                    {
                        MessageBox.Show(errorMassage);

                        return;
                    }
                }
            }

        }

        //아이디 생성 버튼 클릭
        private void ButtonClickInsert()
        {
            JoinWindow joinWindow = new JoinWindow();

            joinWindow.ShowDialog();
        }

        private void RefreshDataGrid()
        {
            string query = "SELECT  ID, NAME, AUTH, PASSWORD FROM TB_MEMBERS";

            using (DataTable dt = DBSqlite.GetSelectData(query))
            {
                if (dt.Rows.Count > 0)
                {
                    MemberCollection.Clear();

                    foreach (DataRow dr in dt.Rows)
                    {
                        members user = new members()
                        {
                            IsSelect = false,
                            ID = dr["ID"].ToString(),
                            NAME = dr["NAME"].ToString(),
                            AUTH = dr["AUTH"].ToString(),
                            PASSWORD = dr["PASSWORD"].ToString()
                        };

                        MemberCollection.Add(user);
                    }

                }

                
            }

            
        }
    }
}
