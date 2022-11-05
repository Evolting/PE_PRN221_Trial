using Q1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PRN221_TrialContext context = new PRN221_TrialContext();

        public void Reload()
        {
            rbMale.IsChecked = true;

            context = new PRN221_TrialContext();

            lvEmployees.ItemsSource = context.Employees.Where(e => e.Phone != null).ToList();
        }

        public MainWindow()
        {
            InitializeComponent();

            rbMale.IsChecked = true;

            lvEmployees.ItemsSource = context.Employees.Where(e => e.Phone != null).ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Name = txtEmployeeName.Text;
            employee.Gender = rbMale.IsChecked.Value ? "Male" : "Female";
            employee.Dob = dpDob.SelectedDate.Value;
            employee.Phone = txtPhone.Text;
            employee.Idnumber = txtIDNumber.Text;

            context.Add(employee);
            context.SaveChanges();

            Reload();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(txtEmployeeId.Text);

            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);

            context.Employees.Remove(emp);
            context.SaveChanges();
            Reload();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(txtEmployeeId.Text);

            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            emp.Name = txtEmployeeName.Text;
            emp.Gender = rbMale.IsChecked.Value ? "Male" : "Female";
            emp.Dob = dpDob.SelectedDate.Value;
            emp.Phone = txtPhone.Text;
            emp.Idnumber = txtIDNumber.Text;

            context.Update(emp);
            context.SaveChanges();

            Reload();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            Employee item = (Employee)(sender as ListView).SelectedItem;
            if (item != null)
            {
                if (item.Gender.Equals("Male"))
                {
                    rbMale.IsChecked = true;
                    rbFemale.IsChecked = false;
                }
                if (item.Gender.Equals("Female"))
                {
                    rbMale.IsChecked = false;
                    rbFemale.IsChecked = true;
                }
            }
        }
    }
}
