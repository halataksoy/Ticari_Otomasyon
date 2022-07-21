using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Ticari_Otomasyon
{
    class SqlBaglantisi
    {
        public SqlConnection baglanti()/*geriye değer döndüren bir metod*/
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-K9OGVO4J\SQLEXPRESS;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
