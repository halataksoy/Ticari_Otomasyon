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
            MailMessage mesajım = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("halat.aksoy2000@gmail.com", "whatthefuck5148");//kendi mail adresimiz
            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;//şifrelesin mi? evet şifrelesin
            mesajım.To.Add(TxtMailAdres.Text);//kime gönderecekse mesajı
            mesajım.From = new MailAddress("halat.aksoy2000@gmail.com");
            mesajım.Subject = TxtKonu.Text;
            mesajım.Body = RchMesaj.Text;
            istemci.Send(mesajım);
            
            //mail adresi oluştur
            //smtp araştır
            //#TODO
            //MAİL HATASINI ÇÖZ-------------------
        }
    }
}
