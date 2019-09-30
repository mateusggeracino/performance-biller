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
            var invoice = _jsonReader.GetData<Invoice>(@"C:\repository\git\performance-biller\PerformanceBiller.Tests\invoices.json");

            var result = $"Statement for {invoice.Customer}\n";
            
            foreach (var performance in invoice.Performances) { 
                var play = _jsonReader.GetPlayById(performance.PlayId);

                amount = SelectType(play.Type);
                amount.Calculate(performance);

                // add volume credits
                amount.AddVolumeCredits(performance);

                // add extra credit for every ten comedy attendees
                if(play.Type == PlayType.Comedy) ((Comedy)amount).AddExtraVolumeCredits(performance);

                // print line for this order
                result += $" {play.Name}: {amount.DividePorCem()} ({performance.Audience} seats)\n";
                amount.SumAmount(amount.AmountValue);
             }

            //result += $"Amount owed is {amount.DividePorCem()}\n";
            //result += $"You earned {amount.VolumeCredits} credits\n";

            return result;
        }

        public Amount SelectType(PlayType type)
        {
            switch (type)
            {
                case PlayType.Tragedy: 
                    return new Tragedy();
                case PlayType.Comedy:
                    return new Comedy();
                default:
                    throw new Exception("Error");
            }
        }
    }
}
