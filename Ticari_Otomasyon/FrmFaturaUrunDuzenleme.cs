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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        public string UrunID;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtURUNID.Text = UrunID;
            SqlCommand komut = new SqlCommand("select * from TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtURUNID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                TxtUrunAD.Text = dr[1].ToString();
                TxtMarka.Text = dr[2].ToString();
                TxtModel.Text = dr[3].ToString();
                TxtMiktar.Text = dr[4].ToString();
                TxtFiyat.Text = dr[5].ToString();
                TxtTutar.Text = dr[6].ToString();
                bgl.baglanti().Close();
            }
        }

        private void BtnFaturaGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURADETAY SET URUNAD=@P1,MARKA=@P2,MODEL=@P3,MIKTAR=@P4,FIYAT=@P5,TUTAR=@P6 WHERE FATURAURUNID=@P7", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunAD.Text);
            komut.Parameters.AddWithValue("@P2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@P3", TxtModel.Text);
            komut.Parameters.AddWithValue("@P4", TxtMiktar.Text);
            komut.Parameters.AddWithValue("@P5", decimal.Parse(TxtFiyat.Text));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@P7", TxtURUNID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnFaturaSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_FATURADETAY WHERE FATURAURUNID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("P1", TxtURUNID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Silme İşlemi Başarılı Bir Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
