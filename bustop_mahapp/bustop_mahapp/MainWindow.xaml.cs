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
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using bustop_mahapp.Logics;
using bustop_mahapp.Models;
using System.Data;

namespace bustop_mahapp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if(!commons.isManager)
            //{
            //    StsResult.Content = "로그인을 먼저 진행해주세요";
            //}
            //else
            //{
            //    StsResult.Content = "관리자 모드 활성화";
            //}
        }

        private async void BtnBusInfor_Click(object sender, RoutedEventArgs e)
        {
            if(!commons.isManager)
            {
                await commons.ShowMessageAsync("권한 없음", $"먼저 관리자모드로 로그인 해주세요!");
            }
            else
            {
                this.DataContext = null;
                List<businfor> list = new List<businfor>();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(commons.myConnString))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        var query = @"SELECT bus_idx,
                                        bus_num,
                                        bus_cnt,
                                        bus_gap
                                    FROM bus_table
                                    ORDER BY bus_idx ASC";
                        var cmd = new MySqlCommand(query, conn);
                        var adapter = new MySqlDataAdapter(cmd);
                        var dSet = new DataSet();
                        adapter.Fill(dSet, "businfor");
                        foreach (DataRow dr in dSet.Tables["businfor"].Rows)
                        {
                            list.Add(new businfor
                            {
                                Bus_idx = Convert.ToInt32(dr["bus_idx"]),
                                Bus_num = Convert.ToString(dr["bus_num"]) + "번",
                                Bus_cnt = Convert.ToInt32(dr["bus_cnt"]) + "명",
                                Bus_gap = Convert.ToInt32(dr["bus_gap"]) + "분"
                            });
                        }
                        this.DataContext = list;
                        StsResult.Content = $"버스 {list.Count}대 정보 조회 완료!";
                    }
                }
                catch (Exception ex)
                {
                    await commons.ShowMessageAsync("오류", $"버스 정보 출력 오류 {ex.Message}");
                }
            }
        }

        private async void BtnInsertBusInfor_Click(object sender, RoutedEventArgs e)
        {
            if(!commons.isManager)
            {
                await commons.ShowMessageAsync("권한 없음", "먼저 관리자모드로 로그인 해주세요!");
            }
            else
            {
                var insertbusinforWindow = new insertbusinfo();
                insertbusinforWindow.Owner = this;
                insertbusinforWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                insertbusinforWindow.ShowDialog();
            }
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(commons.isManager)
            {
                await commons.ShowMessageAsync("오류", "이미 관리자 모드로 로그인 된 상태입니다!");
            }
            else
            {
                var loginWindow = new LoginWindow();
                loginWindow.Owner = this;
                loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                loginWindow.ShowDialog();
            }
        }

        private async void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if(!commons.isManager)
            {
                await commons.ShowMessageAsync("오류", "로그아웃 된 상태입니다!");
            }
            else
            {
                await commons.ShowMessageAsync("로그아웃", "관리자모드 비활성화");
                commons.isManager = false;
                this.DataContext = null;
                StsResult.Content = null;
            }
        }
    }
}
