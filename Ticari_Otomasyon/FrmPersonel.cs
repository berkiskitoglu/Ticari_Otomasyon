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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void personelListe()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_PERSONELLER", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            TxtPersonelID.Focus();
            TxtPersonelID.Text = " ";
            TxtPersonelAd.Text = " ";
            TxtPersonelSoyad.Text = " ";
            MskPersonelTel.Text = " ";
            MskPersonelTC.Text = " ";
            TxtPersonelMail.Text = " ";
            CmbPersonelIL.Text = " ";
            CmbPersonelILCE.Text = " ";
            RchPersonelAdres.Text = " ";
            TxtPersonelGorev.Text = " ";
        }
        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelListe();
            sehirListesi();
            temizle();
        }
        public void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbPersonelIL.Properties.Items.Add(dr[0]);

            }
            bgl.baglanti().Close();
        }

        private void CmbPersonelIL_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbPersonelILCE.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbPersonelIL.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbPersonelILCE.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_PERSONELLER(AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtPersonelAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtPersonelSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskPersonelTel.Text);
            komut.Parameters.AddWithValue("@P4", MskPersonelTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtPersonelMail.Text);
            komut.Parameters.AddWithValue("@P6", CmbPersonelIL.Text);
            komut.Parameters.AddWithValue("@P7", CmbPersonelILCE.Text);
            komut.Parameters.AddWithValue("@P8", RchPersonelAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtPersonelGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Kaydı Başarılı Bir Şekilde Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelListe();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtPersonelID.Text = dr["ID"].ToString();
                TxtPersonelAd.Text = dr["AD"].ToString();
                TxtPersonelSoyad.Text = dr["SOYAD"].ToString();
                MskPersonelTel.Text = dr["TELEFON"].ToString();
                MskPersonelTC.Text = dr["TC"].ToString();
                TxtPersonelMail.Text = dr["MAIL"].ToString();
                CmbPersonelIL.Text = dr["IL"].ToString();
                CmbPersonelILCE.Text = dr["ILCE"].ToString();
                RchPersonelAdres.Text = dr["ADRES"].ToString();
                TxtPersonelGorev.Text = dr["GOREV"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("delete from TBL_PERSONELLER WHERE ID=@P1", bgl.baglanti());
            komutSil.Parameters.AddWithValue("@P1", TxtPersonelID.Text);
            komutSil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            personelListe();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_PERSONELLER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, TC=@P4, MAIL=@P5, IL=@P6, ILCE=@P7, ADRES=@P8, GOREV=@P9 WHERE ID=@P10", bgl.baglanti());

            // Parametreler ekleniyor
            komut.Parameters.AddWithValue("@P1", TxtPersonelAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtPersonelSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskPersonelTel.Text);
            komut.Parameters.AddWithValue("@P4", MskPersonelTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtPersonelMail.Text);
            komut.Parameters.AddWithValue("@P6", CmbPersonelIL.Text);
            komut.Parameters.AddWithValue("@P7", CmbPersonelILCE.Text);
            komut.Parameters.AddWithValue("@P8", RchPersonelAdres.Text); 
            komut.Parameters.AddWithValue("@P9", TxtPersonelGorev.Text);
            komut.Parameters.AddWithValue("@P10", TxtPersonelID.Text);

            komut.ExecuteNonQuery();
            MessageBox.Show("Personel Bilgileri Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelListe();
        }
    }
}


