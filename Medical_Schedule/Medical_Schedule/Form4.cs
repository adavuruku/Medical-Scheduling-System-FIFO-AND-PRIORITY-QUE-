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
    public partial class Form4 : Form
    {
        
        int no_p = 0;
        string[] symptoms_all = new string[20];
        public Form4()
        {
            InitializeComponent();
        }
        private void get_all_the_values()
        {
            
            if (txt1.Checked == true)
            {
                symptoms_all.SetValue(txt1.Text, no_p);
                no_p = no_p + 1; 
            }
            if (txt2.Checked == true)
            {
                symptoms_all.SetValue(txt2.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox2.Checked == true)
            {
                symptoms_all.SetValue(checkBox2.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox3.Checked == true)
            {
                symptoms_all.SetValue(checkBox3.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox4.Checked == true)
            {
                symptoms_all.SetValue(checkBox4.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox5.Checked == true)
            {
                symptoms_all.SetValue(checkBox5.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox6.Checked == true)
            {
                symptoms_all.SetValue(checkBox6.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox7.Checked == true)
            {
                symptoms_all.SetValue(checkBox7.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox8.Checked == true)
            {
                symptoms_all.SetValue(checkBox8.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox9.Checked == true)
            {
                symptoms_all.SetValue(checkBox9.Text, no_p);
                no_p = no_p + 1;
            } 
            if (checkBox10.Checked == true)
            {
                symptoms_all.SetValue(checkBox10.Text, no_p);
                no_p = no_p + 1;
            } 
            if (checkBox11.Checked == true)
            {
                symptoms_all.SetValue(checkBox11.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox12.Checked == true)
            {
                symptoms_all.SetValue(checkBox12.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox13.Checked == true)
            {
                symptoms_all.SetValue(checkBox13.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox14.Checked == true)
            {
                symptoms_all.SetValue(checkBox14.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox15.Checked == true)
            {
                symptoms_all.SetValue(checkBox15.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox16.Checked == true)
            {
                symptoms_all.SetValue(checkBox16.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox17.Checked == true)
            {
                symptoms_all.SetValue(checkBox17.Text, no_p);
                no_p = no_p + 1;
            }
            
            if (checkBox19.Checked == true)
            {
                symptoms_all.SetValue(checkBox19.Text, no_p);
                no_p = no_p + 1;
            }
            if (checkBox20.Checked == true)
            {
                symptoms_all.SetValue(checkBox20.Text, no_p);
                no_p = no_p + 1;
            }
            
    }
        private void saveRecord_Click(object sender, EventArgs e)
        {
           get_all_the_values();
           int malaria_count = 0;
           int typhoid_count = 0;
           int hiv_count = 0;
           
           string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
           //int le =  symptoms_all.Length;
           for (int i = 0; i < no_p; i++)
           {
              // MessageBox.Show(symptoms_all[i]);
                string search = symptoms_all[i];
                MySqlConnection con = new MySqlConnection(ConString);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string query = "SELECT * FROM expert_record where symptoms ='" + search + "'";
                MySqlCommand sqlCmd2 = new MySqlCommand(query, con);
                MySqlDataReader sqlReader2 = sqlCmd2.ExecuteReader();
                while (sqlReader2.Read())
                {
                    string disease = sqlReader2["disease"].ToString();
                    if (disease =="Malaria")
                    {
                        malaria_count = malaria_count + 1;
                    }
                    if (disease == "Typhoid Fever")
                    {
                        typhoid_count = typhoid_count + 1;
                    }
                    if (disease == "HIV")
                    {
                        hiv_count = hiv_count + 1;
                    }
                }
           }
            //find their total and take percentile
           int total_re = hiv_count + typhoid_count + malaria_count;
           
           if (total_re > 0)
           {
             //  MessageBox.Show(total_re.ToString() + "total");
               double percent_hiv = Math.Round((((double)hiv_count / (double)total_re) * 100), 2);
               double percent_malaria = Math.Round((((double)malaria_count / (double)total_re) * 100), 2);
               double percent_typhoid = Math.Round((((double)typhoid_count / (double)total_re) * 100),2);
               result.Text = "The Result for Diagnosis : HIV = " + percent_hiv.ToString() + " % , " + "Malaria = " + percent_malaria.ToString() + " %, " + "Typhoid = " + percent_typhoid.ToString() + " % .";
           }else{
           result.Text ="Please Select a Syptoms";
           }
          
        }

        
    }
}
