using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }

        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            TxtMailAdres.Text = mail;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesaj = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("iskitoglu.berk@gmail.com", "zzhc fdcw ztjb urcz\r\n");
            istemci.Host = "smtp.gmail.com";
            istemci.Port = 587;
            istemci.EnableSsl = true;
            mesaj.To.Add(TxtMailAdres.Text);
            mesaj.From = new MailAddress("iskitoglu.berk@gmail.com");
            mesaj.Subject = TxtKonu.Text;
            mesaj.Body = RchMesaj.Text;
            istemci.Send(mesaj);
            MessageBox.Show("Mail Başarılı Bir Şekilde Gönderildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
