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
        public List<int> _numbers;
        public MicrosoftLogging()
        {
            _numbers = Enumerable.Range(1, 100).ToList();
        }

        [Benchmark]
        public void Log1()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger<MicrosoftLogging>();

            foreach (var item in _numbers)
            {
                logger.LogInformation("Hello World! Logging is {Description}.", Guid.NewGuid());
            }
        }

        [Benchmark]
        public void Log2()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger<MicrosoftLogging>();

            foreach (var item in _numbers)
            {
                logger.LogInformation($"Hello World! Logging is {Guid.NewGuid()}.");
            }
        }

        [LoggerMessage(Level = LogLevel.Information, Message = "Hello World! Logging is {Description}.")]
        static partial void LogStartupMessage(ILogger logger, string description);
        [Benchmark]
        public void Log3()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger<MicrosoftLogging>();

            foreach (var item in _numbers)
            {
                LogStartupMessage(logger, Guid.NewGuid().ToString());
            }
        }
    }
}
