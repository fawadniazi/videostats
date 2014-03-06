using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoStats.LaLiga
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
            dt = con.Select("Select * from laligafixtures order by date asc");

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
    }
}