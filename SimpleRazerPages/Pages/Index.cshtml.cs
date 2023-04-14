using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleRazerPages.Services;
using SimpleRazerPages.Pages;
using SimpleRazerPages.Data;
using System.Text.Json.Nodes;

namespace SimpleRazerPages.Pages
{
    public class IndexModel : DefaultModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Context _db;
        public string name = "Index";
        public JsonNode data; 

        public IndexModel(ILogger<IndexModel> logger, IDataReader reader, Context db): base(reader, "index")
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            data = _dataReader.GetData("index");
            ViewData["count"] = this._db.Contacts.ToList().Count;

        }

    }
}