using HtmlAgilityPack;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Parser
{
    public class CrpCenter
    {
        HttpClient client = null;

        private async Task<HtmlDocument> getPageAsync(string Uri)
        {
            client = new HttpClient();
            var response = await client.GetByteArrayAsync(Uri);
            string source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(source);

            return html;
        }

        public async Task<List<HtmlNode>> filterPageByTagsAsync(string Uri)
        {
            HtmlDocument hd = await getPageAsync(Uri);
            List<HtmlNode> filterTags = new List<HtmlNode>();
            try
            {
                     filterTags = hd.DocumentNode.Descendants()
                    .Where(x => x.Name.Equals("tr") && x.Attributes["class"] != null
                    && (x.Attributes["class"].Value.Contains("even")) 
                    || x.Attributes["class"].Value.Contains("odd")).ToList();
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return filterTags; //check for null
        }

        public List<CompanyAttributes> getInfoOfCompaniesAsync(List<HtmlNode> HtmlParsedByClass)
        {
            var getHtmlOfTags = HtmlParsedByClass;
            foreach (var company in getHtmlOfTags)
            {
                //setDataTo ListOf CompanyAttributes
            }

            return new List<CompanyAttributes>
            {
                new CompanyAttributes()
                {
                    ProjectName = "None",
                    ReturnedMoney = 10
                }
            };
        }
    }
}
