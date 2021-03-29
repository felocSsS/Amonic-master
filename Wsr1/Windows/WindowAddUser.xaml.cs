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
using Wsr1.BD;

namespace Wsr1.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAddUser.xaml
    /// </summary>
    public partial class WindowAddUser : Window
    {
        public WindowAddUser()
        {
            InitializeComponent();
            CmbOffice.SelectedValuePath = "ID";
            CmbOffice.DisplayMemberPath = "Title";
            CmbOffice.ItemsSource = DB.database.Offices.ToList();
        }

        private void GridDragMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Users newUser = new Users()
            {
                Email = tbEmail.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                OfficeID = (int)CmbOffice.SelectedValue,
                Birthdate = Convert.ToDateTime(tbBirthdate.Text),
                Password = tbPassword.Text,
                RoleID = 2
            };
            DB.database.Users.Add(newUser);
            DB.database.SaveChanges();
            MessageBox.Show("Успех", "Пользователь зарегестрирован", MessageBoxButton.OK);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
