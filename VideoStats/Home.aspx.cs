using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.IO;
using HtmlAgilityPack;
using System.Data;


namespace VideoStats
{
    public partial class Home : System.Web.UI.Page
    {

        private async Task<HttpResponseMessage> RetrievePage()
        {
            var client = new HttpClient();
            var result = await client.GetAsync(String.Format("http://xkcd.com/1/"));
            return result;
        }

        private async Task<HtmlAgilityPack.HtmlDocument> GetDocument(HttpResponseMessage responseMessage)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                throw new FileNotFoundException("Unable to retrieve document");

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(result);
            return document;
        }

        protected void Page_Load(object sender, EventArgs e)
        {




        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Server.MapPath("/fixturelist.html"));
            Utilities ut = new Utilities();
            HtmlString str = new HtmlString(ut.getPageData(doc));
            Response.Write(str);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.Select();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Preview Image");
            dt.Columns.Add("Duration");
            dt.Columns.Add("EmbedCode");

            dt.Columns.Add("CODE");
            dt.Columns.Add("SPLIT");
            dt.Columns.Add("League");
            dt.Columns.Add("TEAM A");
            dt.Columns.Add("TEAM B");
            dt.Columns.Add("Event");
            dt.Columns.Add("Filename DR");
            dt.Columns.Add("Filename DP");
            dt.Columns.Add("Type");

            //list[0].Count()
            for (int i = 0; i < list[0].Count(); i++)
            {

                if (list[0][i] == "5276") { break; }
                if (list[4][i].IndexOf("http://foxassets.ballball.com") < 0)
                {
                    continue;
                }


                System.Data.DataRow NewRow = dt.NewRow();
                NewRow[0] = list[0][i];
                NewRow[1] = list[1][i];
                NewRow[2] = list[2][i];
                NewRow[3] = list[3][i];
                NewRow[4] = list[4][i];
                NewRow[5] = list[5][i];
                NewRow[6] = list[6][i];
                try
                {
                    NewRow[7] = list[4][i].Substring(50).Split('/')[0];
                }
                catch (Exception ex)
                {
                    NewRow[7] = "ERROR";
                }

                try
                {
                    strSplit = list[4][i].Substring(50).Split('/')[0].Split('_');

                    if (strSplit.Count() == 4)
                    {

                        NewRow[8] += " Teams : >> " + strSplit[0];
                        NewRow[8] += " T1 : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " T2 : >> " + strSplit[3];
                        NewRow[9] = "EPL";
                        if (strSplit[0].Substring(0, 3) != "WER" && strSplit[0].Substring(3, 3) != "PRE" && strSplit[0].Substring(0, 3) != "MWR" && strSplit[0].Substring(3, 3) != "RUP")
                        {
                            NewRow[10] = strSplit[0].Substring(0, 3);
                            NewRow[11] = strSplit[0].Substring(3, 3);
                            if (strSplit[0].Length > 6) { NewRow[12] = "" + strSplit[0].Substring(6, strSplit[0].Length - 6); }
                        }

                        else
                        {
                            NewRow[10] = "";
                            NewRow[11] = "";
                            NewRow[12] = "" + strSplit[0];
                        }

                        NewRow[13] = strSplit[1];
                        NewRow[14] = strSplit[3];

                        if (NewRow[12].ToString().Contains("IM"))
                        {
                            NewRow[15] = "IN MATCH";
                        }
                        if (NewRow[12].ToString().Contains("FTH"))
                        {
                            NewRow[15] = "FULL TIME HIGHLIGHTS";

                        }

                        if (NewRow[12].ToString().Contains("HTH"))
                        {
                            NewRow[15] = "HALF TIME HIGHLIGHTS";

                        }
                        //NewRow[8] += " time Stamp : >> " + strSplit[4];
                    }
                    else if (strSplit.Count() == 5)
                    {
                        NewRow[8] += " Date : >> " + strSplit[0];
                        NewRow[8] += " League : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " Teams : >> " + strSplit[3];
                        NewRow[8] += " time Stamp : >> " + strSplit[4];
                        NewRow[9] = "" + strSplit[1];
                        NewRow[10] = strSplit[3].Substring(0, 3);
                        NewRow[11] = strSplit[3].Substring(3, 3);
                        NewRow[12] = "" + strSplit[2];
                        NewRow[13] = "";
                        NewRow[14] = strSplit[4];

                        if (strSplit[2].Contains("HL"))
                        {
                            NewRow[15] = "Highlights";
                        }
                        if (strSplit[2].Contains("GO") || strSplit[2].Contains("G0"))
                        {
                            NewRow[15] = "GOAL";
                        }
                    }
                    else
                    {
                        NewRow[8] += "" + list[4][i].Substring(50).Split('/')[0];
                    }
                }
                catch (Exception ex)
                {
                    NewRow[8] = "ERROR";
                }
                dt.Rows.Add(NewRow);
            }







            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.Select();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Preview Image");
            dt.Columns.Add("Duration");
            dt.Columns.Add("EmbedCode");
            dt.Columns.Add("CODE");
            dt.Columns.Add("SPLIT");
            dt.Columns.Add("League");

