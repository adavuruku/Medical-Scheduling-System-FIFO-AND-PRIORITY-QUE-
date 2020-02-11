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
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {

        }

        private void viewPatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registerPatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.ShowDialog();
        }
        private void load_time_and_doctor()
        {

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
        private void MDIParent1_Load(object sender, EventArgs e)
        {
            load_time_and_doctor();
            
        }

        private void adjustScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            medical_Class sherif_system = new medical_Class();
            sherif_system.frequent_search();
            
        }

        private void diagnosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.ShowDialog();
        }
    }
}
