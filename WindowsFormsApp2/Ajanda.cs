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
using System.Media;
using System.Linq;

namespace WindowsFormsApp2
{
    public partial class Ajanda : Form
    {
        private SoundPlayer alarmPlayer;
        private string[] satirlar;
        public Ajanda()
        {
            InitializeComponent();
        }
        private Oluşturucu Form2;

        private Düzenleyici Form3;

        private Silici Form4;


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            // Masaüstü dosya yolu
            string Bilgisayaryolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string Dosyayolu = Path.Combine(Bilgisayaryolu, @"233779008 - Fikret Çalkın\ajanda .txt");

            // Dosya var ise içeriğini yükle
            if (File.Exists(Dosyayolu))
            {
                satirlar = File.ReadAllLines(Dosyayolu); // Tüm satırları oku
                richTextBox1.Text = File.ReadAllText(Dosyayolu);
            }

            // Alarm sesi dosyasını yükle (örnek: alarm.wav)
            string sesDosyaYolu = Path.Combine(Bilgisayaryolu, @"233779008 - Fikret Çalkın\ses.wav");
            if (File.Exists(sesDosyaYolu))
            {
                alarmPlayer = new SoundPlayer(sesDosyaYolu);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = string.Format("{0:dd.MM.yyyy}", DateTime.Now);

            label2.Text = string.Format("{0:HH:mm:ss}", DateTime.Now);

            // Alarm kontrolü
            AlarmKontrol();
        }

        private void AlarmKontrol()
        {
            if (satirlar == null) return; // Eğer dosya boş ise kontrol etme

            string simdikiTarih = DateTime.Now.ToString("dd.MM.yyyy");
            string simdikiSaat = DateTime.Now.ToString("HH:mm:ss");

            foreach (string satir in satirlar)
            {
                if (satir.Contains($"Tarih: {simdikiTarih}") && satir.Contains($"Saat: {simdikiSaat}"))
                {
                    // Alarm sesi çal
                    if (alarmPlayer != null)
                    {
                        alarmPlayer.Play();
                        MessageBox.Show("Alarm zamanı geldi!", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Alarm sesi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Eğer richTextBox güncellenirse dosyayı yeniden yaz
            string Bilgisayaryolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string Dosyayolu = Path.Combine(Bilgisayaryolu, @"233779008 - Fikret Çalkın\ajanda .txt");

            File.WriteAllText(Dosyayolu, richTextBox1.Text);
            satirlar = File.ReadAllLines(Dosyayolu); // Satırları güncelle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2 == null || Form2.IsDisposed)
            {
                Form2 = new Oluşturucu();
                Form2.FormClosed += (s, args) => Form2 = null;
            }

            Form2.Show();
            Form2.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form3 == null || Form3.IsDisposed)
            {
                Form3 = new Düzenleyici();
                Form3.FormClosed += (s, args) => Form3 = null;
            }

            Form3.Show();
            Form3.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form4 == null || Form4.IsDisposed)
            {
                Form4 = new Silici();
                Form4.FormClosed += (s, args) => Form4 = null;
            }

            Form4.Show();
            Form4.BringToFront();
        }
    }
}
