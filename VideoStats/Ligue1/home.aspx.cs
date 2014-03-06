using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoStats.Ligue1
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DisplayButton_Click(object sender, EventArgs e)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Server.MapPath("fixtures.html"));
            string day, hour, minute, teama, teamb, score, date, year, month, kickoff = "";
            DateTime datetime;
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//table"))
            {
                Response.Write("<table>");
                Response.Write("<tr><th>Date</th><th>Time</th><th>Team A</th><th>Team B</th><th>Score</th>");
                HtmlNodeCollection nodeCollection = link.SelectNodes("tr");
                foreach (HtmlNode node in nodeCollection)
                {
                    if (node.SelectNodes("td")[0].InnerText != "&nbsp;")
                    {
                        kickoff = node.SelectNodes("td")[0].InnerText.Split(' ')[1];
                        day = node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[0];
                        month = node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[1];
                        hour = node.SelectNodes("td")[0].InnerText.Split(' ')[1].Split(':')[0];
                        minute = node.SelectNodes("td")[0].InnerText.Split(' ')[1].Split(':')[1];
                        teama = node.SelectNodes("td")[1].InnerText;
                        teamb = node.SelectNodes("td")[2].InnerText;
                        score = node.SelectNodes("td")[3].InnerText.Replace("&nbsp;:&nbsp;", ":");
                        year = Convert.ToInt16(month) <= 7 ? "2014" : "2013";

                        date = year + "-" + month + "-" + day + " " + hour + ":" + minute;

                        DBConnect con = new DBConnect();
                        con.InsertBundesligaFixtures("INSERT INTO videosdb.fl1fixtures(id,date,kickoff,TeamA,TeamB,score)VALUES(NULL,@col1,@col2,@col3,@col4,@col5)", date, kickoff, teama, teamb, score);


                        Response.Write("<tr> <td>" + date + "<td> Day > " + node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[0] +
                            " Month > " + node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[1] + " </td> <td> " +
                            node.SelectNodes("td")[0].InnerText.Split(' ')[1] + "</td> <td>" +
                            node.SelectNodes("td")[1].InnerText + "</td> <td>" +
                            node.SelectNodes("td")[2].InnerText + "</td> <td>" +
                            node.SelectNodes("td")[3].InnerText + "</td></tr>");
                    }
                }
                Response.Write("</table>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.Load(Server.MapPath("results.html"));
            string day, hour, minute, teama, teamb, score, date, year, month, kickoff = "";
            DateTime datetime;
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//table"))
            {
                Response.Write("<table>");
                Response.Write("<tr><th>Date</th><th>Time</th><th>Team A</th><th>Team B</th><th>Score</th>");
                HtmlNodeCollection nodeCollection = link.SelectNodes("tr");
                foreach (HtmlNode node in nodeCollection)
                {
                    
                        kickoff = node.SelectNodes("td")[0].InnerText.Split(' ')[1];
                        day = node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[0];
                        month = node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[1];
                        hour = node.SelectNodes("td")[0].InnerText.Split(' ')[1].Split(':')[0];
                        minute = node.SelectNodes("td")[0].InnerText.Split(' ')[1].Split(':')[1];
                        teama = node.SelectNodes("td")[1].InnerText.Trim();
                        teamb = node.SelectNodes("td")[2].InnerText.Trim();
                        score = node.SelectNodes("td")[3].InnerText.Replace("&nbsp;:&nbsp;", ":");
                        year = Convert.ToInt16(month) <= 7 ? "2014" : "2013";

                        date = year + "-" + month + "-" + day + " " + hour + ":" + minute;

                        DBConnect con = new DBConnect();
                        con.InsertBundesligaFixtures("INSERT INTO videosdb.fl1results(id,date,kickoff,TeamA,TeamB,score)VALUES(NULL,@col1,@col2,@col3,@col4,@col5)", date, kickoff, teama, teamb, score);


                        Response.Write("<tr> <td>" + date + "<td> Day > " + node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[0] +
                            " Month > " + node.SelectNodes("td")[0].InnerText.Split(' ')[0].Split('.')[1] + " </td> <td> " +
                            node.SelectNodes("td")[0].InnerText.Split(' ')[1] + "</td> <td>" +
                            node.SelectNodes("td")[1].InnerText.Trim() + "</td> <td>" +
                            node.SelectNodes("td")[2].InnerText.Trim() + "</td> <td>" +
                            node.SelectNodes("td")[3].InnerText + "</td></tr>");
                    
                }
                Response.Write("</table>");
            }

        }
    }
}