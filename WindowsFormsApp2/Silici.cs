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
    public partial class Silici : Form
    {
        public Silici()
        {
            InitializeComponent();
            maskedTextBox1.Mask = "00:00:00";
            maskedTextBox1.ValidatingType = typeof(DateTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string silinecekSaat = maskedTextBox1.Text.Trim();
            string silinecekTarih = dateTimePicker1.Value.ToShortDateString().Trim();
            string silinecekIsim = textBox1.Text.Trim();

            // Normalize edilen aranan satır
            string arananSatir = NormalizeSatir($"Tarih: {silinecekTarih} Saat: {silinecekSaat} Alarm_ismi: {silinecekIsim}");
            MessageBox.Show($"Aranan Satır: '{arananSatir}'", "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string BilgisayarYolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string DosyaYolu = Path.Combine(BilgisayarYolu, @"233779008 - Fikret Çalkın\ajanda .txt");

            try
            {
                if (File.Exists(DosyaYolu))
                {
                    string[] satirlar = File.ReadAllLines(DosyaYolu, Encoding.UTF8);

                    bool satirBulundu = false;

                    foreach (string satir in satirlar)
                    {
                        // Okunan satır normalize edilir
                        string okunanSatir = NormalizeSatir(satir);
                        MessageBox.Show($"Dosyadan Okunan Satır: '{okunanSatir}'", "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Normalize edilmiş şekilde karşılaştır
                        if (string.Equals(okunanSatir, arananSatir, StringComparison.OrdinalIgnoreCase))
                        {
                            satirBulundu = true;
                            break;
                        }
                    }

                    if (satirBulundu)
                    {
                        // Kayıt silme işlemi
                        var yeniSatirlar = satirlar.Where(satir => !string.Equals(NormalizeSatir(satir), arananSatir, StringComparison.OrdinalIgnoreCase)).ToArray();
                        File.WriteAllLines(DosyaYolu, yeniSatirlar);
                        MessageBox.Show("Kayıt başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Satır normalizasyonu için yardımcı fonksiyon
            string NormalizeSatir(string input)
            {
                return input
                    .Trim()                       // Baştaki ve sondaki boşlukları temizler
                    .Replace("\t", " ")           // Tab karakterlerini boşlukla değiştirir
                    .Replace("  ", " ")           // Çift boşlukları tek boşluğa çevirir
                    .Replace("\r\n", "")          // CRLF (Windows satır sonu karakteri) kaldırır
                    .Replace("\n", "")            // LF (Unix satır sonu karakteri) kaldırır
                    .Replace("İ", "i")            // Türkçe büyük İ düzeltmesi
                    .Replace("ı", "i");           // Türkçe küçük ı düzeltmesi
            }


        }
    }
}
