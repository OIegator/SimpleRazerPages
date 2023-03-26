using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleRazerPages.Services;
using SimpleRazerPages.Pages;
using System.Text.Json.Nodes;

namespace SimpleRazerPages.Pages
{
    public class IndexModel : DefaultModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string name = "Index";
        public JsonNode data; 

        public IndexModel(ILogger<IndexModel> logger, IDataReader reader): base(reader, "index")
        {
            _logger = logger;
        }

        public void OnGet()
        {
            data = _dataReader.GetData("index");

        }

    }
}