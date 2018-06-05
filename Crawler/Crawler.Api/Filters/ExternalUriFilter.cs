using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.Api.Filters
{
    internal class ExternalUriFilter : IUriFilter
    {
        private Uri _root;
        public ExternalUriFilter(Uri root)
        { _root = root; }

        public List<Uri> Filter(IEnumerable<Uri> input)
        {
            var result = input.Where(i => i.Host.Contains(_root.Host)).ToList();
            return result;
        }
    }
}
