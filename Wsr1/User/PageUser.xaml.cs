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
using Wsr1.BD;
using Wsr1.Classes;

namespace Wsr1.User
{
    /// <summary>
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        public PageUser()
        {
            InitializeComponent();
            var datbas = DB.database.Activity;
            tbName.Text = "Привет " + UserHelpClass.user.FirstName.ToString();
            UserDataGrid.ItemsSource = DB.database.Activity.Where(x => x.UserID == UserHelpClass.user.ID).ToList();
        }
    }
}
