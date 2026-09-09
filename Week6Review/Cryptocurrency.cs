using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Week6Review
{

    public class TradeException : Exception
    {
        public TradeException(string message) : base(message) { }
    }

    public class UnsupportedCoinException : TradeException
    {
        public UnsupportedCoinException(string message) : base(message) { }
    }

    public class InvalidTradeTypeException : TradeException
    {
        public InvalidTradeTypeException(string message) : base(message) { }
    }

    public class InvalidQuantityException : TradeException
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    public class InsufficientQuantityException : TradeException
    {
        public InsufficientQuantityException(string message) : base(message) { }
    }

    public class DuplicateTradeException : TradeException
    {
        public DuplicateTradeException(string message) : base(message) { }
    }
    public class Trade
    {
        public string TradeId { get; set; }
        public string CoinSymbol { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public double PriceUsd { get; set; }
        public Trade() { }
        public Trade(string tradeIdn, string coinSymbol, string type, double quantity, double priceUsd)
        {
            TradeId = tradeIdn;
            CoinSymbol = coinSymbol;
            Type = type;
            Quantity = quantity;
            PriceUsd = priceUsd;
        }
    }
    public class Coin
    {
        public string Symbol { get; set; }
        public double FeePercent { get; set; }
    }
    public class Cryptocurrency
    {
        private Dictionary<string, double> holdings;
        private Dictionary<string, double> coinFees;
        private HashSet<string> tradeIds;
        private List<TradeResult> ledger;

        public Cryptocurrency()
        {
            holdings = new Dictionary<string, double>();
            coinFees = new Dictionary<string, double>();
            tradeIds = new HashSet<string>();
            ledger = new List<TradeResult>();
        }

        public void LoadCoinFees(string filePath)
        {
            string jsonFile = File.ReadAllText(filePath);

            JObject jsonData = JObject.Parse(jsonFile);

            JArray coins = (JArray)jsonData["coins"];

            foreach (JObject coin in coins)
            {
                string symbol = coin["symbol"].ToString();
                double feePercent = (double)coin["feePercent"];

                coinFees[symbol] = feePercent;
                holdings[symbol] = 0;
            }
        }
        public void ValidateCoin(string symbol)
        {
            if (!coinFees.ContainsKey(symbol))
            {
                throw new UnsupportedCoinException($"{symbol} is not valid");
            }
        }

        public void ValidateTradeType(string type)
        {
            if (type != "BUY" && type != "SELL")
            {
                throw new InvalidTradeTypeException(
                    $"{type} is not a valid trade type."
                );
            }
        }

        public void ValidateQuantity(double quantity)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException("Quantity must be greater than zero.");
            }
        }

        public void ValidatePrice(double price)
        {
            if (price <= 0)
            {
                throw new TradeException("Price must be greater than zero.");
            }
        }

        public void ValidateDuplicateTrade(string tradeId)
        {
            if (tradeIds.Contains(tradeId))
            {
                throw new DuplicateTradeException($"{tradeId} is a duplicate trade");
            }
        }

        public void AddTradeId(string tradeId)
        {
            tradeIds.Add(tradeId);
        }

        public TradeResult ProcessTrade(Trade trade)
        {
            ValidateDuplicateTrade(trade.TradeId);
            ValidateCoin(trade.CoinSymbol);
            ValidateTradeType(trade.Type);
            ValidateQuantity(trade.Quantity);
            ValidatePrice(trade.PriceUsd);
            if (trade.Type == "BUY")
            {
                holdings[trade.CoinSymbol] += trade.Quantity;
            }
            else if (trade.Type == "SELL")
            {
                if (holdings[trade.CoinSymbol] < trade.Quantity)
                {
                    throw new InsufficientQuantityException(
                        $"Not enough {trade.CoinSymbol} to sell."
                    );
                }

                holdings[trade.CoinSymbol] -= trade.Quantity;
            }

            AddTradeId(trade.TradeId);

            TradeResult result = CalculateTrade(trade);

            ledger.Add(result);

            return CalculateTrade(trade);
        }

        public void LedgerJson()
        {
            string json = JsonConvert.SerializeObject(
                ledger,
                Formatting.Indented);

            File.WriteAllText("ledger.json", json);
        }

        public double GetHolding(string symbol)
        {
            if (holdings.ContainsKey(symbol))
            {
                return holdings[symbol];
            }
            return 0;
        }


        public static void CoinValidation()
        {
            using (StreamReader sr = new StreamReader("trades.csv"))
            using (CsvReader csr = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var records = csr.GetRecords<Trade>().ToList();

                HashSet<string> set = new HashSet<string>();

                foreach (var record in records)
                {
                    string tradeId = record.TradeId;
                    string coinSymbol = record.CoinSymbol;
                    string type = record.Type;
                    double quantity = record.Quantity;
                    double priceUsd = record.PriceUsd;

                    if (set.Contains(tradeId))
                        throw new DuplicateTradeException(
                            $"{tradeId} is duplicate");

                    set.Add(tradeId);

                    if (!IsValidTradeId(tradeId))
                        throw new TradeException(
                            $"{tradeId} is not valid");

                    if (!IsValidTradeType(coinSymbol))
                        throw new UnsupportedCoinException(
                            $"{coinSymbol} is not valid");

                    if (!IsValidCoinType(type))
                        throw new InvalidTradeTypeException(
                            $"{type} is not valid");

                    if (!IsValidQuantity(quantity))
                        throw new InvalidQuantityException(
                            $"{quantity} is not valid");

                    if (!IsValidPriceUsd(priceUsd))
                        throw new TradeException(
                            $"{priceUsd} is not valid");
                }
            }
        }

        public static bool IsValidTradeId(string tradeId)
        {
            if (string.IsNullOrEmpty(tradeId))
                return false;

            if (!tradeId.StartsWith("TR"))
                return false;

            if (tradeId.Length <= 2)
                return false;

            for (int i = 2; i < tradeId.Length; i++)
            {
                if (!char.IsDigit(tradeId[i]))
                    return false;
            }

            return true;
        }

        public static bool IsValidTradeType(string type)
        {
            if (type == "BUY" || type == "SELL")
                return true;

            return false;
        }

        public static bool IsValidCoinType(string coinSymbol)
        {
            if (coinSymbol == "ETH" ||
                coinSymbol == "BTC" ||
                coinSymbol == "SOL")
                return true;

            return false;
        }

        public static bool IsValidQuantity(double quantity)
        {
            if (quantity <= 0)
                return false;

            return true;
        }

        public static bool IsValidPriceUsd(double priceUsd)
        {
            if (priceUsd <= 0)
                return false;

            return true;
        }

        public bool IsSellAvailable(string coinSymbol, double quantity)
        {
            if (!holdings.ContainsKey(coinSymbol))
                return false;

            if (holdings[coinSymbol] >= quantity)
                return true;

            return false;
        }

        public void UpdateHoldings(string coinSymbol, string type, double quantity)
        {
            if (!holdings.ContainsKey(coinSymbol))
            {
                holdings[coinSymbol] = 0;
            }

            if (type == "BUY")
            {
                holdings[coinSymbol] += quantity;
            }
            else if (type == "SELL")
            {
                holdings[coinSymbol] -= quantity;
            }
        }
        public TradeResult CalculateTrade(Trade trade)
        {
            double tradeValue = trade.Quantity * trade.PriceUsd;

            double feePercent = coinFees[trade.CoinSymbol];

            double fee = tradeValue * feePercent / 100;

            double net = tradeValue - fee;

            return new TradeResult
            {
                TradeId = trade.TradeId,
                CoinSymbol = trade.CoinSymbol,
                Type = trade.Type,
                Quantity = trade.Quantity,
                PriceUsd = trade.PriceUsd,
                TradeValue = tradeValue,
                Fee = fee,
                Net = net
            };
        }

        public static TradeResult Calculations(Trade trade)
        {
            string jsonFile = File.ReadAllText("coins.json");

            JObject jsonData = JObject.Parse(jsonFile);

            JArray coins = (JArray)jsonData["coins"];

            double feePercent = 0;

            foreach (JObject coin in coins)
            {
                if (coin["symbol"].ToString() == trade.CoinSymbol)
                {
                    feePercent = (double)coin["feePercent"];
                    break;
                }
            }

            double tradeValue = trade.Quantity * trade.PriceUsd;

            double fee = tradeValue * feePercent / 100;

            double net = tradeValue - fee;

            return new TradeResult
            {
                TradeId = trade.TradeId,
                CoinSymbol = trade.CoinSymbol,
                Type = trade.Type,
                Quantity = trade.Quantity,
                PriceUsd = trade.PriceUsd,
                TradeValue = tradeValue,
                Fee = fee,
                Net = net
            };
        }

        public byte[] WriteTrade(Trade trade)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                writer.Write(trade.TradeId);
                writer.Write(trade.CoinSymbol);
                writer.Write(trade.Type);
                writer.Write(trade.Quantity);
                writer.Write(trade.PriceUsd);

                writer.Flush();

                return ms.ToArray();
            }
        }

        public Trade ReadTrade(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                string tradeId = reader.ReadString();
                string coinSymbol = reader.ReadString();
                string type = reader.ReadString();
                double quantity = reader.ReadDouble();
                double priceUsd = reader.ReadDouble();

                return new Trade(
                    tradeId,
                    coinSymbol,
                    type,
                    quantity,
                    priceUsd
                );
            }
        }
        public void SaveTradeToFile(Trade trade, string filePath)
        {
            byte[] data = WriteTrade(trade);

            using (FileStream fs =
                new FileStream(filePath, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        public Trade ReadTradeFromFile(string filePath)
        {
            using (FileStream fs =
                new FileStream(filePath, FileMode.Open))
            {
                byte[] data = new byte[fs.Length];

                fs.Read(data, 0, data.Length);

                return ReadTrade(data);
            }
        }

        public void AuditLog(TradeResult result)
        {
            string log =
                $"{result.TradeId},{result.CoinSymbol}," +
                $"{result.Type},{result.Quantity}," +
                $"{result.PriceUsd},{result.TradeValue}," +
                $"{result.Fee},{result.Net}" +
                Environment.NewLine;

            byte[] data = Encoding.UTF8.GetBytes(log);

            using FileStream fs =
                new FileStream(
                    "trade_audit.log",
                    FileMode.Append,
                    FileAccess.Write);

            using BufferedStream bs =
                new BufferedStream(fs);

            bs.Write(data, 0, data.Length);
        }
    }

    public class TradeResult
    {
        public string TradeId { get; set; }
        public string CoinSymbol { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public double PriceUsd { get; set; }
        public double TradeValue { get; set; }
        public double Fee { get; set; }
        public double Net { get; set; }
    }
}
