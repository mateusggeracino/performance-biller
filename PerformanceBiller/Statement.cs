using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using PerformanceBiller.Models;
using PerformanceBiller.Models.Base;
using PerformanceBiller.Repository;

namespace PerformanceBiller
{
    public class Statement
    {
        private readonly IJsonReader _jsonReader;

        public Statement(IJsonReader jsonReader)
        {
            _jsonReader = jsonReader;
        }

        public string Run(JObject invoiceObj, JObject plays)
        {
            Amount amount;
            var invoice = _jsonReader.GetInvoice();

            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            var cultureInfo = new CultureInfo("en-US");

            foreach (var performance in invoice.Performances) { //para cada performance existente
                var play = _jsonReader.GetPlayById(performance.PlayId);
                switch (play.Type) { //Obtem tipo do play
                    case PlayType.Tragedy: // se for tragedy
                        amount = new Tragedy();
                        amount.Calculate(performance);
                        break;
                    case PlayType.Comedy:
                        amount = new Comedy();
                        amount.Calculate(performance);
                        break;
                    default:
                        throw new Exception($"unknown type: { play.Type}");
                }

                // add volume credits
                volumeCredits += Math.Max(performance.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (PlayType.Comedy == play.Type) volumeCredits += performance.Audience / 5;
                // print line for this order
                result += $" {play.Name}: {(amount.AmountValue / 100).ToString("C", cultureInfo)} ({performance.Audience} seats)\n";
                amount.TotalAmount += amount.AmountValue;
             }
             result += $"Amount owed is {(amount.TotalAmount / 100).ToString("C", cultureInfo)}\n";
             result += $"You earned {volumeCredits} credits\n";

             return result;
        }
    }
}
