using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Düzenleyici : Form
    {
        public Düzenleyici()
        {
            InitializeComponent();
            maskedTextBox1.Mask = "00:00:00";
            maskedTextBox1.ValidatingType = typeof(DateTime);
            maskedTextBox2.Mask = "00:00:00";
            maskedTextBox2.ValidatingType = typeof(DateTime);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string Bilgisayaryolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string Dosyayolu = Path.Combine(Bilgisayaryolu, @"233779008 - Fikret Çalkın\ajanda .txt");
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string eskiSaat = maskedTextBox1.Text; 
            string yeniSaat = maskedTextBox2.Text; 

            string eskiTarih = dateTimePicker1.Value.ToShortDateString(); 
            string yeniTarih = dateTimePicker2.Value.ToShortDateString(); 

            string eskiIsim = textBox1.Text; 
            string yeniIsim = textBox2.Text; 

            string BilgisayarYolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string DosyaYolu = Path.Combine(BilgisayarYolu, @"233779008 - Fikret Çalkın\ajanda .txt");

            try
            {
                if (File.Exists(DosyaYolu))
                {
                    string[] satirlar = File.ReadAllLines(DosyaYolu);

                    bool guncellendi = false;

                    for (int i = 0; i < satirlar.Length; i++)
                    {
                        if (satirlar[i].Contains($"Tarih: {eskiTarih}") &&
                            satirlar[i].Contains($"Saat: {eskiSaat}") &&
                            satirlar[i].Contains($"Alarm_ismi: {eskiIsim}"))
                        {
                            satirlar[i] = $"Tarih: {yeniTarih}  Saat: {yeniSaat}  Alarm_ismi: {yeniIsim}";
                            guncellendi = true;
                            break;
                        }
                    }

                    if (guncellendi)
                    {
                        File.WriteAllLines(DosyaYolu, satirlar);
                        MessageBox.Show("Kayıt başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen kayıt bulunamadı. Lütfen bilgileri kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Dosya bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
