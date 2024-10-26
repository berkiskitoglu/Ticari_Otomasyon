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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.DirectXPaint;

namespace Ticari_Otomasyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void FirmaListe()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FIRMALAR",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
                TxtAd.Focus();
                TxtFirmaID.Text = "";
                TxtFirmaAd.Text = "";
                TxtYetkili.Text = "";
                TxtYetkiliStatu.Text = "";
                MskYetkiliTC.Text = "";
                TxtSektor.Text = "";
                MskFirmaTel1.Text = "";
                MskFirmaTel2.Text = "";
                MskFirmaTel3.Text = "";
                TxtYetkiliMail.Text = "";
                MskFax.Text = "";
                CmbFirmaIL.Text = "";
                CmbFirmaILCE.Text = "";
                TxtFirmaVergiDaire.Text = "";
                RchFirmaAdres.Text = "";
                TxtKod1.Text = "";
                TxtKod2.Text = "";
                TxtKod3.Text = "";
            

        }

         void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbFirmaIL.Properties.Items.Add(dr[0]);

            }
            bgl.baglanti().Close();
        }
        void carikodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            FirmaListe();
            sehirListesi();
            carikodAciklamalar();
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {

                TxtFirmaID.Text = dr["ID"].ToString();
                TxtFirmaAd.Text = dr["AD"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                TxtYetkiliStatu.Text = dr["YETKILISTATU"].ToString();
                MskYetkiliTC.Text = dr["YETKILITC"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskFirmaTel1.Text = dr["TELEFON1"].ToString();
                MskFirmaTel2.Text = dr["TELEFON2"].ToString();
                MskFirmaTel3.Text = dr["TELEFON3"].ToString();
                TxtYetkiliMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                CmbFirmaIL.Text = dr["IL"].ToString();
                CmbFirmaILCE.Text = dr["ILCE"].ToString();
                TxtFirmaVergiDaire.Text = dr["VERGIDAIRE"].ToString();
                RchFirmaAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void BtnFirmaKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILIADSOYAD,YETKILISTATU,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtFirmaAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkiliStatu.Text);
            komut.Parameters.AddWithValue("@p4", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskFirmaTel1.Text);
            komut.Parameters.AddWithValue("@p7", MskFirmaTel2.Text);
            komut.Parameters.AddWithValue("@p8", MskFirmaTel3.Text);
            komut.Parameters.AddWithValue("@p9", TxtYetkiliMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", CmbFirmaIL.Text);
            komut.Parameters.AddWithValue("@p12", CmbFirmaILCE.Text);
            komut.Parameters.AddWithValue("@p13", TxtFirmaVergiDaire.Text);
            komut.Parameters.AddWithValue("@p14", RchFirmaAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FirmaListe();
            Temizle();
        }

      
        private void CmbFirmaIL_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbFirmaILCE.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbFirmaIL.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbFirmaILCE.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnFirmaSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_FIRMALAR WHERE ID = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtFirmaID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            FirmaListe();
            MessageBox.Show("Firma Listeden Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Hand);
        }

        private void BtnFirmaGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FIRMALAR SET AD=@P1,YETKILIADSOYAD=@P2,YETKILISTATU=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,FAX=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@p17 where ID=@P18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtFirmaAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkiliStatu.Text);
            komut.Parameters.AddWithValue("@p4", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskFirmaTel1.Text);
            komut.Parameters.AddWithValue("@p7", MskFirmaTel2.Text);
            komut.Parameters.AddWithValue("@p8", MskFirmaTel3.Text);
            komut.Parameters.AddWithValue("@p9", TxtYetkiliMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", CmbFirmaIL.Text);
            komut.Parameters.AddWithValue("@p12", CmbFirmaILCE.Text);
            komut.Parameters.AddWithValue("@p13", TxtFirmaVergiDaire.Text);
            komut.Parameters.AddWithValue("@p14", RchFirmaAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.Parameters.AddWithValue("@p18", TxtFirmaID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FirmaListe();
            Temizle();
        }
    }
}
