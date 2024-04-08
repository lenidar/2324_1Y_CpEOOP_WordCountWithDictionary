using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2324_1Y_CpEOOP_WordCountWithDictionary
{
    internal class Program
    {
        static Dictionary<string, int> _words = new Dictionary<string, int>();
        static int[] _counts = new int[] { 0, 0 }; // sentence, words

        static void Main(string[] args)
        {
            wordBreakdown(sentenceBreakdown(FileRead("Gilgamesh.txt")));

            Console.WriteLine($"Sentence count : {_counts[0]}\nTotal Word Count : {_counts[1]}\n\n");

            Console.WriteLine("Sorting the frequency...");
            _words = sortDictionaryByValue();
            Console.WriteLine("Done sorting!");

            foreach(KeyValuePair<string, int> _w in _words)
                Console.WriteLine($"{_w.Key} => {_w.Value}");

            Console.ReadKey();
        }

        static string[] sentenceBreakdown(string content)
        {
            string[] sentence = content.ToLower().Split(new char[] { '.', '?', '!' });
            _counts[0] = sentence.Length;

            return sentence;
        }

        static void wordBreakdown(string[] sentences)
        {
            string[] words = new string[] { };

            foreach(string sentence in sentences)
            {
                words = sentence.Split(' ');
                foreach(string word in words)
                {
                    if (word.Length > 0)
                    {
                        _counts[1]++;

                        if(_words.ContainsKey(word))
                            _words[word] += 1;
                        else
                            _words[word] = 1;
                    }
                }
            }
        }

        static Dictionary<string, int> sortDictionaryByValue()
        {
            Dictionary<string, int> sortedDic = new Dictionary<string, int>();

            int valueToLookFor = 0;
            string keyToRemove = "";
            bool foundFlag = false;

            foreach (KeyValuePair<string, int> _w in _words)
                if (valueToLookFor < _w.Value)
                {
                    valueToLookFor = _w.Value;
                    keyToRemove = _w.Key;
                }

            sortedDic[keyToRemove] = _words[keyToRemove];
            _words.Remove(keyToRemove);

            while (_words.Count > 0 && valueToLookFor > 0)
            {
                foundFlag = false;
                foreach (KeyValuePair<string, int> _w in _words)
                {
                    if (valueToLookFor == _w.Value)
                    {
                        sortedDic[_w.Key] = _words[_w.Key];
                        _words.Remove(_w.Key);
                        //Console.WriteLine($"{sortedDic.Count} words have been sorted...");
                        foundFlag = true;
                        break;
                    }
                }
                if (!foundFlag)
                {
                    valueToLookFor--;
                    //Console.WriteLine($"Sorting words with frequency of {valueToLookFor}");
                }

            }

            return sortedDic;
        }

        static string FileRead(string fileName)
        {
            string outputString = "";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length > 0)
                            outputString += line;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Please check if the filename is correct or if the file is open.");
                Console.WriteLine($"Specific error message is: {e.Message}");
                outputString = "-1";
            }

            return outputString;
        }

        static void FileWrite(string fileName, string message, bool append)
        {
            using (StreamWriter sw = new StreamWriter(fileName, append))
            {
                sw.WriteLine(message);
            }
        }
    }
}
