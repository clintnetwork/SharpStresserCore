using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace SharpStresser
{
    public class SharpStresserCore
    {
        private Methods _method { get; set; }
        private int _actualThreadNumber { get; set; } = 0;
        private int _threadPoolSize { get; set; } = 10;
        private int _maxRequests { get; set; } = int.MaxValue;
        private List<string> _userAgents = new List<string>();
        private string _targetUri { get; set; } 

        public SharpStresserCore SetMethod(Methods method)
        {
            _method = method;
            return this;
        }

        public SharpStresserCore()
        {
            Console.WriteLine("Running SharpStresser...");
        }

        public SharpStresserCore SetupThreadPool(int threadsNumber)
        {
            ThreadPool.SetMaxThreads(threadsNumber, threadsNumber);
            _threadPoolSize = threadsNumber;
            return this;
        }

        public SharpStresserCore DefineTargetUri(string uri)
        {
            _targetUri = uri;
            return this;
        }

        public SharpStresserCore LoadUserAgents(string[] userAgents)
        {
            _userAgents.AddRange(userAgents.ToList());
            return this;
        }

        public SharpStresserCore LoadUserAgentsFromFile(string filename)
        {
            foreach(var ua in System.IO.File.ReadAllLines(filename))
            {
                _userAgents.Add(ua);
            }
            return this;
        }

        public SharpStresserCore MaxRequests(int maxRequests)
        {
            _maxRequests = maxRequests;
            return this;
        }

        public SharpStresserCore Run()
        {
            while(_actualThreadNumber < _maxRequests)
            {
                if(_actualThreadNumber < _threadPoolSize)
                {
                    new Thread(ThreadProc).Start();
                }
            }
            return this;
        }

        private async void ThreadProc(object state)
        {
            _actualThreadNumber++;
            var rc = new RestClient(_targetUri);
            rc.UserAgent = _userAgents[new Random().Next(0, _userAgents.Count)];
            var rs = new RestRequest(Method.GET);
            var response = await rc.ExecuteTaskAsync(rs);

            Console.WriteLine($"Response {response.StatusCode} with {rc.UserAgent}");
            Thread.Sleep(100000);
            _actualThreadNumber--;
        }
    }
}