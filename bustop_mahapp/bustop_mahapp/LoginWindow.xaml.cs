using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using bustop_mahapp.Logics;
using bustop_mahapp.Models;
using System.Data;

namespace bustop_mahapp
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            string strTxtID = "";
            string strTxtPW = "";
            if(string.IsNullOrEmpty(TxtID.Text)||string.IsNullOrEmpty(TxtPW.Password))
            {
                await this.ShowMessageAsync("오류", "아이디/패스워드를 입력해주세요!",MessageDialogStyle.Affirmative,null);
                return;
            }
            try
            {
                using(MySqlConnection conn = new MySqlConnection(commons.myConnString))
                {
                    conn.Open();
                    string selQuery = @"SELECT manager_id,
                                               manager_pwd
                                       FROM manager_table
                                       WHERE manager_id = @manager_id AND manager_pwd = @manager_pwd";
                    MySqlCommand selCmd = new MySqlCommand(selQuery, conn);
                    MySqlParameter prmManager_id = new MySqlParameter("@manager_id", TxtID.Text);
                    MySqlParameter prmManager_pwd = new MySqlParameter("@manager_pwd", TxtPW.Password);

                    selCmd.Parameters.Add(prmManager_id);
                    selCmd.Parameters.Add(prmManager_pwd);

                    MySqlDataReader reader = selCmd.ExecuteReader(); // manager_id,manager_pwd 읽어옴
                    if(reader.Read())
                    {
                        strTxtID = reader["manager_id"] != null ? reader["manager_id"].ToString() : "-"; // id 아니면 - 반환
                        strTxtPW = reader["manager_pwd"] != null ? reader["manager_pwd"].ToString() : "-"; // pwd 아니면 -- 반환
                        commons.isManager = true;
                        await this.ShowMessageAsync("로그인 성공", "관리자 모드 활성화", MessageDialogStyle.Affirmative, null);
                        this.Close();
                    }
                    else
                    {
                        TxtID.Focus();
                        await this.ShowMessageAsync("로그인 실패", "관리자 모드 비활성화", MessageDialogStyle.Affirmative, null);
                        TxtID.Text = "";
                        TxtPW.Password = "";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("로그인 오류", $"{ex.Message}", MessageDialogStyle.Affirmative, null);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TxtID.Focus();
        }

        private void TxtID_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                login_Click(sender, e);
            }
        }

        private void TxtPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login_Click(sender, e);
            }
        }
    }
}
