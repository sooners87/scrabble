using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * Data structure used here is dictionary (key, value) map.
             * Pro: constant time access
             * Time complexity: O(n) n is number of characters in a dictionary word.
             */


            if (args.Length != 2)
            {
                return;
            }

            var dictionaryFile = args[0];
            var valueFile = args[1];

            Dictionary<string, int> value = new Dictionary<string, int>();
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var line in File.ReadLines(valueFile))
            {
                string[] val = line.Split(" ");
                value[val[0]] = int.Parse(val[1]);
            }

            foreach (var line in File.ReadLines(dictionaryFile))
            {
                StringBuilder sb = new StringBuilder();

                Dictionary<string, int> tempHighestValues = new Dictionary<string, int>();
                int highestValueWordValue = 0;
                int currentValue = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    var c = sb.ToString() + line[i];

                    if (value.ContainsKey(c))
                    {
                        sb.Append(line[i]);
                        currentValue = value[c];
                    }
                    else
                    {
                        if (currentValue > highestValueWordValue)
                        {
                            highestValueWordValue = currentValue;
                            tempHighestValues = new Dictionary<string, int>();
                            tempHighestValues[sb.ToString()] = currentValue;
                        }
                        else if (currentValue == highestValueWordValue)
                        {
                            tempHighestValues[sb.ToString()] = currentValue;
                        }

                        sb.Clear();
                        sb.Append(line[i]);

                        if (value.ContainsKey(line[i].ToString()))
                        {
                            currentValue = value[line[i].ToString()];
                        }
                        else
                        {
                            currentValue = 0;
                        }
                    }
                }

                foreach(var v in tempHighestValues)
                {
                    result[v.Key] = v.Value;
                }
            }

            foreach (var r in result)
            {
                Console.WriteLine(r.Key + " " + r.Value);
            }

        }
    }
}
