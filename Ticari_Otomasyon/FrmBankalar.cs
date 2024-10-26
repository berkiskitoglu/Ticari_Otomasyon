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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void BankaListe()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE BANKABILGILERI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbBankaIL.Properties.Items.Add(dr[0]);

            }
            bgl.baglanti().Close();
        }

        private void CmbIL_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbBankaILCE.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBankaIL.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbBankaILCE.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void FirmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.NullText = "Bir Firma Seçin";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }
        void Temizle()
        {
            TxtBankaAd.Text = " ";
            TxtBankaID.Text = " ";
            TxtHesapTUR.Text = " ";
            TxtHesapNo.Text = " ";
            TxtSube.Text = " ";
            TxtYetkili.Text = " ";
            CmbBankaIL.Text = " ";
            CmbBankaILCE.Text = " ";
            MskTelefon.Text = " ";
            MskTarih.Text = " ";
            MskIBAN.Text = " ";
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            BankaListe();
            sehirListesi();
            FirmaListesi();
            Temizle();
        }
      
        private void BtnGiderKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@P2", CmbBankaIL.Text);
            komut.Parameters.AddWithValue("@P3", CmbBankaILCE.Text);
            komut.Parameters.AddWithValue("@P4", TxtSube.Text);
            komut.Parameters.AddWithValue("@P5", MskIBAN.Text);
            komut.Parameters.AddWithValue("@P6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P9",  DateTime.Parse( MskTarih.Text));
            komut.Parameters.AddWithValue("@P10", TxtHesapTUR.Text);
            komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BankaListe();
            Temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            TxtBankaID.Text = dr["ID"].ToString();
            TxtBankaAd.Text = dr["BANKAADI"].ToString();
            CmbBankaIL.Text = dr["IL"].ToString();
            CmbBankaILCE.Text = dr["ILCE"].ToString();
            TxtSube.Text = dr["SUBE"].ToString();
            MskIBAN.Text = dr["IBAN"].ToString();
            TxtHesapNo.Text = dr["HESAPNO"].ToString();
            TxtYetkili.Text = dr["YETKILI"].ToString();
            MskTelefon.Text = dr["TELEFON"].ToString();
            TxtHesapTUR.Text = dr["HESAPTURU"].ToString();
            //lookUpEdit1.Text = dr["FIRMAID"].ToString();
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

        private void BtnGiderSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_BANKALAR where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Temizle();
            MessageBox.Show("Banka Bilgisi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BankaListe();
        }

        private void BtnGiderGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_BANKALAR SET BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10 WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@P2", CmbBankaIL.Text);
            komut.Parameters.AddWithValue("@P3", CmbBankaILCE.Text);
            komut.Parameters.AddWithValue("@P4", TxtSube.Text);
            komut.Parameters.AddWithValue("@P5", MskIBAN.Text);
            komut.Parameters.AddWithValue("@P6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P9", DateTime.Parse(MskTarih.Text));
            komut.Parameters.AddWithValue("@P10", TxtHesapTUR.Text);
            komut.Parameters.AddWithValue("@P11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@P12", TxtBankaID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            BankaListe();
            Temizle();
        }
    }
}
