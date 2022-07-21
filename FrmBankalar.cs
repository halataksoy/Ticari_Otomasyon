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
        
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter( "Execute BankaBilgileri",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            LueFirma.Properties.NullText = "Lütfen bir firma seçiniz";
            LueFirma.Properties.ValueMember = "ID";
            LueFirma.Properties.DisplayMember = "AD";
            LueFirma.Properties.DataSource = dt;
        }
        //temizleme metodu oluşturcaz
        void temizle()
        {
            Txtid.Text = "";
            TxtBankaAdı.Text = "";
            TxtHesapTürü.Text = "";
            TxtSube.Text = "";
            TxtYetkili.Text = "";
            Cmbİl.Text = "";
            Cmbİlce.Text = "";
            MskHesapNo.Text = "";
            MskIban.Text = "";
            MskTarih.Text = "";
            MskTelefon.Text = "";
            LueFirma.EditValue = null;
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
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            firmalistesi();
         
        }
        //Biz firmalar tablosu ile bankalar tablosunu sqlde ilişkilendirdik
        //İstediğimiz şey inner join ile getirdiğimiz ortak veri tablosundan firmalardan gelen id değilde 
        // firma adının yazması bunun için de sqlden firma adına göre idyi getirmesi çok sağlıklı olmaz bunu bir nebze de olsa
        // daha sağlıklı hale getirmek için combobox kullanalım firmalar çıksın biz seçelim.
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAdı.Text);
            komut.Parameters.AddWithValue("@p2", Cmbİl.Text);
            komut.Parameters.AddWithValue("@p3", Cmbİlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", MskIban.Text);
            komut.Parameters.AddWithValue("@p6", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTürü.Text);
            komut.Parameters.AddWithValue("@p11", LueFirma.EditValue);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Sisteme Kaydedildi", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }

        private void Cmbİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbİlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbİl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //imlecin satır odağı değişince ne olsun
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtBankaAdı.Text = dr["BANKAADI"].ToString();
                Cmbİl.Text = dr["IL"].ToString();
                Cmbİlce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                MskIban.Text = dr["IBAN"].ToString();
                MskHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                TxtHesapTürü.Text = dr["HESAPTURU"].ToString();
                //LueFirma.EditValue = LueFirma.Properties.GetKeyValueByDisplayText(dr["FIRMAID"].ToString());


            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_BANKALAR where  ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Banka bilgisi listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtBankaAdı.Text);
            komut.Parameters.AddWithValue("@P2", Cmbİl.Text);
            komut.Parameters.AddWithValue("@P3", Cmbİlce.Text);
            komut.Parameters.AddWithValue("@P4", TxtSube.Text);
            komut.Parameters.AddWithValue("@P5", MskIban.Text);
            komut.Parameters.AddWithValue("@P6", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P9", MskTarih.Text);
            komut.Parameters.AddWithValue("@P10", TxtHesapTürü.Text);
            komut.Parameters.AddWithValue("@P11", LueFirma.EditValue);
            komut.Parameters.AddWithValue("@P12", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
         
        }
    }
}
