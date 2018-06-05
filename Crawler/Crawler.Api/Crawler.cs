using Crawler.Api.Filters;
using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crawler.Api
{
    public class Crawler
    {        
        private IEnumerable<IUriFilter> _filters;
        private Uri _uri;
        private string _hostAddress
        {
            get
            {
                return string.Format(@"{0}://{1}", _uri.Scheme, _uri.Host);
            }
        }

        public Task<List<string>> Links { get; set; }

        public Crawler(Uri uri)
            : this(uri, new ExcludeRootUriFilter(uri), new ExternalUriFilter(uri))
        { }

        public Crawler(Uri uri, params IUriFilter[] filters)
        {
            _uri = uri;
            _filters = filters;
        }

        public async Task<IEnumerable<Uri>> StartCrawlerAsync(bool crawlLinksOnFirstPage)
        {
            var links = new List<Uri>();
            var firstPageLinks = (await CrawlPageAsync(_uri)).ToList();           
            links.AddRange(firstPageLinks);

            //This part could be uncommented if we want to crawl all links found on the initial page
            if (crawlLinksOnFirstPage)
            {
                foreach (var link in firstPageLinks)
                {
                    var newLinks = (await CrawlPageAsync(link)).ToList();
                    if (newLinks != null)
                    {
                        newLinks.ForEach(x =>
                        {
                            if (!links.Any(y => y == x))
                            {
                                links.Add(x);
                            }
                        });
                    }
                }
            }
            //

            return links;
        }

        private async Task<IEnumerable<Uri>> CrawlPageAsync(Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var html = await httpClient.GetStringAsync(uri);
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);

                    var links = htmlDoc.DocumentNode.SelectNodes("//a[@href]").Select(i => i.Attributes["href"].Value)
                        .SafeSelect(i =>
                    {
                        if (i.StartsWith(@"/"))
                        {
                            return new Uri(_hostAddress + i);
                        }
                        else
                        {
                            return new Uri(i);
                        }
                    }).Distinct();
                    links = Filter(links, _filters.ToArray());

                    return links;
                }
                catch
                {
                    return Enumerable.Empty<Uri>(); 
                }
            }
        }

        private List<Uri> Filter(IEnumerable<Uri> uris, params IUriFilter[] filters)
        {
            var filtered = uris.ToList();
            foreach (var filter in filters.ToList())
            {
                filtered = filter.Filter(filtered);
            }
            return filtered;
        }
    }
}
