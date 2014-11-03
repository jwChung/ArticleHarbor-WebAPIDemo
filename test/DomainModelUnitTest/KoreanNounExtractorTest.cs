namespace DomainModelUnitTest
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using DomainModel;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Xunit;

    public class KoreanNounExtractorTest
    {
        [Test(RunOn.CI)]
        public IEnumerable<ITestCase> ExtractCorrectlyExtractsOnlyNouns()
        {
            var testData = new[]
            {
                new
                {
                    Input = "문장에서 단어를 추출합니다.",
                    Output = new[] { "문장", "단어", "추출" }
                },
                new
                {
                    Input = "임의의 단어도 가능하나?.",
                    Output = new[] { "임의", "단어", "가능" }
                }
            };

            return TestCases.WithArgs(testData).Create(
                d =>
                {
                    var actual = KoreanNounExtractor.Execute(d.Input);
                    Assert.Equal(d.Output, actual);
                });
        }

        [Test]
        public void ExecuteWithNullDocumentThrows()
        {
            Assert.Throws<ArgumentNullException>(() => KoreanNounExtractor.Execute(null).ToArray());
        }

        [Test(RunOn.CI)]
        public void SutShouldBeThreadSafe()
        {
            string input = "짧은 문장.";
            var exceptions = new ConcurrentBag<Exception>();
            var threads = new Thread[15];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() =>
                {
                    try
                    {
                        KoreanNounExtractor.Execute(input).ToArray();
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                });

            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();

            for (int i = 0; i < threads.Length; i++)
                threads[i].Join();
            Assert.Empty(exceptions);
        }
    }
}