using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsr1.BD;

namespace Wsr1.Classes
{
    class UserHelpClass
    {
        public static Users user { get; set; }
        public static int ID { get; set; }
        public static int sessionID { get; set; }
        public static bool userWasLoginIn { get; set; }
        public static int IDFromDataGrid { get; set; }
    }
}
