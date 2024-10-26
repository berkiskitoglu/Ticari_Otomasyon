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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }


        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            TxtAlici.Text = "";
            TxtFaturaID.Text = "";
            TxtSeriNO.Text = "";
            TxtSıraNO.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
            TxtVergiDaire.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            Temizle();
        }

        private void BtnGiderKaydet_Click(object sender, EventArgs e)
        {
            if(TxtFaturaID2.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtSeriNO.Text);
                komut.Parameters.AddWithValue("@P2", TxtSıraNO.Text);
                komut.Parameters.AddWithValue("@P3", DateTime.Parse(MskTarih.Text));
                komut.Parameters.AddWithValue("@P4", MskSaat.Text);
                komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if(TxtFaturaID2.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MARKA,MODEL,MIKTAR,FIYAT,TUTAR,FATURAID) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", TxtUrunAD.Text);
                komut2.Parameters.AddWithValue("@P2", TxtMarka.Text);
                komut2.Parameters.AddWithValue("@P3", TxtModel.Text);
                komut2.Parameters.AddWithValue("@P4", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@P5", TxtFiyat.Text);
                komut2.Parameters.AddWithValue("@P6", TxtTutar.Text);
                komut2.Parameters.AddWithValue("@P7", TxtFaturaID2.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            TxtFaturaID.Text = dr["FATURABILGIID"].ToString();
            TxtSeriNO.Text = dr["SERI"].ToString();
            TxtSıraNO.Text = dr["SIRANO"].ToString();
            MskSaat.Text = dr["SAAT"].ToString();
            TxtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
            TxtAlici.Text = dr["ALICI"].ToString();
            TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
            TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();

            DateTime tarih;
            if (DateTime.TryParse(dr["TARIH"].ToString(), out tarih))
            {
                MskTarih.Text = tarih.ToString("dd.MM.yyyy"); // Set the formatted date
            }
            else
            {
                MskTarih.Text = "Invalid Date"; // Handle the case where the date is invalid
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnFaturaSil_Click(object sender, EventArgs e)
        {
          
                SqlCommand komut = new SqlCommand("delete From TBL_FATURABILGI WHERE FATURABILGIID = @p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtFaturaID.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
                listele();
            
        }

        private void BtnFaturaGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURABILGI SET SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE FATURABILGIID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeriNO.Text);
            komut.Parameters.AddWithValue("@P2", TxtSıraNO.Text);
            komut.Parameters.AddWithValue("@P3", DateTime.Parse( MskTarih.Text));
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@P9", TxtFaturaID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            
        }
        FrmFaturaUrunler fr;
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            fr = new FrmFaturaUrunler();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {

                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
