using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_FT.Class;
using WPF_FT.Model;
using WPF_FT.View.Join;

namespace WPF_FT.ViewModel.Join
{
    class JoinWindowViewModel
    {
        private JoinWindow joinWindow;

        public ICommand ButtonClick { get; set; }


        public JoinWindowViewModel(JoinWindow joinWindow)
        {
            this.joinWindow = joinWindow;

            ButtonClick = new Command(executeMethod, canexecuteMethod);
        }

        private bool canexecuteMethod(object arg)
        {
            return true;
        }

        private void executeMethod(object obj)
        {
            if (obj.ToString() == "Cancel") // 취소
            {
                joinWindow.Close();
            }
            else //생성   
            {
                members newUser = new members()
                {
                    ID = joinWindow.tbID.Text,
                    PASSWORD = joinWindow.pbPassWord.Password,
                    AUTH = "user",
                    NAME = joinWindow.tbName.Text
                };

                CreateUser(newUser);
            }
        }


        //유저 생성
        private void CreateUser(members newUser)
        {
            if (ConfirmUserID(newUser))
            {
                InsertUser(newUser);
            }
            else
            {
                MessageBox.Show("이미 등록된 아이디 입니다.");
            }
        }

        //데이터 입력
        private void InsertUser(members newUser)
        {
            string query = "INSERT INTO TB_MEMBERS(ID, NAME, AUTH, PASSWORD) " +
                "VALUES ('" + newUser.ID+ "','"+ newUser.NAME + "','"+ newUser.AUTH + "','"+ newUser.PASSWORD + "')";

            string errMsg;

            if (DBSqlite.QueryExecute(query, out errMsg))
            {

                if (MessageBox.Show("가입이 완료되었습니다.", "완료", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    joinWindow.Close();
                }

            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
            }
        }


        //유저 아이디 판별
        private bool ConfirmUserID(members newUser)
        {
            string query = "SELECT ID FROM TB_MEMBERS WHERE ID = '" + newUser.ID + "'";

            using (DataTable dt = DBSqlite.GetSelectData(query))
            {
                if (dt.Rows.Count <= 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
