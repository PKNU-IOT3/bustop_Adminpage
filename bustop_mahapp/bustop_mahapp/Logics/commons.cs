using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace bustop_mahapp.Logics
{
    public class commons
    {
        public static readonly string myConnString = "Server=localhost;" +
                                                     "Port=3306;" +
                                                     "Database=bus;" +
                                                     "Uid=root;" +
                                                     "Pwd=12345;";
        public static async Task<MessageDialogResult> ShowMessageAsync(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            return await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(title, message, style, null);
        }

        // 관리자모드로 로그인됨을 확인하기 위한 bool 함수, 서로다른 .xaml.cs파일에서 공통으로 사용하기 위해 commons 클래스로 선언
        public static bool isManager = false; 
    }
}