            //list[0].Count()
            for (int i = 0; i < list[0].Count(); i++)
            {

                if (list[0][i] == "5276") { break; }
                if (list[4][i].IndexOf("http://foxassets.ballball.com") < 0)
                {
                    continue;
                }


                System.Data.DataRow NewRow = dt.NewRow();
                NewRow[0] = list[0][i];
                NewRow[1] = list[1][i];
                NewRow[2] = list[2][i];
                NewRow[3] = list[3][i];
                NewRow[4] = list[4][i];
                NewRow[5] = list[5][i];
                NewRow[6] = list[6][i];
                try
                {
                    NewRow[7] = list[4][i].Substring(50).Split('/')[0];
                }
                catch (Exception ex)
                {
                    NewRow[7] = "";
                }

                try
                {
                    strSplit = list[4][i].Substring(50).Split('/')[0].Split('_');

                    if (strSplit.Count() == 4)
                    {

                        NewRow[8] += " Teams : >> " + strSplit[0];
                        NewRow[8] += " T1 : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " T2 : >> " + strSplit[3];
                        NewRow[9] = "EPL";

                        //NewRow[8] += " time Stamp : >> " + strSplit[4];
                    }
                    else if (strSplit.Count() == 5)
                    {
                        NewRow[8] += " Date : >> " + strSplit[0];
                        NewRow[8] += " League : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " Teams : >> " + strSplit[3];
                        NewRow[8] += " time Stamp : >> " + strSplit[4];
                        NewRow[9] = "" + strSplit[1];
                    }
                    else
                    {
                        NewRow[8] += "" + list[4][i].Substring(50).Split('/')[0];
                    }
                }
                catch (Exception ex)
                {
                    NewRow[8] = "";
                }


                con.Insert("INSERT INTO videosDB.videosbyleague"+
                    "(id,date,name,description,preview_image_url,duration,embedcode,code,split,league)"+
                    "VALUES(NULL,@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9)",
                    NewRow[1].ToString(), NewRow[2].ToString(),
                    NewRow[3].ToString(), NewRow[4].ToString(), 
                    NewRow[5].ToString(), NewRow[6].ToString(), 
                    NewRow[7].ToString(), NewRow[8].ToString(), 
                    NewRow[9].ToString());
                //dt.Rows.Add(NewRow);


            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.Select();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Preview Image");
            dt.Columns.Add("Duration");
            dt.Columns.Add("EmbedCode");

            dt.Columns.Add("CODE");
            dt.Columns.Add("SPLIT");
            dt.Columns.Add("League");
            dt.Columns.Add("TEAM A");
            dt.Columns.Add("TEAM B");
            dt.Columns.Add("Event");
            dt.Columns.Add("Filename DR");
            dt.Columns.Add("Filename DP");
            dt.Columns.Add("Type");

            //list[0].Count()
            for (int i = 0; i < list[0].Count(); i++)
            {

                if (list[0][i] == "5276") { break; }
                if (list[4][i].IndexOf("http://foxassets.ballball.com") < 0)
                {
                    continue;
                }


                System.Data.DataRow NewRow = dt.NewRow();
                NewRow[0] = list[0][i];
                NewRow[1] = list[1][i];
                NewRow[2] = list[2][i];
                NewRow[3] = list[3][i];
                NewRow[4] = list[4][i];
                NewRow[5] = list[5][i];
                NewRow[6] = list[6][i];
                try
                {
                    NewRow[7] = list[4][i].Substring(50).Split('/')[0];
                }
                catch (Exception ex)
                {
                    NewRow[7] = "";
                }

                try
                {
                    strSplit = list[4][i].Substring(50).Split('/')[0].Split('_');

                    if (strSplit.Count() == 4)
                    {

                        NewRow[8] += " Teams : >> " + strSplit[0];
                        NewRow[8] += " T1 : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " T2 : >> " + strSplit[3];
                        NewRow[9] = "EPL";
                        if (strSplit[0].Substring(0, 3) != "WER" && strSplit[0].Substring(3, 3) != "PRE" && strSplit[0].Substring(0, 3) != "MWR" && strSplit[0].Substring(3, 3) != "RUP")
                        {
                            NewRow[10] = strSplit[0].Substring(0, 3);
                            NewRow[11] = strSplit[0].Substring(3, 3);
                            if (strSplit[0].Length > 6) { NewRow[12] = "" + strSplit[0].Substring(6, strSplit[0].Length - 6); }
                        }

                        else
                        {
                            NewRow[10] = "";
                            NewRow[11] = "";
                            NewRow[12] = "" + strSplit[0];
                        }

                        NewRow[13] = strSplit[1];
                        NewRow[14] = strSplit[3];

                        if (NewRow[12].ToString().Contains("IM"))
                        {
                            NewRow[15] = "IN MATCH";
                        }
                        if (NewRow[12].ToString().Contains("FTH"))
                        {
                            NewRow[15] = "FULL TIME HIGHLIGHTS";

                        }

                        if (NewRow[12].ToString().Contains("HTH"))
                        {
                            NewRow[15] = "HALF TIME HIGHLIGHTS";

                        }
                        //NewRow[8] += " time Stamp : >> " + strSplit[4];
                        con.Insert(
                            "INSERT INTO videosDB.videosepl (id,date,name,description,preview_image_url,"+
                            "duration,embedcode,code,split,league,TeamA,TeamB,Event,FilenameDR,FilenameDP,Type) VALUES(" +
                            "NULL,@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9,@col10,@col11,@col12,@col13,@col14,@col15)",
                        NewRow[1].ToString(), NewRow[2].ToString(), NewRow[3].ToString(),
                        NewRow[4].ToString(), NewRow[5].ToString(), NewRow[6].ToString(), NewRow[7].ToString(),
                        NewRow[8].ToString(), NewRow[9].ToString(), NewRow[10].ToString(), NewRow[11].ToString(),
                        NewRow[12].ToString(), NewRow[13].ToString(), NewRow[14].ToString(),
                        NewRow[15].ToString());

                    }
                    else if (strSplit.Count() == 5)
                    {
                        NewRow[8] += " Date : >> " + strSplit[0];
                        NewRow[8] += " League : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " Teams : >> " + strSplit[3];
                        NewRow[8] += " time Stamp : >> " + strSplit[4];
                        NewRow[9] = "" + strSplit[1];
                        NewRow[10] = strSplit[3].Substring(0, 3);
                        NewRow[11] = strSplit[3].Substring(3, 3);
                        NewRow[12] = "" + strSplit[2];
                        NewRow[13] = "";
                        NewRow[14] = strSplit[4];

                        if (strSplit[2].Contains("HL"))
                        {
                            NewRow[15] = "Highlights";
                        }
                        if (strSplit[2].Contains("GO") || strSplit[2].Contains("G0"))
                        {
                            NewRow[15] = "GOAL";
                        }
                    }
                    else
                    {
                        NewRow[8] += "" + list[4][i].Substring(50).Split('/')[0];
                    }
                }
                catch (Exception ex)
                {
                    NewRow[8] = "";
                }



                /* foreach (string st in strSplit)
                 {
                     NewRow[8] += " >> "+st;
                 }*/



                //dt.Rows.Add(NewRow);
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.Select();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Preview Image");
            dt.Columns.Add("Duration");
            dt.Columns.Add("EmbedCode");
            dt.Columns.Add("CODE");
            dt.Columns.Add("SPLIT");
            dt.Columns.Add("League");

            dt.Columns.Add("TEAM A");
            dt.Columns.Add("TEAM B");
            dt.Columns.Add("Event");
            dt.Columns.Add("Filename DR");
            dt.Columns.Add("Filename DP");
            dt.Columns.Add("Type");

            //list[0].Count()
            for (int i = 0; i < list[0].Count(); i++)
            {

                if (list[0][i] == "5276") { break; }
                if (list[4][i].IndexOf("http://foxassets.ballball.com") < 0)
                {
                    continue;
                }


                System.Data.DataRow NewRow = dt.NewRow();
                NewRow[0] = list[0][i];
                NewRow[1] = list[1][i];
                NewRow[2] = list[2][i];
                NewRow[3] = list[3][i];
                NewRow[4] = list[4][i];
                NewRow[5] = list[5][i];
                NewRow[6] = list[6][i];
                try
                {
                    NewRow[7] = list[4][i].Substring(50).Split('/')[0];
                }
                catch (Exception ex)
                {
                    NewRow[7] = "";
                }

                try
                {
                    strSplit = list[4][i].Substring(50).Split('/')[0].Split('_');

                    if (strSplit.Count() == 4)
                    {

                        NewRow[8] += " Teams : >> " + strSplit[0];
                        NewRow[8] += " T1 : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " T2 : >> " + strSplit[3];
                        NewRow[9] = "EPL";
                        if (strSplit[0].Substring(0, 3) != "WER" && strSplit[0].Substring(3, 3) != "PRE" && strSplit[0].Substring(0, 3) != "MWR" && strSplit[0].Substring(3, 3) != "RUP")
                        {
                            NewRow[10] = strSplit[0].Substring(0, 3);
                            NewRow[11] = strSplit[0].Substring(3, 3);
                            if (strSplit[0].Length > 6) { NewRow[12] = "" + strSplit[0].Substring(6, strSplit[0].Length - 6); }
                        }

                        else
                        {
                            NewRow[10] = "";
                            NewRow[11] = "";
                            NewRow[12] = "" + strSplit[0];
                        }

                        NewRow[13] = strSplit[1];
                        NewRow[14] = strSplit[3];

                        if (NewRow[12].ToString().Contains("IM"))
                        {
                            NewRow[15] = "IN MATCH";
                        }
                        if (NewRow[12].ToString().Contains("FTH"))
                        {
                            NewRow[15] = "FULL TIME HIGHLIGHTS";

                        }

                        if (NewRow[12].ToString().Contains("HTH"))
                        {
                            NewRow[15] = "HALF TIME HIGHLIGHTS";

                        }
                        //NewRow[8] += " time Stamp : >> " + strSplit[4];

                    }
                    else if (strSplit.Count() == 5)
                    {
                        NewRow[8] += " Date : >> " + strSplit[0];
                        NewRow[8] += " League : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " Teams : >> " + strSplit[3];
                        NewRow[8] += " time Stamp : >> " + strSplit[4];
                        NewRow[9] = "" + strSplit[1];
                        NewRow[10] = strSplit[3].Substring(0, 3);
                        NewRow[11] = strSplit[3].Substring(3, 3);
                        NewRow[12] = "" + strSplit[2];
                        NewRow[13] = "";
                        NewRow[14] = strSplit[4];

                        if (strSplit[2].Contains("HL"))
                        {
                            NewRow[15] = "Highlights";
                        }
                        if (strSplit[2].Contains("GO") || strSplit[2].Contains("G0"))
                        {
                            NewRow[15] = "GOAL";
                        }

                        con.Insert(
                          "INSERT INTO videosDB.videosnonepl (id,date,name,description,preview_image_url,duration,embedcode,code,split,league,TeamA,TeamB,Event,FilenameDR,FilenameDP,Type) VALUES(" +
                      "NULL,@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9,@col10,@col11,@col12,@col13,@col14,@col15)",
                      NewRow[1].ToString(), NewRow[2].ToString(), NewRow[3].ToString(),
                      NewRow[4].ToString(), NewRow[5].ToString(), NewRow[6].ToString(), NewRow[7].ToString(),
                      NewRow[8].ToString(), NewRow[9].ToString(), NewRow[10].ToString(), NewRow[11].ToString(), NewRow[12].ToString(), NewRow[13].ToString(), NewRow[14].ToString(),
                      NewRow[15].ToString());

                    }
                    else
                    {
                        NewRow[8] += "" + list[4][i].Substring(50).Split('/')[0];
                    }
                }
                catch (Exception ex)
                {
                    NewRow[8] = "";
                }
            }



        }

        protected void InsertError_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.Select();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Preview Image");
            dt.Columns.Add("Duration");
            dt.Columns.Add("EmbedCode");
            dt.Columns.Add("CODE");
            dt.Columns.Add("SPLIT");
            dt.Columns.Add("League");

            dt.Columns.Add("TEAM A");
            dt.Columns.Add("TEAM B");
            dt.Columns.Add("Event");
            dt.Columns.Add("Filename DR");
            dt.Columns.Add("Filename DP");
            dt.Columns.Add("Type");

            //list[0].Count()
            for (int i = 0; i < list[0].Count(); i++)
            {

                if (list[0][i] == "5276") { break; }
                if (list[4][i].IndexOf("http://foxassets.ballball.com") < 0)
                {
                    continue;
                }


                System.Data.DataRow NewRow = dt.NewRow();
                NewRow[0] = list[0][i];
                NewRow[1] = list[1][i];
                NewRow[2] = list[2][i];
                NewRow[3] = list[3][i];
                NewRow[4] = list[4][i];
                NewRow[5] = list[5][i];
                NewRow[6] = list[6][i];
                try
                {
                    NewRow[7] = list[4][i].Substring(50).Split('/')[0];
                }
                catch (Exception ex)
                {
                    NewRow[7] = "";
                }

                try
                {
                    strSplit = list[4][i].Substring(50).Split('/')[0].Split('_');

                    if (strSplit.Count() == 4)
                    {

                        NewRow[8] += " Teams : >> " + strSplit[0];
                        NewRow[8] += " T1 : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " T2 : >> " + strSplit[3];
                        NewRow[9] = "EPL";
                        if (strSplit[0].Substring(0, 3) != "WER" && strSplit[0].Substring(3, 3) != "PRE" && strSplit[0].Substring(0, 3) != "MWR" && strSplit[0].Substring(3, 3) != "RUP")
                        {
                            NewRow[10] = strSplit[0].Substring(0, 3);
                            NewRow[11] = strSplit[0].Substring(3, 3);
                            if (strSplit[0].Length > 6) { NewRow[12] = "" + strSplit[0].Substring(6, strSplit[0].Length - 6); }
                        }

                        else
                        {
                            NewRow[10] = "";
                            NewRow[11] = "";
                            NewRow[12] = "" + strSplit[0];
                        }

                        NewRow[13] = strSplit[1];
                        NewRow[14] = strSplit[3];

                        if (NewRow[12].ToString().Contains("IM"))
                        {
                            NewRow[15] = "IN MATCH";
                        }
                        if (NewRow[12].ToString().Contains("FTH"))
                        {
                            NewRow[15] = "FULL TIME HIGHLIGHTS";

                        }

                        if (NewRow[12].ToString().Contains("HTH"))
                        {
                            NewRow[15] = "HALF TIME HIGHLIGHTS";

                        }
                        //NewRow[8] += " time Stamp : >> " + strSplit[4];

                    }
                    else if (strSplit.Count() == 5)
                    {
                        NewRow[8] += " Date : >> " + strSplit[0];
                        NewRow[8] += " League : >> " + strSplit[1];
                        NewRow[8] += " Event : >> " + strSplit[2];
                        NewRow[8] += " Teams : >> " + strSplit[3];
                        NewRow[8] += " time Stamp : >> " + strSplit[4];
                        NewRow[9] = "" + strSplit[1];
                        NewRow[10] = strSplit[3].Substring(0, 3);
                        NewRow[11] = strSplit[3].Substring(3, 3);
                        NewRow[12] = "" + strSplit[2];
                        NewRow[13] = "";
                        NewRow[14] = strSplit[4];

                        if (strSplit[2].Contains("HL"))
                        {
                            NewRow[15] = "Highlights";
                        }
                        if (strSplit[2].Contains("GO") || strSplit[2].Contains("G0"))
                        {
                            NewRow[15] = "GOAL";
                        }

                      

                    }
                    else
                    {
                        NewRow[8] += "" + list[4][i].Substring(50).Split('/')[0];
                    }
                }
                catch (Exception ex)
                {
                    NewRow[8] = "";
                    con.Insert(
                        "INSERT INTO videosDB.videoserror (id,date,name,description,preview_image_url,duration,embedcode,code,split,league,TeamA,TeamB,Event,FilenameDR,FilenameDP,Type) VALUES(" +
                    "NULL,@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9,@col10,@col11,@col12,@col13,@col14,@col15)",
                    NewRow[1].ToString(), NewRow[2].ToString(), NewRow[3].ToString(),
                    NewRow[4].ToString(), NewRow[5].ToString(), NewRow[6].ToString(), NewRow[7].ToString(),
                    NewRow[8].ToString(), NewRow[9].ToString(), NewRow[10].ToString(), NewRow[11].ToString(), NewRow[12].ToString(), NewRow[13].ToString(), NewRow[14].ToString(),
                    NewRow[15].ToString());
                }
            }

        }

