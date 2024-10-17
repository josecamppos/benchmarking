
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarking.Benchmarking
{
    public record NumberWrapper(int Number);
    public record StringWrapper(string String);

    [MemoryDiagnoser(false)]
    public class Find
    {
        private readonly List<int> _numbers;
        private readonly List<string> _strings;

        private readonly List<NumberWrapper> _numbersWrapper;
        private readonly List<StringWrapper> _stringsWrapper;

        public Find()
        {
            _numbers = Enumerable.Range(1, 1000).ToList();

            _strings = Enumerable.Range(1, 1000).Select(x => x.ToString()).ToList();

            _numbersWrapper = Enumerable.Range(1, 1000).Select(x => new NumberWrapper(x)).ToList();

            _stringsWrapper = Enumerable.Range(1, 1000).Select(x => new StringWrapper(x.ToString())).ToList();

        }

        [Benchmark]
        public int FindNumber()
        {
            return _numbers.Find(x => x == 500);
        }

        [Benchmark]
        public int FirstOrDefaultNumber()
        {
            return _numbers.FirstOrDefault(x => x == 500);
        }

        [Benchmark]
        public NumberWrapper FindNumberWrapper()
        {
            return _numbersWrapper.Find(x => x.Number == 500);
        }

        [Benchmark]
        public NumberWrapper FirstOrDefaultNumberWrapper()
        {
            return _numbersWrapper.FirstOrDefault(x => x.Number == 500);
        }

        [Benchmark]
        public string FindString()
        {
            return _strings.Find(x => x == "500");
        }

        [Benchmark]
        public string FirstOrDefaultString()
        {
            return _strings.FirstOrDefault(x => x == "500");
        }

        [Benchmark]
        public StringWrapper FindStringWrapper()
        {
            return _stringsWrapper.Find(x => x.String == "500");
        }

        [Benchmark]
        public StringWrapper FirstOrDefaultStringWrapper()
        {
            return _stringsWrapper.FirstOrDefault(x => x.String == "500");
        }

    }
}
