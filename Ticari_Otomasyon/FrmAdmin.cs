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

namespace Ticari_Otomasyon
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = ColorTranslator.FromHtml("#8a00b8");
          
        }

        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = ColorTranslator.FromHtml("#c0c0ff"); 

        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_ADMIN WHERE KULLANICIAD=@P1 AND SIFRE=@P2", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmAnaModül fr = new FrmAnaModül();
                fr.kullanici = TxtKullaniciAd.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
