using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crawler.Demo.Models
{
    public class CrawlerModel
    {
        public CrawlerModel()
        {
            Links = new List<string>();
        }

        [Required(ErrorMessage = "Uri is required!")]
        public string Uri { get; set; }
        public IList<string> Links { get; set; }
    }
}