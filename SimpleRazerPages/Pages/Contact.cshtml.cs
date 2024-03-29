using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CsvHelper;
using SimpleRazerPages.Services;
using System.Security.Principal;
using System.Globalization;
using SimpleRazerPages.Data;

namespace SimpleRazerPages.Pages
{
    public class EmailMessage
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    [IgnoreAntiforgeryToken]
    public class ContactModel : DefaultModel
    {


        private readonly ILogger<IndexModel> _logger;
        private readonly Context _db;
        public ContactModel(ILogger<IndexModel> logger, IDataReader reader, Context db) : base(reader, "contact")
        {
            _logger = logger;
            _db = db;
        }

      
        public string Email { get; set; }
        public string Body { get; set; }
        public string name
        {
            get { return _pageName; }
        }

        [BindProperty]
        public EmailMessage Message { get; set; }
        public IActionResult OnPost()
        {
            foreach (var key in Request.Form.Keys)
            {
                _logger.LogInformation($"{key}:{Request.Form[key]}");
            }


            if (Message.First_Name == null || Message.First_Name == "")
            {
                return Content("<div class=\"error_message\">Attention! You must enter your name.</div>");
            }
            else
            {
                this._db.Contacts.Add(new Models.Contact { FirstName = Message.First_Name, Email = Message.Email, LastName = Message.Last_Name, Phone = Message.Phone });
                this._db.SaveChanges();
                WriteFile("comments.csv", Message);
                return Content($@"<fieldset>
                        <div id='success_page'> 
                        <h1>Email Sent Successfully.</h1>
                        <p>Thank you <strong>{Message.First_Name}<strong>, your message has been submitted to us.<p>
                        </div>
                    <fieldset>");
            }
        }

        private void WriteFile(string filepath, EmailMessage message)
        {
            var first = !System.IO.File.Exists(filepath);
            using (var writer = new StreamWriter(filepath, true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (first)
                {
                    csv.WriteHeader<EmailMessage>();
                    csv.NextRecord();
                }
                csv.WriteRecord(message);
                csv.NextRecord();
            }
        }
    }
}
