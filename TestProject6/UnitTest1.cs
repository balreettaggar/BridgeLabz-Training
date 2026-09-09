using CsvHelper;
using System.Globalization;
using Week6Review;

namespace TestProject6
{
    public class Tests
    {
        Cryptocurrency cc;
        [SetUp]
        public void Setup()
        {
            cc = new Cryptocurrency();
        }

        [Test]
        public void Test1()
        {
            bool result = Cryptocurrency.isValidCoinType("IND");
            Assert.IsFalse(result);
        }

        [Test]
        public void Test2()
        {
            bool result = Cryptocurrency.isValidCoinType("SELL");
            Assert.IsTrue(result);
        }

        [Test]
        public void Test3()
        {
            bool result = Cryptocurrency.isSellAvailable("BUY", 20);
            Assert.IsTrue(!result);

        }

        [Test]
        public void Test4()
        {
            using (StreamReader sr = new StreamReader("D:\\BridgeLabz-Training\\Week6Review\\coins.csv"))
            using (CsvReader csr = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var records = csr.GetRecords<Coin>().ToList();
                int cnt = 0;
                foreach (var record in records) ++cnt;
                bool res = cnt == 5;
                Assert.IsTrue(res);
            };

        }

        [Test]
        public void Test5()
        {
            bool result = Cryptocurrency.isValidQuantity(-10);
            Assert.IsFalse(result);
        }
        
    }
}