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
using Wsr1.Windows;
using Wsr1.BD;
using System.Data;
using Wsr1.Classes;

namespace Wsr1
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
            AdminDataGrid.ItemsSource = BD.DB.database.Users.ToList();
            cmbOffice.DisplayMemberPath = "Title";
            cmbOffice.ItemsSource = DB.database.Offices.ToList();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            WindowAddUser addUser = new WindowAddUser();
            addUser.Show();
        }

        private void cmbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdminDataGrid.ItemsSource = BD.DB.database.Users.Where(x => x.Offices.Title == cmbOffice.Text).ToList();
        }
        private void AdminDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowAddUser addUser = new WindowAddUser();
            addUser.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dynamic row = AdminDataGrid.SelectedItem;
            UserHelpClass.IDFromDataGrid = row.ID;
            WindowsEditUser editUser = new WindowsEditUser();
            editUser.Show();
        }

        private void btnBan_Click(object sender, RoutedEventArgs e)
        {
            dynamic row = AdminDataGrid.SelectedItem;
            UserHelpClass.IDFromDataGrid = row.ID;
            var user = DB.database.Users.FirstOrDefault(x => x.ID == UserHelpClass.IDFromDataGrid);
            user.Active = false;
            try
            {
                DB.database.SaveChanges();
                MessageBox.Show("Пользователь забанен, вахавхавхахвхахвхахвхавхахвхав БАААННН", "Успех", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