        protected void ReadEPL_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();
            dt = con.Select("Select * from videosnonepl");

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        
        protected void Button12_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("Arsenal", "ARS ASR");
           // dictionary.Add("Arsenal","ASR");
            dictionary.Add("Aston Villa","AST");
            dictionary.Add("Cardiff","CAR");
            dictionary.Add("Chelsea","CHE");
            dictionary.Add("Crystal Palace","CRY");
            dictionary.Add("Everton","EVE");
            dictionary.Add("Fulham","FUL");
            dictionary.Add("Hull","HUL");
            dictionary.Add("Liverpool","LIV");
            dictionary.Add("Man Utd","MAN MNU");
            dictionary.Add("Man City","MNC");
           // dictionary.Add("Man Utd","MNU");
            dictionary.Add("Newcastle","NEW");
            dictionary.Add("Norwich","NOR NOU");
           // dictionary.Add("Norwich","NOU");
            dictionary.Add("Southampton","SOU");
            dictionary.Add("Stoke","STO");
            dictionary.Add("Sunderland","SUN");
            dictionary.Add("Swansea","SWA");
            dictionary.Add("Spurs","TOT");
            dictionary.Add("West Brom","WBA");
            dictionary.Add("West Ham","WHU");

            

            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = con.Select("SELECT *, STR_TO_DATE(Date, '%W %d %M %Y') as fixtureDate FROM videosdb.eplfixtures");
            string teamA,teamB ="";
            DateTime KickoffDate = new DateTime();
            DateTime RecDate = new DateTime();

