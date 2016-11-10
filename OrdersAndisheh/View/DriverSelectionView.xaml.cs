
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.Native;
using OrdersAndisheh.DBL;
using BL;


namespace OrdersAndisheh.View
{
    /// <summary>
    /// Description for MvvmView1.
    /// </summary>
    public partial class DriverSelectionView : Window
    {
       bool isDragStarted;

       public DriverSelectionView()
       {
            InitializeComponent();

            Loaded += new RoutedEventHandler(DriverSelectionView_Loaded);
        }

       void DriverSelectionView_Loaded(object sender, RoutedEventArgs e)
       {

           DriverListBox1.AllowDrop = true;
           DriverListBox2.AllowDrop = true;
           ErsallistBox.AllowDrop = true;

           DriverListBox1.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent, new MouseButtonEventHandler(list_MouseLeftButtonDown), true);
           DriverListBox1.PreviewMouseMove += new MouseEventHandler(list_PreviewMouseMove);
           DriverListBox1.DragOver += new DragEventHandler(list_DragOver);
           DriverListBox1.Drop += new DragEventHandler(list_Drop);

           DriverListBox2.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent, new MouseButtonEventHandler(list_MouseLeftButtonDown), true);
           DriverListBox2.PreviewMouseMove += new MouseEventHandler(list_PreviewMouseMove);
           DriverListBox2.DragOver += new DragEventHandler(list_DragOver);
           DriverListBox2.Drop += new DragEventHandler(list_Drop);

           ErsallistBox.AddHandler(ListBoxEdit.MouseLeftButtonDownEvent, new MouseButtonEventHandler(list_MouseLeftButtonDown), true);
           ErsallistBox.PreviewMouseMove += new MouseEventHandler(list_PreviewMouseMove);
           ErsallistBox.DragOver += new DragEventHandler(list_DragOver);
           ErsallistBox.Drop += new DragEventHandler(list_Drop);
        }

       void list_Drop(object sender, DragEventArgs e)
       {
           ItemSefaresh item = (ItemSefaresh)e.Data.GetData(typeof(ItemSefaresh));
           ObservableCollection<ItemSefaresh> senderItemsSource = (ObservableCollection<ItemSefaresh>)((ListBoxEdit)sender).ItemsSource;

           if (!senderItemsSource.Contains(item) || IsCopyEffect(e))
           {

               if (!IsCopyEffect(e))
               {
                   ListBoxEdit dragSourceListBox = (ListBoxEdit)e.Data.GetData("dragSource");
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
           ObservableCollection<ItemSefaresh> senderItemsSource = (ObservableCollection<ItemSefaresh>)((ListBoxEdit)sender).ItemsSource;

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
                   ListBoxEdit listBoxEdit = (ListBoxEdit)sender;
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
           ListBoxEdit listBoxEdit = (ListBoxEdit)sender;
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

    //public class TestData {
    //    public string Text {get; set;}
    //}
}