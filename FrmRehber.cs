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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //MÜŞTERİ BİLGİLERİ 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT AD,SOYAD,TELEFON,TELEFON2,MAIL FROM TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            //FİRMA BİLGİLERİ
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT AD,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX FROM TBL_FIRMALAR", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
            
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);//seçtiğimiz satırın verisini frmmail classının değerine atayacaktır.
            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frm = new FrmMail();
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);//seçtiğimiz satırın verisini frmmail classının değerine atayacaktır.
            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }
    }
}
