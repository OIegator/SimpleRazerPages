using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleRazerPages.Services;

namespace SimpleRazerPages.Pages
{
    public class DefaultModel : PageModel
    {
        protected IDataReader _dataReader;
        public string _pageName;
        public DefaultModel(IDataReader dataService,string name)
        {
            _dataReader = dataService;
            _pageName = name;
        }
    }
}
