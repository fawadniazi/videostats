using System;
using System.Data;

namespace VideoStats.Ligue1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();
            dt = con.Select("Select * from fl1fixtures order by date asc");

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}