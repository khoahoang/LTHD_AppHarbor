using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Authentication.Models;
using HtmlAgilityPack;

namespace Authentication.Controllers
{
    [RoutePrefix("api/GetActicles")]
    public class ExtractionController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetActicles()
        {
            HtmlDocument html = new HtmlDocument();
            html.Load(new WebClient().OpenRead("http://genk.vn/"), Encoding.UTF8);

            List<Acticle> lstAct = new List<Acticle>();
            HtmlNode root = html.DocumentNode;
            IEnumerable<HtmlNode> acticle = root.Descendants()
                .Where(n => n.GetAttributeValue("class", "").Equals("news-stream w690 clearfix"))
                .Single().ChildNodes;

            foreach (HtmlNode child in acticle)
            {
                if (child.Name == "div")
                {
                    Acticle act = new Acticle();
                    // img
                    HtmlNode titleNode = child.Descendants().Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("list-news-img")).Single();
                    act.Image = titleNode.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).GetAttributeValue("src", "");
                    // url
                    act.Url = "http://genk.vn" + titleNode.ChildNodes.ElementAt(0).GetAttributeValue("href", "");
                    // head
                    HtmlNode contentNode = child.Descendants().Where(d => d.Name.Equals("div")).ElementAt(1);
                    act.Head = contentNode.ChildNodes.ElementAt(3).InnerText;
                    // title
                    act.Title = contentNode.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).InnerText;
                    // date
                    act.Date = contentNode.ChildNodes.ElementAt(2).InnerText;

                    lstAct.Add(act);
                }
            }

            return Ok(lstAct);
        }
    }
}
