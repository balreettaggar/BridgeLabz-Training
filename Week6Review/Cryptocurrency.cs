using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Week6Review;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Week6Review
{
    public class TradeException : Exception
    {
        public TradeException(string message) : base(message) { }
    }

    public class UnsupportedCoinException : Exception
    {
        public UnsupportedCoinException(string message) : base(message) { }
    }

    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message) : base(message) { }
    }

    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    public class InsufficientQuantityException : Exception
    {
        public InsufficientQuantityException(string message) : base(message) { }
    }

    public class DuplicateTradeException : Exception
    {
        public DuplicateTradeException(string message) : base(message) { }
    }
    public class Coin
    {
        public string TradeIdn { get; set; }
        public string CoinSymbol { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public double PriceUsd { get; set; }

        public Coin() { }
        public Coin(string tradeIdn, string coinSymbol, string type, double quantity, double priceUsd)
        {
            TradeIdn = tradeIdn;
            CoinSymbol = coinSymbol;
            Type = type;
            Quantity = quantity;
            PriceUsd = priceUsd;
        }
    }
    public class Cryptocurrency
    {
        public static void CryptocurrencyMethod()
        {
            List<Coin> coinList = new List<Coin>()
            {
                new Coin("TR1","BTC","BUY",0.5d, 60000),
                new Coin("TR2", "ETH", "SELL", 2d, 3000),
                new Coin("TR3", "DOGE9", "BUY", 1000d, 0.1),
                new Coin ("TR4", "BTC", "SELL", 0, 61400),
                new Coin("TR5", "SOL", "BUY", 10, 140)
            };

            using (StreamWriter sr = new StreamWriter("coins.csv"))
            using (CsvWriter csw = new CsvWriter(sr, CultureInfo.InvariantCulture))
                csw.WriteRecords<Coin>(coinList);
        }

        public static void CoinValidation()
        {
            using (StreamReader sr = new StreamReader("coins.csv"))
            using (CsvReader csr = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var records = csr.GetRecords<Coin>().ToList();
                HashSet<string>set = new HashSet<string>();
                foreach (var record in records)
                {
                    string tradeId = record.TradeIdn;
                    string coinSymbol = record.CoinSymbol;
                    string type = record.Type;
                    double quantity = record.Quantity;
                    double priceUsd = record.PriceUsd;

                    try
                    {
                        if (set.Contains(tradeId)) throw new DuplicateTradeException($"{tradeId} is duplicate");
                        else set.Add(tradeId);
                    }
                    catch(DuplicateTradeException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        if (IsValidTradeId(tradeId)) continue;
                        else throw new TradeException($"{tradeId} is not valid");
                    }
                    catch (TradeException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        if (isValidTradeType(coinSymbol)) continue;
                        else throw new UnsupportedCoinException($"{coinSymbol} is not valid");
                    }
                    catch (UnsupportedCoinException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        if (isValidCoinType(type)) continue;
                        else throw new InvalidTypeException($"{type} is not valid");
                    }
                    catch (InvalidTypeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    try
                    {
                        if (isValidQuantity(quantity)) continue;
                        else throw new InvalidQuantityException($"{quantity} is not valid");
                    }
                    catch (InvalidQuantityException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    try
                    {
                        if (isValidPriceUsd(priceUsd)) continue;
                        else throw new InvalidQuantityException($"{priceUsd} is not valide");
                    }
                    catch (InvalidQuantityException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            }

        }

        public static bool IsValidTradeId(string tradeId)
        {
            if (tradeId == null) return false;
            if (tradeId.Length < 3) return false;
            int num = (int)tradeId[2];
            if (num < 0) return false;
            return true;
        }

        public static bool isValidTradeType(string coinSymbol)
        {
            if (coinSymbol == "ETH" || coinSymbol == "BTC" || coinSymbol == "DOGE9" || coinSymbol == "SOL") return true;
            return false;
        }

        public static bool isValidCoinType(string coinType)
        {
            if (coinType == "BUY" || coinType == "SELL") return true;
            return false;
        }

        public static bool isValidQuantity(double quantity)
        {
            if (quantity < 0) return false;
            return true;
        }

        public static bool isValidPriceUsd(double priceUsd)
        {
            if(priceUsd < 0) return false;
            return true;
        }

        public static bool isSellAvailable(string type, double quantity)
        {
            if (quantity < 0) return false;
            if (type == null) return false;
            if (type == "SELL")
            {
                quantity -= 1;
                return true;
            }
            return false;
        }

        public static void AuditLog()
        {
            using FileStream fsw = new FileStream("trade_audit.log", FileMode.Create, FileAccess.Write);
            using FileStream fsr = new FileStream("coins.csv", FileMode.Open, FileAccess.Read);
            using BufferedStream bs = new BufferedStream(fsr);
            {
                byte[] buffer = new byte[1024];

                while(bs.Read(buffer, 0, buffer.Length) > 0)
                {
                    fsw.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void LedgerJson()
        {
            using (StreamWriter sw = new StreamWriter("ledger.json")) 
            using (StreamReader sr = new StreamReader("coins.csv"))
            using (CsvReader csr = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var allRecords = csr.GetRecords<Coin>().ToList();
                foreach (var record in allRecords)
                {
                    var entry = JsonConvert.SerializeObject(record);
                    sw.WriteLine(entry);
                }
            }

        }

        public static double Calculations(double quantity, double priceUsd)
        {
            string jsonFile = File.ReadAllText("coins.json");
            JObject jsonData = JObject.Parse(jsonFile);

            List<double>feepercents = new List<double>();
            while (jsonData != null)
            {
                feepercents.Add((double)jsonData["feepercent"]);
            }

            double tradeValue = quantity * priceUsd;
            double fee = tradeValue * feepercents[0] / 100;
            double net = tradeValue - fee;
            return fee;
        }
    }
}

//Problem — Cryptocurrency Trade Ledger & Integrity Engine

//Input
//trades.csv
//TradeId, CoinSymbol, Type, Quantity, PriceUsd
//TR1, BTC, BUY,0.5,60000
//TR2, ETH, SELL,2,3000
//TR3, DOGE9, BUY,1000,0.1
//TR4, BTC, SELL,0,61000
//TR5, SOL, BUY,10,140
//coins.json
//{
//  "coins": [
//    { "symbol": "BTC", "feePercent": 0.5 },
//    { "symbol": "ETH", "feePercent": 0.4 },
//    { "symbol": "SOL", "feePercent": 0.3 }
//  ]
//}Requirements
//Support BUY and SELL.
//Validate:
//Coin
//Trade type
//Quantity
//Price
//Duplicate TradeId
//SELL availability
//Calculate:
//TradeValue = Quantity × Price
//Fee = TradeValue × FeePercent / 100
//Net = TradeValue - Fee
//Maintain holdings and prevent selling more than available quantity.
//Use MemoryStream + BinaryWriter to encode:
//TradeId
//CoinSymbol
//Quantity
//PriceUsd
//Read it back using BinaryReader.
//Generate:
//ledger.json
//trade_audit.log
//Use BufferedStream for the audit log.
//ExceptionsTradeException
// ├── UnsupportedCoinException
// ├── InvalidTradeTypeException
// ├── InvalidQuantityException
// ├── InsufficientQuantityException
// └── DuplicateTradeExceptionNUnit Testing — Minimum 10 Tests
//Test:
//Valid BUY.
//Valid SELL.
//Unsupported coin.
//Invalid trade type.
//Zero quantity.
//Negative quantity.
//Zero/negative price.
//SELL greater than available holdings.
//Correct fee calculation.
//Duplicate TradeId.
//Holdings update after BUY.
//Holdings update after SELL.
//Binary integrity round-trip.
//Empty/header-only input.
