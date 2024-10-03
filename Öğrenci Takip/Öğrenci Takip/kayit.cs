using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Öğrenci_Takip
{
    public partial class kayit : Form
    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MesafeYoklama.accdb");
        OleDbCommand kmt = new OleDbCommand();
        public kayit()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                label6.Text = sonuc;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void kayit_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = Form1.portismi;
            serialPort1.BaudRate = Convert.ToInt16(Form1.banthizi);

            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();
                    label7.Text = "Bağlantı Sağlandı";
                    label7.ForeColor = Color.Green;
                }
                catch
                {
                    label7.Text = "Bağlantı Sağlanamadı";

                }


            }
            else
            {

                label7.Text = "Bağlantı Sağlanamadı";
                label7.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "123123" || guna2TextBox1.Text == "" || guna2ComboBox1.Text == "seçiniz" || guna2ComboBox2.Text == "seçiniz")
            {
                label8.Text = "Bilgilerinizi Eksiksiz Giriniz";
                label8.ForeColor = Color.Red;

            }
            else
            {
                try
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO tablo(kid,isim,sinif,sube) VALUES ('" + label6.Text + "','" + guna2TextBox1.Text + "','" + guna2ComboBox1.Text + "','" + guna2ComboBox2.Text + "')";
                    kmt.ExecuteNonQuery();
                    label8.Text = "Kayıt Başarılı";
                    label8.ForeColor = Color.Green;



                    bag.Close();
                }
                catch (Exception )
                {

                    guna2MessageDialog1.Show("Hata");
                }




            }

        }

        private void button3_Click(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void kayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label6.Text = "__________";
            guna2TextBox1.Text = "";
            guna2ComboBox1.Text = "seçiniz";
            guna2ComboBox2.Text = "seçiniz";
            label8.Text = "";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "123123" || guna2TextBox1.Text == "" || guna2ComboBox1.Text == "seçiniz" || guna2ComboBox2.Text == "seçiniz")
            {
                label8.Text = "Bilgilerinizi Eksiksiz Giriniz";
                label8.ForeColor = Color.Red;

            }
            else
            {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO tablo(kid,isim,sinif,sube) VALUES ('" + label6.Text + "','" + guna2TextBox1.Text + "','" + guna2ComboBox1.Text + "','" + guna2ComboBox2.Text + "')";
                    kmt.ExecuteNonQuery();
                    label8.Text = "Kayıt Başarılı";
                    label8.ForeColor = Color.Green;

                    bag.Close();
                
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
