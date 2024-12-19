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

namespace WindowsFormsApp2
{
    public partial class Oluşturucu : Form
    {
        public Oluşturucu()
        {
            InitializeComponent();

            maskedTextBox1.Mask = "00:00:00";
            maskedTextBox1.ValidatingType = typeof(DateTime);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            string selectedDate = dateTimePicker1.Value.ToShortDateString();
            string selectedTime = maskedTextBox1.Text;

            
            string[] timeParts = selectedTime.Split(':');

            if (timeParts.Length == 3 &&
                int.TryParse(timeParts[0], out int hour) && hour >= 0 && hour < 24 &&
                int.TryParse(timeParts[1], out int minute) && minute >= 0 && minute < 60 &&
                int.TryParse(timeParts[2], out int second) && second >= 0 && second < 60)
            {
                
                string dateTimeString = $"Tarih: {selectedDate}  Saat: {selectedTime} Alarm_ismi: {text}";

                string Bilgisayaryolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string Dosyayolu = Path.Combine(Bilgisayaryolu, @"233779008 - Fikret Çalkın\ajanda .txt");

                try
                {
                    File.AppendAllText(Dosyayolu, dateTimeString + Environment.NewLine);
                    MessageBox.Show("Tarih, saat ve Alarm ismi dosyaya başarıyla yazıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                
                MessageBox.Show("Hatalı giriş yaptınız. Lütfen saat formatına uygun bir değer girin (HH:MM:SS). Saat 00-23, dakika ve saniye 00-59 arası olmalıdır.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
    }
}
