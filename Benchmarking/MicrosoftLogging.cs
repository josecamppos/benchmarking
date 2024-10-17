using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarking.Benchmarking
{
    [MemoryDiagnoser(true)]
    public partial class MicrosoftLogging
    {
        ILogger _logger;

        public List<int> _numbers;
        public MicrosoftLogging()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<MicrosoftLogging>();

            _numbers = Enumerable.Range(1, 1000).ToList();
        }

        [Benchmark]
        public void Log1()
        {
            foreach (var item in _numbers)
            _logger.LogInformation("Hello World! Logging is {Description}.", Guid.NewGuid());
            
        }

        [Benchmark]
        public void Log2()
        {
            foreach (var item in _numbers)
            _logger.LogInformation($"Hello World! Logging is {Guid.NewGuid()}.");
            
        }

        [LoggerMessage(Level = LogLevel.Information, Message = "Hello World! Logging is {Description}.")]
        static partial void LogStartupMessage(ILogger logger, string description);
        [Benchmark]
        public void Log3()
        {
            foreach (var item in _numbers)
            LogStartupMessage(_logger, Guid.NewGuid().ToString());
            
        }
    }
}
