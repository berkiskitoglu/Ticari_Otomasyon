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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }


        SqlBaglantisi bgl = new SqlBaglantisi();
        void giderListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_GIDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            TxtDogalgaz.Text = " ";
            TxtEkstralar.Text = " ";
            TxtElektirik.Text = " ";
            TxtGiderID.Text = " ";
            TxtInternet.Text = " ";
            TxtMaaslar.Text = " ";
            TxtSu.Text = " ";
            CmbAy.Text = " ";
            CmbYIL.Text = " ";
            RchNotlar.Text = " ";
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderListesi();
            Temizle();
        }

        private void BtnGiderKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER(AY,YIL,ELEKTIRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", CmbAy.Text);
            komut.Parameters.AddWithValue("@P2", CmbYIL.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektirik.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstralar.Text));
            komut.Parameters.AddWithValue("@P9", RchNotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Başarılı Bir Şekilde Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderListesi();
            Temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {
                TxtGiderID.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYIL.Text = dr["YIL"].ToString();
                TxtElektirik.Text = dr["ELEKTIRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtInternet.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstralar.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnGiderSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("delete from TBL_GIDERLER WHERE ID=@P1", bgl.baglanti());
            komutSil.Parameters.AddWithValue("@P1", TxtGiderID.Text);
            komutSil.ExecuteNonQuery();
            bgl.baglanti().Close();
            giderListesi();
            MessageBox.Show("Gider Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Temizle();
        }

        private void BtnGiderGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("update TBL_GIDERLER SET AY=@P1,YIL=@P2,ELEKTIRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 WHERE ID=@P10", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@P1", CmbAy.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", CmbYIL.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektirik.Text));
            komutGuncelle.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komutGuncelle.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komutGuncelle.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komutGuncelle.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komutGuncelle.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstralar.Text));
            komutGuncelle.Parameters.AddWithValue("@P9", RchNotlar.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", TxtGiderID.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderListesi();
            Temizle();
        }
    }
}
