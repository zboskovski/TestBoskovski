This is a simple MVC app, that crawls all links on the specified page. Url can be entered entered in the "domain" textbox field on the Home page. 
(Also there is option recursively to crawl all links/pages that are on the initial page. This could be set in the HomeController, if you call StartCrawlerAsync
method with parameter crawlLinksOnFirstPage set to "true").



The solution contains three projects:

1. Crawler.Api - class library project. In this project is the whole logic for the web crawler. "Crawler.cs" is the main class. There are two constructors...
So you could initiate crawler just with Uri, or with Uri and you could define custom filters (let's say to excleude external links or root link).
Crawler will start to crawl through the page if you call method StartCrawlAsync (you could set crawlLinksOnFirstPage parameter to "true", if you want to recursively
crawl all links that would be found on the first page).

2. Crawler.Demo - MVC app, used just to display the crawled results/links. You just need to enter the domain in the "domain" textbox and click on the "Crawl" button
and links will be printed bellow in the Home/Index page. The logic is in the HomeController.

3. Crawler.Api.Test.Unit - Test project. There is only one test for the StartCrawlAsync method.
