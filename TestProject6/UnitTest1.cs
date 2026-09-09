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

            cc.LoadCoinFees(
                "D:\\BridgeLabz-Training\\Week6Review\\coins.json"
            );
        }


        [Test]
        public void Test1_UnsupportedCoin()
        {
            bool result = Cryptocurrency.IsValidCoinType("IND");
            Assert.IsFalse(result);
        }

        [Test]
        public void Test2_ValidTradeType()
        {
            bool result = Cryptocurrency.IsValidTradeType("SELL");
            Assert.IsTrue(result);
        }

        [Test]
        public void Test3_InvalidTradeType()
        {
            bool result = Cryptocurrency.IsValidTradeType("HOLD");
            Assert.IsFalse(result);
        }

        [Test]
        public void Test4_NegativeQuantity()
        {
            bool result = Cryptocurrency.IsValidQuantity(-10);
            Assert.IsFalse(result);
        }


        [Test]
        public void Test5_ZeroQuantity()
        {
            bool result = Cryptocurrency.IsValidQuantity(0);
            Assert.IsFalse(result);
        }


        [Test]
        public void Test6_NegativePrice()
        {
            bool result = Cryptocurrency.IsValidPriceUsd(-100);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test7_ValidTradeId()
        {
            bool result = Cryptocurrency.IsValidTradeId("TR10");
            Assert.IsTrue(result);
        }

        [Test]
        public void Test8_InvalidTradeId()
        {
            bool result = Cryptocurrency.IsValidTradeId("ABC");
            Assert.IsFalse(result);
        }

        [Test]
        public void Test9_CalculateTrade()
        {
            Trade trade = new Trade(
                "TR1",
                "BTC",
                "BUY",
                0.5,
                60000
            );

            TradeResult result = cc.CalculateTrade(trade);

            Assert.That(result.TradeValue, Is.EqualTo(30000));
            Assert.That(result.Fee, Is.EqualTo(150));
            Assert.That(result.Net, Is.EqualTo(29850));
        }

        [Test]
        public void Test10_BuyUpdatesHoldings()
        {
            Trade trade = new Trade("TR1","BTC","BUY", 0.5, 60000);

            cc.ProcessTrade(trade);

            Assert.IsTrue(cc.IsSellAvailable("BTC", 0.5));
        }

        [Test]
        public void Test11_SellUpdatesHoldings()
        {
            Trade buyTrade = new Trade("TR1","BTC","BUY",1,60000);

            Trade sellTrade = new Trade("TR2","BTC","SELL", 0.5,61000);

            cc.ProcessTrade(buyTrade);
            cc.ProcessTrade(sellTrade);

            Assert.IsTrue(cc.IsSellAvailable("BTC", 0.5));
            Assert.IsFalse(cc.IsSellAvailable("BTC", 0.6));
        }

        [Test]
        public void Test12_InsufficientSellThrowsException()
        {
            Trade trade = new Trade("TR1", "BTC", "SELL", 1, 60000);
            Assert.Throws<InsufficientQuantityException>(() => cc.ProcessTrade(trade));
        }
    }
}