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
    public partial class Form3 : Form
    {
        MySqlCommand sCommand;
        MySqlDataAdapter sAdapter;
        MySqlCommandBuilder sBuilder;
        DataSet sDs;
        DataTable sTable;
        DataView dv;
        public Form3()
        {
            InitializeComponent();
        }
        private void load_doctor_patience_list()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(ConString);
            con = new MySqlConnection(ConString);
            string sql = "";
            DateTime dt2 = DateTime.Now;
            string status = "0";
            string curdate = dt2.ToString("yyyy/MM/dd");
            sql = "SELECT id As 'SNo', patience_Name As 'Patiece Name',time_Schedule As 'Time Schedule', complain As 'Complain', date_Reg As 'Date Registered', date_Schedule As 'Scheduled Date',emergency As 'Emmergency Required', period As 'Period', status As 'Clearance Status' FROM patient_record where Period = '" + medical_Class.session_Period + "' AND status = '" + status + "' And date_Schedule between '" + curdate + "' AND '" + curdate + "' order by emergency desc";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            sCommand = new MySqlCommand(sql, con);
            sAdapter = new MySqlDataAdapter(sCommand);
            sBuilder = new MySqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            dv = new DataView();
            sAdapter.Fill(sDs, "patient_record");
            sTable = sDs.Tables["patient_record"];
            dv.Table = sDs.Tables["patient_record"];
            con.Close();
            dataGridView1.DataSource = dv;
        }
        private void shift_patient_period(string pp, string backs) {
           // MessageBox.Show(pp);
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            //check if all mornin was cleared by doctor
            MySqlConnection con22 = new MySqlConnection(ConString);
            if (con22.State == ConnectionState.Open)
            {
                con22.Close();
            }
            con22.Open();
            string stat = "0";
            DateTime hh = DateTime.Now;
            string curdate_ch = hh.ToString("yyyy/MM/dd");
            string query22 = "SELECT * FROM patient_record where (date_Schedule between '" + curdate_ch + "' AND '" + curdate_ch + "') AND (status ='" + stat +"'AND period ='" + pp + "')";
            MySqlCommand sqlCmd22 = new MySqlCommand(query22, con22);
            MySqlDataReader sqlReader22 = sqlCmd22.ExecuteReader();
            int count = 0;
            while (sqlReader22.Read())
            {
                count = count + 1;
            }
            if (count >=1)
            {
                //MessageBox.Show("kk");
                MySqlConnection con22A = new MySqlConnection(ConString);
                if (con22A.State == ConnectionState.Open)
                {
                    con22A.Close();
                }
                con22A.Open();
                stat = "0";
                
                string query22A = "SELECT * FROM patient_record where status ='"+ stat +"'";
                MySqlCommand sqlCmd22A = new MySqlCommand(query22A, con22A);
                MySqlDataReader sqlReader22A = sqlCmd22A.ExecuteReader();
                int count_two = 0;
                string save_date=""; 
                string change_curdate="";
                while (sqlReader22A.Read())
                {
                    //update
                    string sherif = sqlReader22A["id"].ToString();
                    DateTime dt2 = Convert.ToDateTime(sqlReader22A["date_Schedule"].ToString());
                    string curdate_here = dt2.ToString("yyyy/MM/dd");

                    if (change_curdate == "")
                    {
                        save_date = curdate_here;
                    }
                    else
                    {
                        save_date = change_curdate;
                        curdate_here = change_curdate;
                        //MessageBox.Show(save_date);
                    }
                    //MessageBox.Show(sherif);
                    //MessageBox.Show(backs);
                    MySqlConnection con22o0= new MySqlConnection(ConString);
                    if (con22.State == ConnectionState.Open)
                    {
                        con22o0.Close();
                    }
                    con22o0.Open();
                    MySqlCommand sav200 = new MySqlCommand();
                    sav200.CommandText = "Update patient_record set period = @period,date_Schedule=@date_Schedule Where id=@id and status =@status Limit 1";
                    sav200.Connection = con22o0;
                    sav200.Parameters.Clear();
                    sav200.Parameters.AddWithValue("@period", backs);
                   // MessageBox.Show(save_date);
                    sav200.Parameters.AddWithValue("@date_Schedule", save_date);
                    sav200.Parameters.AddWithValue("@id", sherif);
                    sav200.Parameters.AddWithValue("@status", stat);
                    int res20 = sav200.ExecuteNonQuery();
                    count_two = count_two + 1;
                    if (count_two >= 4)
                    {
                        if (backs == "Morning")
                        {
                            backs = "Afternoon";
                        }
                        else
                        {
                            if (backs == "Afternoon")
                            {
                                backs = "Evening";
                            }else{
                                if (backs == "Evening")
                                {
                                    backs = "Morning";
                                    DateTime dt = Convert.ToDateTime(curdate_here);
                                    change_curdate = dt.AddDays(1).ToString("yyyy/MM/dd");
                                   // string curdate_here = dt2.ToString("yyyy/MM/dd");
                                   // MessageBox.Show(change_curdate);
                                }
                            }

                        }
                      
                        count_two = 0;
                    }

                    
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string me = medical_Class.session_Period;
            medical_Class sherif_system = new medical_Class();
            sherif_system.frequent_search();

            label2.Text = DateTime.Now.ToLongDateString() + " - " + medical_Class.session_Period + " Session";
            label1.Text = "Doctor " + medical_Class.begin_name + " - List Of All Patience to attend to for the Day";
            if (medical_Class.session_Period != me)
            {
                shift_patient_period(me, medical_Class.session_Period);
            }
            load_doctor_patience_list();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(" ");
            medical_Class sherif_system = new medical_Class();
            sherif_system.frequent_search();
            label2.Text = DateTime.Now.ToLongDateString() + " - " + medical_Class.session_Period + " Session";
            label1.Text = "Doctor " + medical_Class.begin_name + " - List Of All Patience to attend to for the Day";

            load_doctor_patience_list();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int f = e.RowIndex;
            int s = e.ColumnIndex;
            if ((f < 0))
            {
                return;
            }
            else
            {

               string search = string.Empty;
               medical_Class.doctor_search = dataGridView1.Rows[e.RowIndex].Cells["SNo"].Value.ToString();
               string pat_name = dataGridView1.Rows[e.RowIndex].Cells["Patiece Name"].Value.ToString();
              DialogResult H = MessageBox.Show("Do you want to Clear this patient of Treatment ...\nPatient Name : "+ pat_name +" \nTo Continue with the Clearance process Click Yes to stop Click No", "Ptience Report Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
              if (H == DialogResult.No)
              {
                  return;
              }
              else
              {
                  string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                  MySqlConnection con2 = new MySqlConnection(ConString);
                  if (con2.State == ConnectionState.Open)
                  {
                      con2.Close();
                  }
                  con2.Open();
                  MySqlCommand sav3 = new MySqlCommand();
                  sav3.CommandText = "Update patient_record set status = @status where id= @id";
                  sav3.Connection = con2;
                  sav3.Parameters.Clear();

                  sav3.Parameters.AddWithValue("@status", "1");
                  sav3.Parameters.AddWithValue("@id", medical_Class.doctor_search);
                  int res2 = sav3.ExecuteNonQuery();
              }
               
            }
        }
    }
}
