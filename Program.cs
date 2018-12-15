using System;
using System.Net;
using SharpStresser;

namespace ExampleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var stresser = new SharpStresserCore()
                .SetupThreadPool(100)
                .DefineTargetUri("http://google.com")
                .LoadUserAgentsFromFile("UserAgents.txt")
                .Run();
        }
    }
}
