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
                .DefineTargetUri("http://hasard.io")
                .LoadUserAgentsFromFile("UserAgents.txt")
                .Run();
        }
    }
}
