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
using Wsr1.Login;

namespace Wsr1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SupClass.frm = FrmMain;
            FrmMain.Navigate(new PageLogin());
            DB.database = new felcosWsr1Entities1();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (UserHelpClass.userWasLoginIn)
            {
                var Info = DB.database.Activity.FirstOrDefault(
                        x => x.SessionID == UserHelpClass.sessionID);
                Info.TimeLogout = DateTime.Now.TimeOfDay;
                try
                {
                    DB.database.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            this.Close();
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
