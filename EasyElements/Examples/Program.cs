using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyElements;
using EasyElements.Configs;

namespace EasyElementsExample
{
    class Program
    {
        private static Stopwatch _stopwatch;

        private static void Main(string[] args)
        {
            _stopwatch = Stopwatch.StartNew();

            //Open configuration file
            var configReader = new ConfigReader("config.xml");
            var config = configReader.Open();

            ShowInfo("Config opened");

            //Open elements.data
            var elementsReader = new ElementsReader("elements.data", config);
            var elementsData = elementsReader.Open();

            ShowInfo("Elements.data opened");

            Console.WriteLine(elementsData.GetFreeID(29874, 65000));

            ShowInfo("getfreeid");

            /*
            //Get data
            foreach (DataRow row in elementsData.Data.Tables["012 - MEDICINE SUB TYPE"].Rows)
                Console.WriteLine($"ID: {row["ID"]} \t Name: {row["Name"]}");

            ShowInfo("Get data completed");

            //Get type row and list
            foreach (var confList in elementsData.ConfigForThisElements.Lists)
            {
                var elementsList = elementsData.Data.Tables[confList.Name];
                Console.WriteLine($"Count {confList.Caption}: {elementsList.Rows.Count}");
            }


            //Save elements.data
            var elementsWriter = new ElementsWriter(elementsReader);
            elementsWriter.Save();

            ShowInfo("Elements.data saved");


    */

            _stopwatch.Stop();
            Console.ReadKey();
        }

        private static void ShowInfo(string text)
        {
            _stopwatch.Stop();

            Console.WriteLine($"{text}: {_stopwatch.Elapsed}");

            _stopwatch = Stopwatch.StartNew();
        }
    }
}
