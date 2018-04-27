using ListBoxBinding.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ListBoxBinding
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand _NewWindow = null;
        public ICommand NewWindow
        {
            get
            {
                if (_NewWindow == null)
                    _NewWindow = new ProxyCommand(() =>
                    {
                        Window Win = new Window();
                        Win.Height = Double.NaN;
                        Win.Width = Double.NaN;

                        Grid DynamicGrid = new Grid();
                        DynamicGrid.Width = Double.NaN;
                        DynamicGrid.Height= Double.NaN;
                        DynamicGrid.Background = new SolidColorBrush(Colors.Transparent);

                        // Create Rows
                        RowDefinition gridRow1 = new RowDefinition();
                        RowDefinition gridRow2 = new RowDefinition();

                        DynamicGrid.RowDefinitions.Add(gridRow1);
                        DynamicGrid.RowDefinitions.Add(gridRow2);

                        ContentControl cc = new ContentControl();
                        cc.Content = new ManageSkillsViewModel();

                        Button bt = new Button();
                        bt.Content = "Clear the the content below";
                        bt.VerticalAlignment = VerticalAlignment.Center;
                        bt.HorizontalAlignment = HorizontalAlignment.Center;
                        bt.Padding = new Thickness(3);
                        bt.Command = new ProxyCommand(()=>{
                            cc.Content=null;
                            MessageBox.Show("ContentControl = null done !");
                        });

                        Grid.SetRow(bt, 0);
                        Grid.SetRow(cc, 1);
                        DynamicGrid.Children.Add(bt);
                        DynamicGrid.Children.Add(cc);
                        Win.Content = DynamicGrid;
                        Win.Show();
                    });
                return _NewWindow;
            }
        }

    }
}
