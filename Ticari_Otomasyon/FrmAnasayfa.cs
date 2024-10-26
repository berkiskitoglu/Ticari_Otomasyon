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
using DevExpress.DirectX.Common.Direct2D;
using EO.WebBrowser;
using DevExpress.XtraReports.Design;
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD,SUM(ADET) AS 'Adet' FROM TBL_URUNLER GROUP BY URUNAD HAVING SUM(ADET)<=20 ORDER BY SUM(ADET)", bgl.baglanti());
            da.Fill(dt);
            GridControlStoklar.DataSource = dt;
        }
        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 13 TARIH,SAAT,BASLIK FROM TBL_NOTLAR ORDER BY ID DESC",bgl.baglanti());
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;
        }

        void firmaHareketler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 13 URUNAD,TBL_FIRMAHAREKETLER.MODEL,TBL_FIRMAHAREKETLER.ADET,(TBL_PERSONELLER.AD + ' ' + SOYAD) AS 'PERSONEL',TBL_FIRMALAR.AD , FIYAT FROM TBL_FIRMAHAREKETLER INNER JOIN TBL_URUNLER ON TBL_FIRMAHAREKETLER.URUNID = TBL_URUNLER.ID INNER JOIN TBL_PERSONELLER ON TBL_FIRMAHAREKETLER.PERSONEL = TBL_PERSONELLER.ID INNER JOIN TBL_FIRMALAR ON TBL_FIRMAHAREKETLER.FIRMA = TBL_FIRMALAR.ID ORDER BY HAREKETID DESC", bgl.baglanti());
            da.Fill(dt);
            GridControlHareketler.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT AD,TELEFON1 FROM TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            GridControlFihrist.DataSource = dt;
        }
        void habeler()
        {
            XmlTextReader xml = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while(xml.Read())
            {
                if(xml.Name == "title")
                {
                    listBox1.Items.Add(xml.ReadString());
                }
            }
        }
        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            firmaHareketler();
            fihrist();
            habeler();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}
