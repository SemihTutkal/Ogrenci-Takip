using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;
using Guna.UI2.WinForms;

namespace Öğrenci_Takip
{

    public partial class Form1 : Form
    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=MesafeYoklama.accdb");
        OleDbCommand kmt = new OleDbCommand();

        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Start();
            portismi = comboBox1.Text;
            banthizi = comboBox2.Text;
            try
            {
                serialPort1.PortName = portismi;
                serialPort1.BaudRate = Convert.ToInt16(banthizi);

                serialPort1.Open();
                label1.Text = "Bağlantı sağlandı";
                label1.ForeColor = Color.Green;
            }
            catch
            {
                serialPort1.Close();
                serialPort1.Open();
                MessageBox.Show("Bağlantı zaten açık");

            }





        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label1.Text = "Bağlantı kesildi";
                label1.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                label2.Text = sonuc;
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText="SELECT * FROM  tablo WHERE kid='"+ sonuc +"'";

                OleDbDataReader oku = kmt.ExecuteReader();

                if (oku.Read())
                {
                   
                    DateTime bugun = DateTime.Now;
                    label8.Text = oku["isim"].ToString();
                    label9.Text = oku["sinif"].ToString() + "/" + oku["sube"].ToString();
                    label10.Text = bugun.ToShortDateString();
                    label11.Text = bugun.ToShortTimeString();
                    bag.Close();

                    bag.Open();
                    kmt.CommandText="INSERT INTO zaman (isim,tarih,saat)VALUES('"+label8.Text+"','"+label10.Text+"','"+label11.Text+"')";
                    kmt.ExecuteReader();
                   bag.Close() ;
                }
                else
                {
                   
                    label2.Text = "Kart Kayıtlı Değil";
                    label2.ForeColor= Color.Red;
                    label8.Text = "__________";
                    label9.Text = "__________";
                    label10.Text = "__________";
                    label11.Text = "__________";
                }



                bag.Close();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (portismi == null || banthizi == null)
            {
                MessageBox.Show("Bağlantını kontrol et");
            }
            else
            {
                timer1.Stop();
                serialPort1.Close();
                label1.Text = "Bağalntı kapandı!";
                label1.ForeColor = Color.Red;

                kayit kyt = new kayit();
                kyt.ShowDialog();
            }
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {
            label2.Text = "__________";
            label8.Text = "__________";
            label9.Text = "__________";
            label10.Text = "__________";
            label11.Text = "__________";
        }

        private void guna2Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {

                guna2Button1.PerformClick();
               
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {

                guna2Button1.PerformClick();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }
                comboBox2.Items.Add("9600");
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;


                timer1.Start();
                portismi = comboBox1.Text;
                banthizi = comboBox2.Text;

                try
                {
                    serialPort1.PortName = portismi;
                    serialPort1.BaudRate = Convert.ToInt32(banthizi);

                    serialPort1.Open();
                    label1.Text = "Bağlandı";
                    label1.ForeColor = Color.Green;

                }
                catch
                {
                    serialPort1.Close();
                    serialPort1.Open();
                    guna2MessageDialog2.Show("Bağlantı zaten açık");
                }

            }
            catch (Exception)
            {
                guna2MessageDialog1.Show("Kart okuyucu takılı değill!!!");
            }

        }
    }
}
