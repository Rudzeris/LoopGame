using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Generator
{
    static public class Helper 
    {
        public const float zoneOffset = 29;
        public const int maxLEVEL = 1;

        public static List<T> ShuffleList<T>(List<T> list)
        {
            System.Random random = new System.Random();
            List<T> shuffledList = new List<T>(list);
            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }
            return shuffledList;
        }

        public static List<List<T>> SplitList<T>(List<T> list, int numberOfParts)
        {
            List<List<T>> splitLists = new List<List<T>>();
            int partSize = list.Count / numberOfParts;

            for (int i = 0; i < numberOfParts; i++)
            {
                int startIndex = i * partSize;
                int endIndex = (i == numberOfParts - 1) ? list.Count : startIndex + partSize;
                splitLists.Add(list.GetRange(startIndex, endIndex - startIndex));
            }

            return splitLists;
        }
    }


}

