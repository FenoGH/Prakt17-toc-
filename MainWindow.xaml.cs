using System;
using System.Collections.Generic;
using System.Data.Entity;
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


namespace Prakt17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB_KPRAKT17Entities db = DB_KPRAKT17Entities.GetContext();
        List<Drug_store> _drug;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Drug_stores.Load();
            DrugDG.ItemsSource= db.Drug_stores.Local.ToBindingList();   
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddList f = new AddList();
            f.ShowDialog();
            DrugDG.Focus();

        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
           
            int indexRow = DrugDG.SelectedIndex;
            if (indexRow != -1)
            {
                Drug_store row = (Drug_store)DrugDG.Items[indexRow];
                Transmission.Id = row.Id;

                ChangeList f = new ChangeList();
                f.ShowDialog();
                DrugDG.Items.Refresh();
                DrugDG.Focus();
            }
        }

        private void DelFromDb_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Вы действительно хотите удалить запись ?","Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    Drug_store row = (Drug_store)DrugDG.SelectedItems[0];
                    db.Drug_stores.Remove(row);
                    db.SaveChanges();
                }
                catch(ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберете запись");
                }
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0;i<DrugDG.Items.Count;i++) 
            {
             var row = (Drug_store)DrugDG.Items[i];
                string findContent = row.Factory;
                try
                {
                    if(findContent != null && findContent.Contains(FindBox.Text))
                    {
                        object item = DrugDG.Items[i];
                        DrugDG.SelectedItem= item;
                        DrugDG.ScrollIntoView(item);
                        DrugDG.Focus();
                        break;
                    }
                }
                catch
                {
                    
                }
            
            }
        }

        private void QuantityButton_Click(object sender, RoutedEventArgs e)
        {
            short.TryParse(quantityBox.Text, out short quantity);
            _drug = db.Drug_stores.ToList();
            var filtered = _drug.Where(_drug => _drug.quantity == quantity);
            DrugDG.ItemsSource= filtered;
        }

        private void About_prog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: студент группы ИСП-31 Борькин И.В\nЗадание:\nУчет лекарств в аптеке. База данных должна содержать следующую информацию: наименование лекарства, стоимость одной единицы, количество единиц, дату изготовления, срок годности, а также название фабрики, где производится данное лекарство, ее адрес.");
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void filtrstop(object sender, RoutedEventArgs e)
        {
            DrugDG.ItemsSource = db.Drug_stores.Local.ToBindingList();
        }
    }
}

