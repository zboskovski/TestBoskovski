using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Crawler.Api.Test.Unit
{
    [TestClass]
    public class CrawlerTest
    {
        private Uri _uri;
        private Api.Crawler _crawler;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _uri = new Uri("https://hirespace.com/");
            _crawler = new Crawler(_uri);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task StartCrawlerAsyncTestAsync()
        {
            var expectedLink = new Uri("https://venues.hirespace.com/");

            var result = (await _crawler.StartCrawlerAsync(false)).ToList();
                ;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
            Assert.IsTrue(result.Any(x => x == expectedLink));
        }
    }
}
