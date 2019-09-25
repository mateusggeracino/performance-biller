using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using PerformanceBiller.Models;
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
            var invoice = _jsonReader.GetInvoice();
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            var cultureInfo = new CultureInfo("en-US");

            foreach (var performance in invoice.Performances) { //para cada performance existente
                var play = _jsonReader.GetPlayById(performance.PlayId);
                var thisAmount = 0;
                switch (play.Type) { //Obtem tipo do play
                    case PlayType.Tragedy: // se for tragedy
                        thisAmount = 40000;
                        if (performance.Audience > 30) { //se valor da audience da performance for maior que 30
                            thisAmount += 1000 * (performance.Audience - 30);
                        }
                        break;
                    case PlayType.Comedy:
                        thisAmount = 30000;
                        if (performance.Audience > 20) {
                            thisAmount += 10000 + 500 * (performance.Audience - 20);
                        }
                        thisAmount += 300 * performance.Audience;
                        break;
                    default:
                        throw new Exception($"unknown type: { play.Type}");
                }
                // add volume credits
                volumeCredits += Math.Max(performance.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (PlayType.Comedy == play.Type) volumeCredits += performance.Audience / 5;
                // print line for this order
                result += $" {play.Name}: {(thisAmount/100).ToString("C", cultureInfo)} ({performance.Audience} seats)\n";
                totalAmount += thisAmount;
             }
             result += $"Amount owed is {(totalAmount/100).ToString("C", cultureInfo)}\n";
             result += $"You earned {volumeCredits} credits\n";

             return result;
        }
    }
}
