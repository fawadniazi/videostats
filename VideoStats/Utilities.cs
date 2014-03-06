using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoStats
{
	public class Utilities
	{

	  
		public string getPageData(HtmlDocument doc)
        {
            return getData(doc);
		}

        private static string getData(HtmlDocument doc)
        {

            string dateOne = "";
            string str = "";
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//tbody"))
            {
                str += "<table>";
                HtmlNodeCollection c = link.SelectNodes("tr");
                foreach (HtmlNode n1 in c)
                {
                    HtmlNodeCollection thnode = n1.SelectNodes("th");
                    if (thnode != null)
                    {
                        foreach (HtmlNode n2 in thnode)
                        {
                            dateOne = n2.InnerText;
                        }
                    }
                    else
                    {
                        HtmlNodeCollection tdnode = n1.SelectNodes("td");
                        if (tdnode != null)
                        {
                            str += "<tr><td>" + dateOne + "</td><td>" +
                                tdnode[0].InnerText + "</td><td>" + tdnode[1].InnerText +
                                "</td><td>" + tdnode[2].InnerText + "</td><td>  " +
                                tdnode[3].InnerText + "</td><tr>";

                           DBConnect con = new DBConnect();
                            con.InsertFixtures("INSERT INTO videosDB.eplfixtures (id,date,Kickoff,TeamA,TeamB,Score) VALUES(" +
                    "NULL,@col1,@col2,@col3,@col4,@col5)",
                    dateOne, tdnode[0].InnerText, tdnode[1].InnerText.Trim(), tdnode[3].InnerText.Trim(), tdnode[2].InnerText.Trim());


                        }
                    }
                }
                str += "<br><br></table>";
            }
            return str;
        }
	}
}