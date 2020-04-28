using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpAdvanced.ConcurrentCollections
{
    public static class ConcurrentDictionaryExample
    {
        public static void RunExample1()
        {
            var dictionary = new ConcurrentDictionary<string, int>();

            for (int i = 1; i <= 1000; i++)
            {
                dictionary.TryAdd($"item-{i}", i);
            }

            Parallel.ForEach(dictionary, (kv, po) =>
             {
                 try
                 {
                     var record = kv as Nullable<KeyValuePair<string, int>>;
                     var key = record.Value.Key;
                     var value = record.Value.Value;
                     var newValue = value;

                     Console.WriteLine($"Task: {Task.CurrentId} has started with key {key} and value {value}");

                     dictionary.TryUpdate(key, ++newValue, value);

                     Console.WriteLine($"Task: {Task.CurrentId} has finished with key {key} and value {dictionary[key]}");
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                 }

             });

            foreach (var item in dictionary)
            {
                var message = $"{item.Key} : {item.Value}";
                if (!ValidateItem(item))
                {
                    throw new InvalidOperationException(message);
                }
                Console.WriteLine(message);

            }
        }

        public static void RunExample2()
        {
            var dictionary = new ConcurrentDictionary<string, int>();

            Parallel.For(1, 1001, (i) => { dictionary.TryAdd($"item-{i}", i); });

            Parallel.ForEach(dictionary, (kv) =>
            {
                try
                {
                    var record = kv as Nullable<KeyValuePair<string, int>>;
                    var key = record.Value.Key;
                    var value = record.Value.Value;
                    var newValue = value;

                    Console.WriteLine($"Task: {Task.CurrentId} has started with key {key} and value {value}");

                    dictionary.TryUpdate(key, ++newValue, value);

                    Console.WriteLine($"Task: {Task.CurrentId} has finished with key {key} and value {dictionary[key]}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            Parallel.ForEach(dictionary, (kv) =>
             {
                 var message = $"{kv.Key} : {kv.Value}";
                 if (!ValidateItem(kv))
                 {
                     throw new InvalidOperationException(message);
                 }
                 Console.WriteLine(message);
             });

        }

        private static bool ValidateItem(KeyValuePair<string, int> kv)
        {
            var itemOrder = Convert.ToInt32(kv.Key.Split('-')[1]);
            return itemOrder == (kv.Value - 1);
        }

    }
}
