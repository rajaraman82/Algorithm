using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution {

    // Complete the pairs function below.
    static int pairs(int k, int[] arr) {

 return EligibleCount(k, arr);

    }

private static int EligibleCount(int k, int[] arr)
        {
            SortedDictionary<int, int> sortedSet = DoSort(arr);
            int countPairs= NumberOfPairs(sortedSet,k);
            return countPairs;
        }
     
         
        private static SortedDictionary<int,int> DoSort(int[] arr)
        {
            SortedDictionary<int, int> sortedSet = new SortedDictionary<int, int>();

            for(int i=0; i< arr.Length;i++)
            {
                if(!sortedSet.ContainsKey(arr[i]))
                {
                    sortedSet.Add(arr[i],1);
                }
            }

            return sortedSet;
        }

        private static int NumberOfPairs(SortedDictionary<int, int> sortedSet,int k)
        {
            int numberOfPairs = 0;

            foreach(KeyValuePair<int,int> keyValuePair in sortedSet)
            {
                if (sortedSet.ContainsKey(keyValuePair.Key + k))
                {
                    numberOfPairs = numberOfPairs + 1;
                }
            }

            return numberOfPairs;
        }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0]);

        int k = Convert.ToInt32(nk[1]);

        int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
        ;
        int result = pairs(k, arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
