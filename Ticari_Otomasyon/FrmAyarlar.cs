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
using DevExpress.PivotGrid.OLAP.AdoWrappers;

namespace Ticari_Otomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }

        private void BtnIslem_Click(object sender, EventArgs e)
        {
            if (BtnIslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_ADMIN VALUES(@P1,@P2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
                komut.Parameters.AddWithValue("@P2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if(BtnIslem.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("UPDATE TBL_ADMIN SET SIFRE=@P2 WHERE KULLANICIAD=@P1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@P1", TxtKullaniciAd.Text);
                komut1.Parameters.AddWithValue("@P2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {
                TxtKullaniciAd.Text = dr["KULLANICIAD"].ToString();
                TxtSifre.Text = dr["SIFRE"].ToString();
            }
        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if(TxtKullaniciAd.Text != "" && gridView1.FocusedRowHandle >= 1)
            {
                BtnIslem.Text = "Güncelle";
                BtnIslem.BackColor = Color.GreenYellow;
            }
            else
            {
                BtnIslem.Text = "Kaydet";
                BtnIslem.BackColor = Color.Bisque;

            }
        }
    }
}
