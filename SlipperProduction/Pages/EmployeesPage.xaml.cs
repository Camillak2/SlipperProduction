using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
using SlipperProduction.Windows;

namespace SlipperProduction.Pages
{
    /// <summary>
    /// Логика взаимодействия для MaimMenuPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public static List<Employee> employees { get; set; }
        public static List<Position> positions { get; set; }
        public static List<MachineTool> machines { get; set; }
        public EmployeesPage()
        {
            InitializeComponent();
            employees = DbConnectionClass.DB.Employee.ToList();
            positions = DbConnectionClass.DB.Position.ToList();
            machines = DbConnectionClass.DB.MachineTool.ToList();

            this.DataContext = this;
            //if(App.employee.ID_Position == 1)
            //{

            //}
        }

        private void EmployeesLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = EmployeesLv.SelectedItem as Employee;
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = EmployeesLv.SelectedItem as Employee;
            //new AddEmployeeWindow(employee).ShowDialog();
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow(employee);
            addEmployeeWindow.ShowDialog();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            EmployeesLv.ItemsSource = DbConnectionClass.DB.Employee.ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }
    }
}
