using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FT.Model
{
    class members : BindableBase
    {
        bool isSelect;

        public bool IsSelect
        {
            get { return isSelect; }
            set
            {
                SetProperty(ref isSelect, value);
            }
        }

        string id;

        public string ID    
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }

        string name;

        public string NAME
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }


        string auth;

        public string AUTH
        {
            get { return auth; }
            set
            {
                SetProperty(ref auth, value);
            }
        }

        string password;

        public string PASSWORD
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }
    }
}
