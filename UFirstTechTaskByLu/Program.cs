using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UFirstTechTaskByLu.Models;
using UFirstTechTaskByLu.Services;

namespace UFirstTechTaskByLu
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parsedLogs = new List<ParsedLog>();

            var lines = File.ReadAllLines(@"../../Resources/epa-http.txt");
            foreach (string line in lines)
            {
                parsedLogs.Add(Serializer.Deserialize(line));
            }

            var jsonString = JsonConvert.SerializeObject(parsedLogs, Formatting.Indented);

            File.WriteAllText("jsonParsed.json", jsonString);

            Console.WriteLine("There were {0} lines.", parsedLogs.Count);
            Console.ReadLine();
        }
    }
}