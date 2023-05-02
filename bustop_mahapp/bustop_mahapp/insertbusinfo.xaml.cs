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
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using MahApps.Metro.Controls.Dialogs;
using bustop_mahapp.Logics;
using System.Data;
using bustop_mahapp.Models;
using bustop_mahapp;

namespace bustop_mahapp
{
    /// <summary>
    /// insertbusinfo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class insertbusinfo : MetroWindow
    {
        public insertbusinfo()
        {
            InitializeComponent();
        }

        private bool Isexit() // 이미 DB에 저장되어있는 버스번호 인지 확인 
        {
            List<businfor> list = new List<businfor>();
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
                        Bus_num = Convert.ToString(dr["bus_num"]),
                        Bus_cnt = Convert.ToInt32(dr["bus_cnt"]) + "명",
                        Bus_gap = Convert.ToInt32(dr["bus_gap"]) + "분"
                    });
                }
                foreach (businfor item in list)
                {
                    string strbus_num = item.Bus_num;
                    if (strbus_num == TxtBus_num.Text)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private async void insert_Click(object sender, RoutedEventArgs e)
        {
            string strBus_idx = TxtBus_idx.Text;
            string strBus_num = TxtBus_num.Text;
            string strBus_cnt = TxtBus_cnt.Text;
            string strBus_gap = TxtBus_gap.Text;
            if (string.IsNullOrEmpty(TxtBus_idx.Text)|| string.IsNullOrEmpty(TxtBus_num.Text)|| string.IsNullOrEmpty(TxtBus_cnt.Text)|| string.IsNullOrEmpty(TxtBus_gap.Text))
            {
                await this.ShowMessageAsync("오류", "버스 정보를 모두 입력해주세요!", MessageDialogStyle.Affirmative, null);
                return;
            }
            if(Isexit())
            {
                TxtBus_idx.Focus();
                await this.ShowMessageAsync("데이터 중복", "이미 존재하는 버스 번호입니다!", MessageDialogStyle.Affirmative, null);
                TxtBus_idx.Text = "";
                TxtBus_num.Text = "";
                TxtBus_cnt.Text = "";
                TxtBus_gap.Text = "";
            }
            else
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(commons.myConnString))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        var query = @"INSERT INTO bus_table
                                                (bus_idx,
                                                bus_num,
                                                bus_cnt,
                                                bus_gap)
                                                VALUES
                                                (@bus_idx,
                                                @bus_num,
                                                @bus_cnt,
                                                @bus_gap)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@bus_idx", Convert.ToInt32(strBus_idx));
                        cmd.Parameters.AddWithValue("@bus_num", strBus_num);
                        cmd.Parameters.AddWithValue("@bus_cnt", Convert.ToInt32(strBus_cnt));
                        cmd.Parameters.AddWithValue("@bus_gap", Convert.ToInt32(strBus_gap));
                        cmd.ExecuteNonQuery(); // DB에 실질적으로 저장시킴
                        await this.ShowMessageAsync("성공", "버스 정보 DB 저장 성공!", MessageDialogStyle.Affirmative, null);
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        mainWindow.BtnBusInfor_Click(sender, e);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    await this.ShowMessageAsync("오류", $"DB 저장 오류 {ex.Message}", MessageDialogStyle.Affirmative, null);
                    return;
                }
            }
        }

        private void TxtBus_idx_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                insert_Click(sender, e);
            }
        }

        private void TxtBus_num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                insert_Click(sender, e);
            }
        }

        private void TxtBus_cnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                insert_Click(sender, e);
            }
        }

        private void TxtBus_gap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                insert_Click(sender, e);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TxtBus_idx.Focus();
        }
    }
}
