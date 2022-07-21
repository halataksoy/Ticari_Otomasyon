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
    public partial class FrmFaturaUrunDetay : Form
    {
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
        }
        public string id;
        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FATURADETAY where FATURAID='" + id + "'", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            //TEK TIRNAĞI ŞUNDAN DOLAYI KULLANIRIZ
            //SQLDE SORGULAMA YAPARKEN EŞİTLİK SAYISAL BİR İFADE DEĞİLSE TEK TIRNAK İÇİNDE YAZILIYOR 
            //C# DA TEK TIRNAK TEK BAŞINA KULLANILMADIĞI İÇİN "" İÇİNE ALDIK
        }
        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            listele();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme fr = new FrmFaturaUrunDuzenleme();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.urunid = dr["FATURAURUNID"].ToString();
            }
            fr.Show();
        }
    }
}
