using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoStats.Videos
{
    public partial class home : System.Web.UI.Page
    {
        string newvideotable = "28022014_newvideos";
        string videosbyleaguetable = "28022014_videosbyleague";
        string videosepltbale = "28022014_videosepl";
        string videononepltable = "28022014_videosnonepl";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DisplayButton_Click(object sender, EventArgs e)
        {
            Ooyala.API.OoyalaAPI api = new Ooyala.API.OoyalaAPI("N2d2kxOqq2Drk_2CQjxdgm3ZH9kg.SepyJ", "EXlrcoixWJ3ZSbWl1r3S4PrYfy_XP0nflFpYY21q");
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("limit", "500");
            // parameters.Add("where", "updated_at>'2013-12-01T01:22:50Z' AND updated_at<'2013-12-3T01:22:50Z'");

            Hashtable assets = new Hashtable();
            ArrayList alist = new ArrayList();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Date");
            dt.Columns.Add("Duration");
            DBConnect con = new DBConnect();
            string col1, col2, col3, col4, col5, col6 = "";


            do
            {
                assets = (Hashtable)api.get("assets", parameters);
                if (assets != null)
                {
                    alist = (ArrayList)assets["items"];
                    Response.Write(">>>> ..   <br>");

                    foreach (Hashtable asset in alist)
                    {

                        Console.WriteLine(asset["embed_code"].ToString() + " - " + asset["name"].ToString());
                        System.Data.DataRow NewRow = dt.NewRow();

                        if (asset["name"] != null) { NewRow[0] = asset["name"].ToString(); } else { NewRow[0] = ""; }
                        if (asset["description"] != null) { NewRow[1] = asset["description"].ToString(); } else { NewRow[1] = ""; }
                        if (asset["created_at"] != null)
                        {
                            DateTime dat = Convert.ToDateTime(asset["created_at"]);
                            NewRow[2] = dat.GetDateTimeFormats('F')[12].ToString();
                        }
                        else { NewRow[2] = ""; }

                        if (asset["duration"] != null) { NewRow[3] = Math.Round(((Convert.ToDouble(asset["duration"]) / 1000) / 60), 2); } else { NewRow[3] = ""; }

                        //List<string>[] list = con.Select();

                        // con.Insert("INSERT INTO videosDB.fixtures (id,date,teama,teamascore,teamb,teambscore) VALUES(NULL,'" + NewRow[2] + "', '" + NewRow[0] + "','" + NewRow[1] + "','" + NewRow[3] + "','" + NewRow[2] + "')");


                        if (asset["created_at"] != null) { col1 = asset["created_at"].ToString(); } else { col1 = ""; }
                        if (asset["name"] != null) { col2 = asset["name"].ToString(); } else { col2 = ""; }
                        if (asset["description"] != null) { col3 = asset["description"].ToString(); } else { col3 = ""; }
                        if (asset["preview_image_url"] != null) { col4 = asset["preview_image_url"].ToString(); } else { col4 = ""; }
                        if (asset["duration"] != null) { col5 = asset["duration"].ToString(); } else { col5 = ""; }
                        if (asset["embed_code"] != null) { col6 = asset["embed_code"].ToString(); } else { col6 = ""; }


                        con.Insert("INSERT INTO videosDB."+newvideotable+" (id,date,name,description,preview_image_url,duration,embedcode)" +
                        "VALUES(NULL,@col1,@col2,@col3,@col4,@col5,@col6)", col1, col2, col3, col4, col5, col6);
                        dt.Rows.Add(NewRow);
                    }

                    parameters.Clear();
                    if (assets["next_page"] != null)
                    {
                        //Response.Write(">>>> ..   <br>");
                        parameters.Add(assets["next_page"].ToString().Split('?')[1].Split('&')[0].Split('=')[0], assets["next_page"].ToString().Split('?')[1].Split('&')[0].Split('=')[1]);
                        parameters.Add(assets["next_page"].ToString().Split('?')[1].Split('&')[1].Split('=')[0], assets["next_page"].ToString().Split('?')[1].Split('&')[1].Split('=')[1]);
                        parameters.Add(assets["next_page"].ToString().Split('?')[1].Split('&')[2].Split('=')[0], assets["next_page"].ToString().Split('?')[1].Split('&')[2].Split('=')[1]);
                    }

                }
                else break;

            } while (assets["next_page"] != null);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.SelectData("Select * from "+newvideotable+"");
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


                con.Insert("INSERT INTO videosDB."+videosbyleaguetable+"" +
                    "(id,date,name,description,preview_image_url,duration,embedcode,code,split,league)" +
                    "VALUES(NULL,@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9)",
                    NewRow[1].ToString(), NewRow[2].ToString(),
                    NewRow[3].ToString(), NewRow[4].ToString(),
                    NewRow[5].ToString(), NewRow[6].ToString(),
                    NewRow[7].ToString(), NewRow[8].ToString(),
                    NewRow[9].ToString());
                //dt.Rows.Add(NewRow);


            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.SelectData("Select * from "+newvideotable);
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
                            "INSERT INTO videosDB."+videosepltbale+" (id,date,name,description,preview_image_url," +
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            string[] strSplit;
            DBConnect con = new DBConnect();
            List<string>[] list = con.SelectData("Select * from "+newvideotable);
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
                          "INSERT INTO videosDB."+videononepltable+" (id,date,name,description,preview_image_url,duration,embedcode,code,split,league,TeamA,TeamB,Event,FilenameDR,FilenameDP,Type) VALUES(" +
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

        protected void Button4_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();


            dt = con.Select("SELECT Name, Type,league,TeamA,TeamB, STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Time(STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb."+videononepltable+" where league ='SPR' order by Date desc");

            Response.Write("<table>");
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr><td>" + dr["Name"] + " </td><td>"  + dr["Type"] + "</td><td>" + dr["league"] + "</td><td>"  + dr["TeamA"] + "</td><td>" + dr["TeamB"] + "</td><td> Original Time " + dr["Date"] + "</td><td> Converted Time " + Convert.ToDateTime(dr["Date"]).ToLocalTime() + "</td></tr>");
                Response.Write("<tr><td>  </td><td>  </td><td>  </td></tr>");
            }
            Response.Write("</table>");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();


            dt = con.Select("SELECT Name, Type,league,TeamA,TeamB, STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Time(STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb."+videononepltable+" where league ='GBU' order by Date desc");

            Response.Write("<table>");
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr><td>" + dr["Name"] + " </td><td>" + dr["Type"] + "</td><td>" + dr["league"] + "</td><td>" + dr["TeamA"] + "</td><td>" + dr["TeamB"] + "</td><td> Original Time " + dr["Date"] + "</td><td> Converted Time " + Convert.ToDateTime(dr["Date"]).ToLocalTime() + "</td></tr>");
                Response.Write("<tr><td>  </td><td>  </td><td>  </td></tr>");
            }
            Response.Write("</table>");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();


            dt = con.Select("SELECT Name, Type,league,TeamA,TeamB, STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Time(STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb."+videononepltable+" where league ='FL1' order by Date desc");

            Response.Write("<table>");
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr><td>" + dr["Name"] + " </td><td>" + dr["Type"] + "</td><td>" + dr["league"] + "</td><td>" + dr["TeamA"] + "</td><td>" + dr["TeamB"] + "</td><td> Original Time " + dr["Date"] + "</td><td> Converted Time " + Convert.ToDateTime(dr["Date"]).ToLocalTime() + "</td></tr>");
                Response.Write("<tr><td>  </td><td>  </td><td>  </td></tr>");
            }
            Response.Write("</table>");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();


            dt = con.Select("SELECT Name, Type,league,TeamA,TeamB, STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Time(STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb."+videononepltable+" where league ='ISA' order by Date desc");

            Response.Write("<table>");
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr><td>" + dr["Name"] + " </td><td>" + dr["Type"] + "</td><td>" + dr["league"] + "</td><td>" + dr["TeamA"] + "</td><td>" + dr["TeamB"] + "</td><td> Original Time " + dr["Date"] + "</td><td> Converted Time " + Convert.ToDateTime(dr["Date"]).ToLocalTime() + "</td></tr>");
                Response.Write("<tr><td>  </td><td>  </td><td>  </td></tr>");
            }
            Response.Write("</table>");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();


            dt = con.Select("SELECT Name, Type,league,TeamA,TeamB, STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Time(STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb."+videononepltable+" where league ='FAC' order by Date desc");

            Response.Write("<table>");
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr><td>" + dr["Name"] + " </td><td>" + dr["Type"] + "</td><td>" + dr["league"] + "</td><td>" + dr["TeamA"] + "</td><td>" + dr["TeamB"] + "</td><td> Original Time " + dr["Date"] + "</td><td> Converted Time " + Convert.ToDateTime(dr["Date"]).ToLocalTime() + "</td></tr>");
                Response.Write("<tr><td>  </td><td>  </td><td>  </td></tr>");
            }
            Response.Write("</table>");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();


            dt = con.Select("SELECT Name, Type,league,TeamA,TeamB, STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Time(STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s')) as time FROM videosdb."+videosepltbale+" order by Date desc");

            Response.Write("<table>");
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr><td>" + dr["Name"] + " </td><td>" + dr["Type"] + "</td><td>" + dr["league"] + "</td><td>" + dr["TeamA"] + "</td><td>" + dr["TeamB"] + "</td><td> Original Time " + dr["Date"] + "</td><td> Converted Time " + Convert.ToDateTime(dr["Date"]).ToLocalTime() + "</td></tr>");
                Response.Write("<tr><td>  </td><td>  </td><td>  </td></tr>");
            }
            Response.Write("</table>");

        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();
            ArrayList listofdelayedfiles = new ArrayList();
            ArrayList listofontimefiles = new ArrayList();

            dt = con.Select("SELECT * FROM videosdb.laligafixtures");

            Response.Write("<table class=\"table table-hover\">");

            Response.Write("<th>Name</th>");
            Response.Write("<th>Team A</th>");
            Response.Write("<th>Team B</th>");
            Response.Write("<th>Type</th>");
            Response.Write("<th>Date Fixture</th>");
            Response.Write("<th>Received</th>");
            Response.Write("<th>Difference</th>");


            foreach (DataRow dr in dt.Rows)
            {
                string teamA= dr["TeamACode"].ToString();
                string teamB= dr["TeamBCode"].ToString();
                string query = "";


                if (teamA.Contains("or") && teamB.Contains("or"))
                {

                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='SPR'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");
                
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if(teamA.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type , description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB + "') and league='SPR'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamB.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type , description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='SPR'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB + "') and league='SPR'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");


                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
            }
            Response.Write("</table>");

            for (int i = 0; i < listofdelayedfiles.Count; i++)
            {
                Response.Write("" + listofdelayedfiles[i].ToString().Split('+')[2] + "<br>");

            }
            Response.Write("List of delayed files" + listofdelayedfiles.Count + "<br>");
            Response.Write("List of delayed files" + listofontimefiles.Count);
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();

            ArrayList listofdelayedfiles = new ArrayList();
            ArrayList listofontimefiles = new ArrayList();
            dt = con.Select("SELECT * FROM videosdb.fl1results");

            Response.Write("<table class=\"table table-hover\">");

            Response.Write("<th>Name</th>");
            Response.Write("<th>Team A</th>");
            Response.Write("<th>Team B</th>");
            Response.Write("<th>Type</th>");
            Response.Write("<th>Date Fixture</th>");
            Response.Write("<th>Received</th>");
            Response.Write("<th>Difference</th>");


            foreach (DataRow dr in dt.Rows)
            {
                string teamA = dr["TeamACode"].ToString();
                string teamB = dr["TeamBCode"].ToString();
                string query = "";


                if (teamA.Contains("or") && teamB.Contains("or"))
                {

                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='FL1'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamA.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB + "') and league='FL1'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamB.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type , description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='FL1'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB + "') and league='Fl1'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");


                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(5, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
            }
            Response.Write("</table>");
            for (int i = 0; i < listofdelayedfiles.Count; i++)
            {
                Response.Write("" + listofdelayedfiles[i].ToString().Split('+')[2] + "<br>");

            }
            Response.Write("List of delayed files" + listofdelayedfiles.Count + "<br>");
            Response.Write("List of delayed files" + listofontimefiles.Count);
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();

            dt = con.Select("SELECT * FROM videosdb.isaresults");

            ArrayList listofdelayedfiles = new ArrayList();
            ArrayList listofontimefiles = new ArrayList();

            Response.Write("<table class=\"table table-hover\">");
            Response.Write("<th>Name</th>");
            Response.Write("<th>Team A</th>");
            Response.Write("<th>Team B</th>");
            Response.Write("<th>Type</th>");
            Response.Write("<th>Date Fixture</th>");
            Response.Write("<th>Received</th>");
            Response.Write("<th>Difference</th>");


            foreach (DataRow dr in dt.Rows)
            {
                string teamA = dr["TeamACode"].ToString();
                string teamB = dr["TeamBCode"].ToString();
                string query = "";


                if (teamA.Contains("or") && teamB.Contains("or"))
                {

                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='ISA'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamA.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB + "') and league='ISA'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamB.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='ISA'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB + "') and league='ISA'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");


                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2,0,0) ));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
            }

           
            Response.Write("</table>");

            for (int i = 0; i < listofdelayedfiles.Count; i++)
            {
                Response.Write("" + listofdelayedfiles[i].ToString().Split('+')[2] + "<br>");

            }
            Response.Write("List of delayed files" + listofdelayedfiles.Count + "<br>");
            Response.Write("List of delayed files" + listofontimefiles.Count);
        }

        protected void Button13_Click(object sender, EventArgs e)
        {


            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();

            ArrayList listofdelayedfiles = new ArrayList();
            ArrayList listofontimefiles = new ArrayList();


            dt = con.Select("SELECT * FROM videosdb.bplresults");

            Response.Write("<table class=\"table table-hover\">");
            Response.Write("<th>Name</th>");
            Response.Write("<th>Team A</th>");
            Response.Write("<th>Team B</th>");
            Response.Write("<th>Type</th>");
            Response.Write("<th>Date Fixture</th>");
            Response.Write("<th>Received</th>");
            Response.Write("<th>Difference</th>");


            foreach (DataRow dr in dt.Rows)
            {
                string teamA = dr["TeamACode"].ToString();
                string teamB = dr["TeamBCode"].ToString();
                string query = "";


                if (teamA.Contains("or") && teamB.Contains("or"))
                {

                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb."+videosepltbale+" Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and (type like 'FULL TIME %' or  type like 'IN MA%')";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"9\" ></td></tr><tr><td colspan=\"9\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }
                        Response.Write("<td>");
                        Response.Write(ldr["description"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["preview_image_url"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["duration"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["embedcode"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["code"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["split"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["league"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamB"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["Event"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDR"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDP"]);
                        Response.Write("</td>");

                        Response.Write("</tr>");
                    }
                }
                else if (teamA.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videosepltbale + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB + "') and (type like 'FULL TIME %' or  type like 'IN MA%')";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"9\" ></td></tr><tr><td colspan=\"9\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("<td>");
                        Response.Write(ldr["description"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["preview_image_url"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["duration"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["embedcode"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["code"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["split"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["league"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamB"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["Event"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDR"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDP"]);
                        Response.Write("</td>");

                        Response.Write("</tr>");
                    }
                }
                else if (teamB.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videosepltbale + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and (type like 'FULL TIME %' or  type like 'IN MA%')";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"9\" ></td></tr><tr><td colspan=\"9\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }
                        Response.Write("<td>");
                        Response.Write(ldr["description"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["preview_image_url"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["duration"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["embedcode"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["code"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["split"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["league"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamB"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["Event"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDR"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDP"]);
                        Response.Write("</td>");

                        Response.Write("</tr>");
                    }
                }
                else
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videosepltbale + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB + "') and (type like 'FULL TIME %' or  type like 'IN MA%')";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"9\" ></td></tr><tr><td colspan=\"9\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");


                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("<td>");
                        Response.Write(ldr["description"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["preview_image_url"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["duration"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["embedcode"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["code"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["split"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["league"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["TeamB"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["Event"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDR"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(ldr["FilenameDP"]);
                        Response.Write("</td>");


                        Response.Write("</tr>");
                    }
                }
            }

            
            Response.Write("</table>");

            for(int i=0;i<listofdelayedfiles.Count;i++)
            {
                Response.Write("" + listofdelayedfiles[i].ToString().Split('+')[2] + "<br>");

            }
            Response.Write("List of delayed files" + listofdelayedfiles.Count+"<br>");
            Response.Write("List of delayed files" + listofontimefiles.Count);

        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            DBConnect con = new DBConnect();
            DataTable dt = new DataTable();

            dt = con.Select("SELECT * FROM videosdb.gburesults");

            ArrayList listofdelayedfiles = new ArrayList();
            ArrayList listofontimefiles = new ArrayList();

            Response.Write("<table class=\"table table-hover\">");
            Response.Write("<th>Name</th>");
            Response.Write("<th>Team A</th>");
            Response.Write("<th>Team B</th>");
            Response.Write("<th>Type</th>");
            Response.Write("<th>Date Fixture</th>");
            Response.Write("<th>Received</th>");
            Response.Write("<th>Difference</th>");


            foreach (DataRow dr in dt.Rows)
            {
                string teamA = dr["TeamACode"].ToString();
                string teamB = dr["TeamBCode"].ToString();
                string query = "";


                if (teamA.Contains("or") && teamB.Contains("or"))
                {

                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='GBU'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamA.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA.Substring(0, 3) + "' or TeamA='" + teamA.Substring(7, 3) + "')  and (TeamB='" + teamB + "') and league='GBU'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else if (teamB.Contains("or"))
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB.Substring(0, 3) + "' or TeamB='" + teamB.Substring(7, 3) + "') and league='GBU'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
                else
                {
                    query = "Select STR_TO_DATE(date, '%Y-%m-%dT%H:%i:%s') As Date, Name,Type, description,    preview_image_url,    duration,    embedcode,    code,    split,    league,    TeamA,    TeamB,    Event,    FilenameDR,    FilenameDP from videosdb." + videononepltable + " Where (TeamA='" + teamA + "')  and (TeamB='" + teamB + "') and league='GBU'";

                    DataTable ldt = new DataTable();
                    ldt = con.Select(query);

                    Response.Write("<tr><td colspan=\"7\" ></td></tr><tr><td colspan=\"7\" >");
                    Response.Write("Fixture : " + dr["Date"] + " " + dr["TeamA"] + " vs " + dr["TeamB"] + " " + dr["score"]);
                    Response.Write("</td>");

                    foreach (DataRow ldr in ldt.Rows)
                    {
                        Response.Write("<tr><td>");
                        Response.Write(ldr["Name"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamA"]);
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(dr["TeamB"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(ldr["Type"]);
                        Response.Write("</td>");


                        Response.Write("<td>");
                        Response.Write(dr["Date"]);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(Convert.ToDateTime(ldr["Date"]).ToLocalTime());
                        Response.Write("</td>");

                        TimeSpan duration = Convert.ToDateTime(ldr["Date"]).ToLocalTime() - Convert.ToDateTime(dr["Date"]);

                        Response.Write("<td>");
                        Response.Write(" " + duration);
                        Response.Write("</td>");

                        Response.Write("<td>");
                        Response.Write(" " + (duration - new TimeSpan(2, 0, 0)));
                        Response.Write("</td>");

                        if (duration - new TimeSpan(2, 0, 0) > new TimeSpan(4, 0, 0))
                        {
                            Response.Write("<td>");
                            Response.Write("Exceed Time limit");
                            Response.Write("</td>");
                            listofdelayedfiles.Add("" + ldr["Name"] + " + " + ldr["description"] + " + Diff in Duration " + (duration - new TimeSpan(2, 0, 0)));
                        }
                        else
                        {
                            Response.Write("<td>");
                            Response.Write("On Time ");
                            Response.Write("</td>");
                            listofontimefiles.Add("" + ldr["Name"] + " + " + ldr["description"]);
                        }

                        Response.Write("</tr>");
                    }
                }
            }


            Response.Write("</table>");

            for (int i = 0; i < listofdelayedfiles.Count; i++)
            {
                Response.Write("" + listofdelayedfiles[i].ToString().Split('+')[2] + "<br>");

            }
            Response.Write("List of delayed files" + listofdelayedfiles.Count + "<br>");
            Response.Write("List of delayed files" + listofontimefiles.Count);
        }
    }
}