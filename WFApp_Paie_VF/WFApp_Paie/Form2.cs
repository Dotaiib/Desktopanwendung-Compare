using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WFApp_Paie
{
    public partial class Form2 : Form
    {
        SqlConnection sc = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Paie ; integrated security = false; User ID = S_Paie; Password = S_Paie0823;");

        public Form2()
        {
            InitializeComponent();
        }

        void Afficher_vrai()
        {
            dataGridView1.Rows.Clear();
            sc.Open();
            SqlCommand cmd = new SqlCommand("select cibel_liste.matricule, cibel_liste.name, right(cibel_liste.persnl_time, 8) [time], archiv_liste.matricule, archiv_liste.name, right(archiv_liste.persnl_time, 8) [time] from archiv_liste, cibel_liste where archiv_liste.matricule = cibel_liste.matricule and cibel_liste.value='"+textBox1.Text+"' and archiv_liste.value= '"+textBox1.Text+"' ", sc);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5));
            }
            sc.Close();
        }

        void Afficher_faux()
        {
            dataGridView1.Rows.Clear();
            sc.Open();
            SqlCommand cmd = new SqlCommand("select cibel_liste.matricule, cibel_liste.name, right(cibel_liste.persnl_time, 8) [time] from cibel_liste where cibel_liste.matricule not in (select archiv_liste.matricule from archiv_liste where archiv_liste.value='"+textBox1.Text+"') and cibel_liste.value='"+textBox1.Text+"' ", sc);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2));
            }
            sc.Close();
        }

        void ExportTOExcel(DataGridView dataGridView1)
        {

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //add data 
            int StartCol = 1;
            int StartRow = 1;
            int j = 0, i = 0;

            //Write Headers
            for (j = 0; j < dataGridView1.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView1.Columns[j].HeaderText;
            }

            StartRow++;

            //Write datagridview content
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    try
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    }
                    catch
                    {
                        ;
                    }
                }
            }

            //For Diagramme :

            //Microsoft.Office.Interop.Excel.Range chartRange;

            //Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            //Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            //Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

            //chartRange = xlWorkSheet.get_Range("A1", "B" + dataGridView1.Rows.Count);
            //chartPage.SetSourceData(chartRange, misValue);
            //chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;

            xlApp.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExportTOExcel(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Afficher_vrai();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Afficher_faux();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Txtchanged_box2(object sender, EventArgs e)
        {
            //Textbox
            if (textBox1.Text != "") { button1.Enabled = true; button2.Enabled = true; }
            else { button1.Enabled = false; button2.Enabled = false; }


            sc.Open();
            SqlCommand cmd = new SqlCommand("select distinct cibel_liste.value from cibel_liste", sc);
            SqlDataReader dr = cmd.ExecuteReader();
            AutoCompleteStringCollection autodata = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                autodata.Add(dr.GetString(0));
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = autodata;
            sc.Close();
        }
    }
}
