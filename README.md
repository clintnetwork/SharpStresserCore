# SharpStresserCore

It's a small HTTP stresser that allows you to make multiple requests on the URL of your choice and it's coded in C# for the .Net Core framework.

Example Code:
```csharp
var stresser = new SharpStresserCore()
    .SetupThreadPool(20)
    .DefineTargetUri("http://your_url.com")
    .LoadUserAgents(new [] {
        "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0) Gecko/20100101 Firefox/5.0",
        "Mozilla/5.0 (compatible; proximic; +http://www.proximic.com/info/spider.php)",
        "msnbot/2.0b (+http://search.msn.com/msnbot.htm)"
    })
    .LoadUserAgentsFromFile("UserAgents.txt")
    .MaxRequests(1000)
    .Run();
```
