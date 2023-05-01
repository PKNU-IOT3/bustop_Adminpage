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
            try
            {
                using(MySqlConnection conn = new MySqlConnection(commons.myConnString))
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
                    cmd.Parameters.AddWithValue("@bus_num", Convert.ToInt32(strBus_num));
                    cmd.Parameters.AddWithValue("@bus_cnt", Convert.ToInt32(strBus_cnt));
                    cmd.Parameters.AddWithValue("@bus_gap", Convert.ToInt32(strBus_gap));
                    cmd.ExecuteNonQuery(); // DB에 실질적으로 저장시킴
                    await this.ShowMessageAsync("성공", "버스 정보 DB 저장 성공!", MessageDialogStyle.Affirmative, null);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"DB 저장 오류 {ex.Message}",MessageDialogStyle.Affirmative,null);
                return;
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
