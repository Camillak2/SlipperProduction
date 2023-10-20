using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SlipperProduction.DB;
using SlipperProduction.Pages;

namespace SlipperProduction.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public static List<Employee> employees { get; set; }
        public static List<Position> positions { get; set; }
        public static List<MachineTool> machines { get; set; }
        Employee contextEmployee;

        public AddEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            PositionCb.ItemsSource = DbConnectionClass.DB.Position.ToList();
            MachineToolCb.ItemsSource = DbConnectionClass.DB.MachineTool.ToList();
            contextEmployee = employee;
            employees = new List<Employee>
                (DbConnectionClass.DB.Employee.ToList());
            positions = new List<Position>
                (DbConnectionClass.DB.Position.ToList());
            machines = new List<MachineTool>
                (DbConnectionClass.DB.MachineTool.ToList());
            this.DataContext = this;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var dcvd = SurnameTb.Text + " " + NameTb.Text + " " + PatronymicTb.Text
            + " " + DateOfBirthDp.SelectedDate + " " + PassportDetailsTb.Text;
            if (MessageBox.Show(dcvd, "Проверьте корректность введенных данных",
                MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
            {
                Employee employee = new Employee();

                employee.Surname = SurnameTb.Text.Trim();
                employee.Name = NameTb.Text.Trim();
                employee.Patronymic = PatronymicTb.Text.Trim();
                employee.DateOfBirth = DateOfBirthDp.SelectedDate;
                employee.PassportDetails = PassportDetailsTb.Text.Trim();

                var t = PositionCb.SelectedItem as Position;
                employee.ID_Position = t.ID;

                var q = MachineToolCb.SelectedItem as MachineTool;
                employee.ID_MachineTool = q.ID;

                //if(MachineToolCb.SelectedItem is MachineTool machineTool)
                //{
                //    employee.MachineTool = machineTool;
                //}


                DbConnectionClass.DB.Employee.Add(employee);
                DbConnectionClass.DB.SaveChanges();

                this.DialogResult = true;
            }
        }
    }
}
