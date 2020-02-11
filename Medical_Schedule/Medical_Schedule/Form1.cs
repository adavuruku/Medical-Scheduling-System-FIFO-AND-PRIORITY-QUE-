using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Medical_Schedule
{
    public partial class Form1 : Form
    {
        MySqlCommand sCommand;
        MySqlDataAdapter sAdapter;
        MySqlCommandBuilder sBuilder;
        DataSet sDs = new DataSet();
        DataTable sTable;
        DataView dv;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        public void load_grid() {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(ConString);
            con = new MySqlConnection(ConString);
            string sql = "";

            sql = "SELECT id As 'SNo',doc_Name As 'Doctor Name', doc_Period As 'Duty Period', Start_Time As 'Start Time', End_Time As 'End Time' FROM doctors";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            sCommand = new MySqlCommand(sql, con);
            sAdapter = new MySqlDataAdapter(sCommand);
            sBuilder = new MySqlCommandBuilder(sAdapter);
            dv = new DataView();
            sAdapter.Fill(sDs, "doctors");
            sTable = sDs.Tables["doctors"];
            dv.Table = sDs.Tables["doctors"];
            con.Close();
            dataGridView1.DataSource = dv;
        }

        private void load_time_and_doctor() {

            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(ConString);
            con = new MySqlConnection(ConString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
           

            MySqlCommand sqlCmd2 = new MySqlCommand("SELECT * FROM doctors", con);

            //  con.Open();
            MySqlDataReader sqlReader2 = sqlCmd2.ExecuteReader();

            while (sqlReader2.Read())
            {
                if (sqlReader2["doc_Period"].ToString() == "Morning")
                {
                    medical_Class.morn_begin_time = sqlReader2["Start_Time"].ToString();
                    medical_Class.morn_end_time = sqlReader2["End_Time"].ToString();
                    medical_Class.morn_begin_name = sqlReader2["doc_Name"].ToString();
                }
                if (sqlReader2["doc_Period"].ToString() == "Afternoon")
                {
                    medical_Class.aft_begin_time = sqlReader2["Start_Time"].ToString();
                    medical_Class.aft_end_time = sqlReader2["End_Time"].ToString();
                    medical_Class.aft_begin_name = sqlReader2["doc_Name"].ToString();
                }
                if (sqlReader2["doc_Period"].ToString() == "Evening")
                {
                    medical_Class.eve_begin_time = sqlReader2["Start_Time"].ToString();
                    medical_Class.eve_end_time = sqlReader2["End_Time"].ToString();
                    medical_Class.eve_begin_name = sqlReader2["doc_Name"].ToString();
                }

            }
            sqlReader2.Close();
            con.Close();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            load_time_and_doctor();
           // MessageBox.Show(" ");
            load_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(ConString);
            //con = new MySqlConnection(ConString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            sAdapter.Update(sTable);
            MessageBox.Show("Record was succesfully Updated ...", "Process Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /**string curdate = DateTime.Now.ToShortDateString();
            //TimeSpan no = 1;
           // DateTime curdate2 = Convert.ToDateTime(curdate).Add(no);
           // curdate = curdate2.ToShortDateString();
           // DateTime last_time = Convert.ToDateTime(sqlReader2["period"].ToString());
          //  MessageBox.Show(" ");
           
            DateTime last_time = DateTime.Now;
            string last_time2 = last_time.AddMilliseconds(864000).ToString();
            string[] morning = new string[2]{
                "Sherif", "Sam"
            };
            MessageBox.Show(last_time2);
            DateTime dt = DateTime.Now;
            string curdate = dt.AddDays(1).ToString("yyyy/MM/dd");
            MessageBox.Show(morning[0]);**/
        }
    }
}
