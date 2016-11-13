using BL;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using OrdersAndisheh.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrdersAndisheh.View
{
    /// <summary>
    /// Interaction logic for DriverContainerUserControl.xaml
    /// </summary>
    public partial class DriverContainerUserControl : UserControl
    {
        bool isDragStarted;
        public DriverContainerUserControl()
        {
            InitializeComponent();
            //this.DataContext = new DriverContainerViewModel();
            Loaded += new RoutedEventHandler(DriverContainerView_Loaded);
        }

        private void DriverContainerView_Loaded(object sender, RoutedEventArgs e)
        {
            DriverListBox.AllowDrop = true;
            DriverListBox.AddHandler(ListView.MouseLeftButtonDownEvent, new MouseButtonEventHandler(list_MouseLeftButtonDown),
                true);
            DriverListBox.PreviewMouseMove += new MouseEventHandler(list_PreviewMouseMove);
            DriverListBox.DragOver += new DragEventHandler(list_DragOver);
            DriverListBox.Drop += new DragEventHandler(list_Drop);
        }

        void list_Drop(object sender, DragEventArgs e)
        {
            ItemSefaresh item = (ItemSefaresh)e.Data.GetData(typeof(ItemSefaresh));
            ObservableCollection<ItemSefaresh> senderItemsSource = (ObservableCollection<ItemSefaresh>)((ListView)sender).ItemsSource;

            if (!senderItemsSource.Contains(item) || IsCopyEffect(e))
            {

                if (!IsCopyEffect(e))
                {
                    ListView dragSourceListBox = (ListView)e.Data.GetData("dragSource");
                    ObservableCollection<ItemSefaresh> dragSourceItemsSource = (ObservableCollection<ItemSefaresh>)dragSourceListBox.ItemsSource;

                    dragSourceItemsSource.Remove(item);
                }

                ItemSefaresh copyItem = item;
                senderItemsSource.Add(copyItem);
            }
        }

        void list_DragOver(object sender, DragEventArgs e)
        {
            ItemSefaresh item = (ItemSefaresh)e.Data.GetData(typeof(ItemSefaresh));
            ObservableCollection<ItemSefaresh> senderItemsSource = (ObservableCollection<ItemSefaresh>)((ListView)sender).ItemsSource;

            if (senderItemsSource.Contains(item))
            {
                e.Effects = IsCopyEffect(e) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            else
            {
                e.Effects = IsCopyEffect(e) ? DragDropEffects.Copy : DragDropEffects.Move;
            }

            e.Handled = true;
        }

        bool IsCopyEffect(DragEventArgs e)
        {
            return (e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey;
        }

        void list_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (isDragStarted)
                {
                    ListView listBoxEdit = (ListView)sender;
                    ItemSefaresh item = (ItemSefaresh)listBoxEdit.SelectedItem;

                    if (item != null)
                    {
                        DataObject data = CreateDataObject(item);
                        data.SetData("dragSource", listBoxEdit);
                        DragDrop.DoDragDrop(listBoxEdit, data, DragDropEffects.Move | DragDropEffects.Copy);
                        isDragStarted = false;
                    }
                }
            }
        }

        void list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView listBoxEdit = (ListView)sender;
            DependencyObject hittedObject = listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)) as DependencyObject;
            FrameworkElement hittedItem = LayoutHelper.FindParentObject<ListBoxEditItem>(hittedObject);

            if (hittedItem != null)
            {
                isDragStarted = true;
            }
        }

        private DataObject CreateDataObject(ItemSefaresh item)
        {
            DataObject data = new DataObject();
            data.SetData(typeof(ItemSefaresh), item);
            return data;
        }
    }
}
