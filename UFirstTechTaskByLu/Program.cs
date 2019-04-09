using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UFirstTechTaskByLu.Models;
using UFirstTechTaskByLu.Services;

namespace UFirstTechTaskByLu
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<string> argumentsList = args.OfType<string>().ToList();

            var parsedLogs = new List<ParsedLog>();

            Console.WriteLine("Please enter the input file location");
            string inputFileLocation = Console.ReadLine();

            while (!Directory.Exists(inputFileLocation))
            {
                Console.WriteLine("The INPUT location is not valid or not found, please enter the valid existing INPUT file location");
                inputFileLocation = Console.ReadLine();
            }

            var lines = File.ReadAllLines(inputFileLocation + "\\input.txt");

            Console.WriteLine("Please enter the output file location");
            string outputFileLocation = Console.ReadLine();
            
            while (!Directory.Exists(outputFileLocation))
            {
                Console.WriteLine("The OUTPUT location is not valid or not found, please enter the valid existing OUTPUT file location");
                outputFileLocation = Console.ReadLine();
            }

            foreach (string line in lines)
            {
                parsedLogs.Add(Serializer.Deserialize(line));
            }

            var jsonString = JsonConvert.SerializeObject(parsedLogs, Formatting.Indented);

            File.WriteAllText(Path.Combine(outputFileLocation, "output.json"), jsonString);

            Console.WriteLine("{0} lines were parsed and added into JSON-Array in the following location: {1}\\output.json", parsedLogs.Count, outputFileLocation);
            Console.ReadLine();
        }
    }
}