using System;
using System.Collections.Generic;

namespace Crawler.Api.Filters
{
    public interface IUriFilter
    {
        List<Uri> Filter(IEnumerable<Uri> input);
    }
}
