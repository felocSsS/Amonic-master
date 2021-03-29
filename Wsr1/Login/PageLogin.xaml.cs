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
using Wsr1.User;
using Wsr1.Classes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Data.Entity;

namespace Wsr1.Login
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public int tick; // тик для таймера
        public DispatcherTimer copyOfTimer; // копия DispatcherTimer 
        public bool canUserLoginIn = true; // для того, что бы ограничить возможность пользователю логинится в систему во время бана на 10 сек
        public int k = 0; // счетчик неудачных попыток залогинится

        public PageLogin()
        {
            InitializeComponent();
        }

        private void btnSingIn_Click(object sender, RoutedEventArgs e)
        {
            if (canUserLoginIn)
            {
                try
                {
                    var Info = DB.database.Users.FirstOrDefault(
                        x => x.Email == tbLogin.Text && x.Password == pbPassword.Password);
                    if (Info != null && Info.Active == true)
                    {
                        BD.Activity userTimeInfo = new BD.Activity()
                        {
                            UserID = Info.ID,
                            Date = DateTime.Today,
                            TimeLogin = (TimeSpan)DateTime.Now.TimeOfDay.ToString("HH:mm:ss"),
                            TimeLogin = DateTime.Now.TimeOfDay
                        };
                        DB.database.Activity.Add(userTimeInfo);
                        DB.database.SaveChanges();
                        UserHelpClass.user = Info;
                        UserHelpClass.sessionID = userTimeInfo.SessionID;
                        UserHelpClass.userWasLoginIn = true;
                        switch (Info.RoleID)
                        {
                            case 1:
                                SupClass.frm.Navigate(new PageAdmin());
                                break;
                            case 2:
                                SupClass.frm.Navigate(new PageUser());
                                break;
                        }
                    }
                    else
                    { 
                        if(Info.Active == false)
                        {
                            MessageBox.Show("Вы забанены", "Ошибка", MessageBoxButton.OK);
                        }
                        else
                        {
                            k++;
                            if (k % 3 == 0)
                            {
                                MessageBox.Show("Такой пользователь не найден, подождите 10 секунд", "Ошибка", MessageBoxButton.OK);
                                Timer();
                            }
                            else
                            {
                                MessageBox.Show("Такой пользователь не найден", "Ошибка", MessageBoxButton.OK);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } 
        }
    
        public void Timer()
        {
            canUserLoginIn = false;
            DispatcherTimer timer = new DispatcherTimer();
            copyOfTimer = timer;
            timer.Tick += new EventHandler(updateText);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void updateText(object sender, EventArgs e)
        {
            TBTimer.Text =  tick.ToString();
            tick += 1;
            if(tick == 12)
            {
                copyOfTimer.Stop();
                tick = 0;
                TBTimer.Text = "";
                canUserLoginIn = true;
            }
        }
    }
}
