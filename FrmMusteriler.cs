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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {   //sqle bağlanıyoruz bir veri tablosu oluşturuyoruz 
            // veri tabanından müşteriler tablosunu çağırıyoruz
            // ve çağırdığımız tabloyu oluşturduğumuz dtye atıyoruz
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbİl.Properties.Items.Add(dr[0]);

            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            TxtVergiD.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            RchAdres.Text = "";
            Cmbİl.Text = "";
            Cmbİlce.Text = "";
            
        }
       
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            temizle();
        }

        private void Cmbİl_SelectedIndexChanged(object sender, EventArgs e)///iller seçilince indexleri değiştirme
        {
            Cmbİlce.Properties.Items.Clear();//daha önce seçilmiş olan ilçeleri silsin.
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbİl.SelectedIndex+1);//cmblilin seçilen ilinden gelen parametrenin değerini al 
            //sql tabanındaki sehir idsi 1den bailıyor biz 0 dedik o yğzden 1 ekliyoruz
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbİlce.Properties.Items.Add(dr[0]);//0.ındex ilçe adı
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4",MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5",MskTC.Text);
            komut.Parameters.AddWithValue("@p6",TxtMail.Text);
            komut.Parameters.AddWithValue("@p7",Cmbİl.Text);
            komut.Parameters.AddWithValue("@p8", Cmbİlce.Text);
            komut.Parameters.AddWithValue("@p9", RchAdres.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergiD.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        ///grid kontrol içindeki değerleri araçlara taşıyalım
        ///silme işlemi
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //imlecin satır odağı değişince ne olsun
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbİl.Text = dr["IL"].ToString();
                Cmbİlce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
            }
           
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from TBL_MUSTERILER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            listele();
          
        }
         //# TODO
         //diyalog pencerinsde gerçekten silmek istiyor musunuz diye sorsun cevap evetse silsin hayırsa silmesin

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,ADRES=@P9,VERGIDAIRE=@P10 where ID=@P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P5", MskTC.Text);
            komut.Parameters.AddWithValue("@P6", TxtMail.Text);
            komut.Parameters.AddWithValue("@P7", Cmbİl.Text);
            komut.Parameters.AddWithValue("@P8", Cmbİlce.Text);
            komut.Parameters.AddWithValue("@P9", RchAdres.Text);
            komut.Parameters.AddWithValue("@P10", TxtVergiD.Text);
            komut.Parameters.AddWithValue("@P11", Txtid.Text);


            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
