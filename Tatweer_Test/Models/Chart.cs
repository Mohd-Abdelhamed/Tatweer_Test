using Newtonsoft.Json.Linq;
using System.Security.Principal;

namespace Tatweer_Test.Models
{
    public class Chart
    {
        public int ID { set; get; }
        public string Date1 { set; get; }
        public Decimal Value1 { set; get; }
        public Decimal Value2 { set; get; }

    }
}
