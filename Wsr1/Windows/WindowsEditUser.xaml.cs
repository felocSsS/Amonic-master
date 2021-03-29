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
using Wsr1.Classes;

namespace Wsr1.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowsEditUser.xaml
    /// </summary>
    public partial class WindowsEditUser : Window
    {
        public WindowsEditUser()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(tbEmail.Text != null && tbFirstName.Text != null && tbLastName.Text != null)
            {
                var user = DB.database.Users.FirstOrDefault(x => x.ID == UserHelpClass.IDFromDataGrid);
                user.Email = tbEmail.Text;
                user.LastName = tbLastName.Text;
                user.FirstName = tbFirstName.Text;
                try
                {
                    DB.database.SaveChanges();
                    MessageBox.Show("Информация о пользователе изменена", "Успех", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Заполните поля" ,"Ошибка", MessageBoxButton.OK);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GridDragMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
