using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_FT.Class;
using WPF_FT.Model;
using WPF_FT.View.Setting;

namespace WPF_FT.ViewModel.Setting
{
    class SettingWindowViewModel
    {
        private SettingWindow settingWindow;

        public ObservableCollection<question> QuestionCollection { get; set; }

        public ICommand ButtonClick { get; set; }


        public SettingWindowViewModel(SettingWindow settingWindow)
        {
            this.settingWindow = settingWindow;

            QuestionCollection = new ObservableCollection<question>();
            ButtonClick = new Command(button_Click, can_button_Click);


            GetQuestion();
        }


        private bool can_button_Click(object arg)
        {
            return true;
        }

        private void button_Click(object obj)
        {
            foreach (question q in QuestionCollection)
            {
                applyDataBase(q);
            }
        }

        private void applyDataBase(question q)
        {
            
        }

        private void GetQuestion()
        {
            string query = "SELECT  QUESTION_ID, QUESTION_USER_ID, QUESTION_TEXT, ROW_INDEX FROM TB_USER_QUESTION";

            using (DataTable dt = DBSqlite.GetSelectData(query))
            {
                if (dt.Rows.Count > 0)
                {
                    QuestionCollection.Clear();

                    foreach (DataRow dr in dt.Rows)
                    {
                        question Question = new question()
                        {
                            QUESTION_ID = dr["QUESTION_ID"].ToString(),
                            QUESTION_USER_ID = dr["QUESTION_USER_ID"].ToString(),
                            QUESTION_TEXT = dr["QUESTION_TEXT"].ToString(),
                            ROW_INDEX = (int)dr["ROW_INDEX"]
                        };

                        QuestionCollection.Add(Question);
                    }
                }
            }
        }
    }
}
