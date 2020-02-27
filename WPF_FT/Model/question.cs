using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FT.Model
{
    class question : BindableBase
    {
        string question_id;

        public string QUESTION_ID
        {
            get { return question_id; }
            set
            {
                SetProperty(ref question_id, value);
            }
        }

        string question_user_id;

        public string QUESTION_USER_ID
        {
            get { return question_user_id; }
            set
            {
                SetProperty(ref question_user_id, value);
            }
        }

        string question_text;

        public string QUESTION_TEXT
        {
            get { return question_text; }
            set
            {
                SetProperty(ref question_text, value);
            }
        }


        int row_index;

        public int ROW_INDEX
        {
            get { return row_index; }
            set
            {
                SetProperty(ref row_index, value);
            }
        }

    }
}
