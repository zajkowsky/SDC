using System;
using System.Collections.Generic;
using System.Linq;

namespace VeryExpensiveService
{
    //This is sample external service implementation - this class cannot be changed
    public class VeryExpensiveService
    {
        public VeryExpensiveService()
        {
            TotalCost = 0;
        }
        public decimal TotalCost { get; private set; }

        /// each call for this function cost 10 EURO.
        ///this just simulate query to external system  
        public string GetTypeForClientId(int clientId)
        {
            TotalCost += 10;
            switch (clientId)
            {
                case 1: return "PRIV";
                case 2: return "FIRM";
                case 3: return "PRIV";

                default:
                    return "";
            }
        }
    }


    ///simple model class - do not change
    public class Transaction
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public int ClientId { get; set; }
        public string ClientType { get; set; }
        public bool IsSuspicious { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Value: {1}, ClientId: {2}, ClientType: {3}", Id, Value, ClientId, ClientType);
        }
    }


    public class Program
    {
        private static VeryExpensiveService service = new VeryExpensiveService();

        public static void Main(string[] args)
        {
            var transactions = GetTestData().ToList();

            Step1(transactions);
            Step2(transactions);
            Step3(transactions);
            Step4(transactions);

            Console.ReadLine();
        }

        private static void Step1(List<Transaction> transactions)
        {
            transactions.ForEach(SetClientType);
            transactions.ForEach(x => Console.WriteLine(x));
            Console.WriteLine(string.Format("Step 1 cost: {0}", service.TotalCost));
        }

        private static void SetClientType(Transaction transaction)
        {
            transaction.ClientType = service.GetTypeForClientId(transaction.ClientId);
        }

        private static void Step2(List<Transaction> transactions)
        {
            Console.WriteLine(string.Format("Total sum of the transactions: {0}", transactions.Sum(x => x.Value)));
        }

        private static void Step3(List<Transaction> transactions)
        {
            Console.WriteLine("Total sum of the transactions per client:");
            var groups = transactions.GroupBy(x => x.ClientId);
            foreach (var group in groups)
            {
                Console.WriteLine(string.Format("ClientId: {0}, ToatlSum: {1}", group.Key, group.Sum(x => x.Value)));
            }
        }

        private static void Step4(List<Transaction> transactions)
        {
            transactions.ForEach(SetIsSuspicious);
            Console.WriteLine("Suspicious transactions:");
            transactions.Where(x => x.IsSuspicious).ToList().ForEach(x => Console.WriteLine(x));
        }

        private static void SetIsSuspicious(Transaction transaction)
        {
            if (transaction.ClientType == "PRIV" && transaction.Value > 200)
            {
                transaction.IsSuspicious = true;
                return;
            }

            if (transaction.ClientType == "FIRM" && transaction.Value > 300)
            {
                transaction.IsSuspicious = true;
                return;
            }

            transaction.IsSuspicious = false;
        }

        /// simple function to generate data
        private static IEnumerable<Transaction> GetTestData()
        {
            yield return new Transaction { Id = 1, Value = 100, ClientId = 1, };
            yield return new Transaction { Id = 2, Value = 400, ClientId = 2 };
            yield return new Transaction { Id = 3, Value = -101, ClientId = 3 };
            yield return new Transaction { Id = 4, Value = -100, ClientId = 1 };
            yield return new Transaction { Id = 5, Value = 299, ClientId = 2 };
            yield return new Transaction { Id = 6, Value = -100, ClientId = 3 };
            yield return new Transaction { Id = 7, Value = -200, ClientId = 1 };
            yield return new Transaction { Id = 8, Value = 201, ClientId = 2 };
            yield return new Transaction { Id = 9, Value = -200, ClientId = 3 };
            yield return new Transaction { Id = 10, Value = 700, ClientId = 1 };
            yield return new Transaction { Id = 11, Value = 300, ClientId = 2 };
            yield return new Transaction { Id = 12, Value = 60, ClientId = 3 };
        }
    }
}
