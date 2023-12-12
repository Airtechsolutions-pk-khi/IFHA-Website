using IFHA.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace IFHA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            string ToEmail, SubJect, cc, Bcc;
            cc = ConfigurationManager.AppSettings["cc"].ToString();
            Bcc = "";
            ToEmail = ConfigurationManager.AppSettings["From"].ToString();
            SubJect = "New Query From Customer";
            string Body = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "contact.txt");
            Body = Body.Replace("#name#", contact.Name.ToString());
            Body = Body.Replace("#email#", contact.Email.ToString());
            Body = Body.Replace("#phone#", contact.Phone.ToString());
            Body = Body.Replace("#subject#", contact.Subject.ToString());
            Body = Body.Replace("#message#", contact.Message.ToString());

            try
            {
                WebMail.SmtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString();
                WebMail.SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = false;
                WebMail.UserName = ConfigurationManager.AppSettings["UserName"].ToString();
                WebMail.From = ConfigurationManager.AppSettings["From"].ToString();
                WebMail.Password = ConfigurationManager.AppSettings["Password"].ToString();
                WebMail.Send(to: ToEmail, subject: SubJect, body: Body, cc: cc, bcc: Bcc, isBodyHtml: true);
                ViewBag.Contact = "";
            }
            catch (Exception ex)
            {
                ViewBag.Contact = "";
            }
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Portfolio()
        {
            return View();
        }
    }
}