            foreach (DataRow dr in dt.Rows)
            {
                Console.Write(dictionary[dr["TeamA"].ToString()] + dr["TeamA"] + "" + dr["TeamB"]);
                if (dictionary[dr["TeamA"].ToString()].Length > 3 ){ teamA ="(TeamA='"+dictionary[dr["TeamA"].ToString()].Substring(0,3)+"' OR TeamA='"+dictionary[dr["TeamA"].ToString()].Substring(4,3)+"')";} else {teamA ="TeamA='"+dictionary[dr["TeamA"].ToString()]+"'";}
                if (dictionary[dr["TeamB"].ToString()].Length > 3) { teamB = "(TeamB='" + dictionary[dr["TeamB"].ToString()].Substring(0, 3) + "' OR TeamB='" + dictionary[dr["TeamB"].ToString()].Substring(4, 3) + "')"; } else { teamB = "TeamB='" + dictionary[dr["TeamB"].ToString()] + "'"; }
                             
                             dt2 =   con.Select("SELECT date,name,league,TeamA,TeamB,Event,FilenameDR,FilenameDP,Type ,"+
  "Date(videosepl.date) as videoDate, Time(STR_TO_DATE(videosepl.date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb.videosepl Where "+teamA+" and "+teamB+"");

                             Response.Write("<table>");
                Response.Write("<tr> <td>" + "Date : " + dr["Date"] + " Time : " + dr["Kickoff"] + "</td></tr> ");
                             KickoffDate = Convert.ToDateTime(dr["fixtureDate"]).AddHours(Convert.ToDouble(dr["Kickoff"].ToString().Split(':')[0])).AddMinutes(Convert.ToDouble(dr["Kickoff"].ToString().Split(':')[1]));
                             Response.Write("<tr><td> SOCRE : " + dr["Score"] + " Kick Off " + KickoffDate.ToShortDateString() + " " + KickoffDate.TimeOfDay+"</td><tr>");


                             for (int i = 0; i < dt2.Rows.Count; i++)
                             {
                                 RecDate = Convert.ToDateTime(dt2.Rows[i]["videoDate"]).AddHours(Convert.ToDouble(dt2.Rows[i]["time"].ToString().Split(':')[0])).AddMinutes(Convert.ToDouble(dt2.Rows[i]["time"].ToString().Split(':')[1]));
                                 Response.Write("<tr><td>" + dt2.Rows[i]["TeamA"] + " vs " + dt2.Rows[i]["TeamB"] + "</td><td> " + dt2.Rows[i]["Type"] + "</td><td> " + Convert.ToDateTime(dt2.Rows[i]["videoDate"]).ToShortDateString() + "</td> <td>" + dt2.Rows[i]["time"]+"</td>");
                                TimeSpan t = RecDate - KickoffDate;
                                Response.Write(" <td>" + t + "</td></tr>");//+ (RecDate - KickoffDate).TotalHours + " Min " + (RecDate - KickoffDate).t TotalMinutes);
                             }

                             Response.Write("</table>");
                             Response.Write("<br><br>");
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
    }
}