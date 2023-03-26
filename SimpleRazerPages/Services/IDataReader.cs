using System.Text.Json.Nodes;

namespace SimpleRazerPages.Services
{
    public interface IDataReader
    {
        public JsonNode GetData(string key);
    }
}