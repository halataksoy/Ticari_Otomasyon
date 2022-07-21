using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

      
        SqlBaglantisi bgl = new SqlBaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();//datatable sınıfından dt adında bir nesne ürettik
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_URUNLER", bgl.baglanti());//select sorgusu ile verileri dataset ya da datatable'a yüklememizi sağlar.
            da.Fill(dt);//dt doldur daları
            gridControl1.DataSource = dt;//dtden gelen değere eşitlensin
        }
        void temizle()
        {
            TxtAd.Text = "";
            TxtAlis.Text = "";
            Txtid.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            TxtSatis.Text = "";
            MskYil.Text = "";
            NudAdet.Value = 0;
            RchDetay.Text = "";
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            Listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //verileri kaydetme
            //SqlCommand, T-Sql sorguları ile veritabanı üzerinde sorgulama, ekleme, güncelleme, silme işlemlerini yapmak için kullanılmaktadır
            SqlCommand komut = new SqlCommand(
                "insert into TBL_URUNLER(URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",
                bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@p3", TxtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));//numeric updowndan gelen değeri convert et
            komut.Parameters.AddWithValue("@p6", decimal.Parse((TxtAlis.Text).ToString()));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from TBL_URUNLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1",Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Listele();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //imlecin satır odağı değişince ne olsun
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            
            Txtid.Text = dr["ID"].ToString();
            TxtAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYil.Text = dr["YIL"].ToString();
            NudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            TxtAlis.Text = dr["ALISFIYAT"].ToString();
            TxtSatis.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_URUNLER set URUNAD=@P1,MARKA=@P2,MODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYAT=@P6,SATISFIYAT=@P7,DETAY=@P8 where ID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@P3", TxtModel.Text);
            komut.Parameters.AddWithValue("@P4", MskYil.Text);
            komut.Parameters.AddWithValue("@P5", int.Parse((NudAdet.Value).ToString()));//numeric updowndan gelen değeri convert et
            komut.Parameters.AddWithValue("@P6", decimal.Parse((TxtAlis.Text).ToString()));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@P8", RchDetay.Text);
            komut.Parameters.AddWithValue("@P9", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            Listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }


    }
}
