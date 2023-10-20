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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SlipperProduction.DB;

namespace SlipperProduction.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlanPage.xaml
    /// </summary>
    public partial class PlanPage : Page
    {
        public static List<Plan> plans { get; set; }
        public static List<Employee> employees { get; set; }
        public PlanPage()
        {
            InitializeComponent();
            plans = new List<Plan>
                (DbConnectionClass.DB.Plan.ToList());
            employees = new List<Employee>
                (DbConnectionClass.DB.Employee.ToList());
            this.DataContext = this;
            PlanLv.ItemsSource = new List<Plan>
                (DbConnectionClass.DB.Plan.ToList());
        }

        private void RecordBT_Click(object sender, RoutedEventArgs e)
        {
            var dcvd = "Цветные тапочки " + ColorTb.Text + " " +
                "Обычные тапочки " + OrdinaryTb.Text;

            if (MessageBox.Show(dcvd, "Проверьте корректность введенных данных", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                Plan plans = new Plan();

                plans.Date = DateP.SelectedDate;
                plans.AmountColorSlippes = int.Parse(ColorTb.Text);
                plans.AmountOrdinarySlippes = int.Parse(OrdinaryTb.Text);

                var t = EmployeeCb.SelectedItem as Employee;
                plans.ID_Employee = t.ID;

                var check = DoneCb.IsChecked;
                plans.DoneOrNot = check.Value;

                DbConnectionClass.DB.Plan.Add(plans);
                DbConnectionClass.DB.SaveChanges();

                //PlanLv.ItemsSource = DbConnectionClass.DB.Plan.ToList();
                Refresh();
            }
        }
        private void Refresh()
        {
            PlanLv.ItemsSource = DbConnectionClass.DB.Plan.ToList();
        }
        private void BackBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }
    }
}
