using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Prakt17
{
    /// <summary>
    /// Логика взаимодействия для AddList.xaml
    /// </summary>
    public partial class AddList : Window
    {
        DB_KPRAKT17Entities db = DB_KPRAKT17Entities.GetContext();
        Drug_store p1 = new Drug_store ();
        public AddList()
        {
            InitializeComponent();
        }

        private void CancelDb_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddToDb_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(PriceDrug.Text, out int price);
            short.TryParse(QuantityDrug.Text, out short quantity);
            DateTime dp = Convert.ToDateTime(CreatedDrug.SelectedDate);
            DateTime dp2= Convert.ToDateTime(experationdrug.SelectedDate);
            StringBuilder errors = new StringBuilder();
            if (NameDrug.Text.Length == 0) errors.AppendLine("Введите название лекарства");
            if ( price == 0 && price<0) errors.AppendLine("Задайте правильную цену");
            if (quantity < 0) errors.AppendLine("Колличество не может быть меньше 0");
            if (dp2 < dp) errors.AppendLine("Срок годности не может быть меньше даты производства");
            if (FirmDrug.Text.Length < 0) errors.AppendLine("Задайте корректное название фирмы производителя");
            p1.Name_of_the_drug=NameDrug.Text;
            p1.Price = price;
            p1.quantity = quantity;
            p1.production_date = dp;
            p1.expiration_date = dp2;
            p1.Factory = FirmDrug.Text;
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                db.Drug_stores.Add(p1);
                db.SaveChanges();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
