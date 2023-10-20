using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Employee> employees { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text.Trim();
            string password = PasswordTB.Password.Trim();

            employees = DbConnectionClass.DB.Employee.ToList();
            Employee currentUser = employees.FirstOrDefault(i => i.Login == login && i.Password == password);
            //var user = employees.FirstOrDefault(r => r.Login == LoginTB.Text.Trim());
            //if (user != null)
            //{
            //    MessageBox.Show("Пользователь с таким логином уже существует");
            //    return;
            //}
            if (currentUser != null)
            {
                App.employee = currentUser;
                NavigationService.Navigate(new MainMenuPage());
            }
            else
                MessageBox.Show("Неправильный логин или пароль. Попробуйте снова.");
        }
    }
}
