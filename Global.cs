using ListBoxBinding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ListBoxBinding
{
    public class Global
    {
        private static DataProvider _Context = null;
        public static DataProvider Context
        {
            get
            {
                if (_Context == null)
                {
                    try
                    {
                        _Context = new DataProvider();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                        throw;
                    }
                }
                return _Context;
            }
            set
            {
                _Context = value;
            }
        }

    }
}
