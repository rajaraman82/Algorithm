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

    // Complete the activityNotifications function below.
    static int activityNotifications(int[] expenditure, int d) {

            return ProcessExpenditure(expenditure, d);
    }

        private static int ProcessExpenditure(int[] expenditure, int days)
        {
            int numberOfClientNotification = 0;

            //int numberOfExpenditureCheck = expenditure.Count() - days;
           // int[] getNumberOfLeadingDays = new int[days];

            decimal getMedian =0.00m;
            int startIndex = 0;
            int endIndex = days;

            int middleElement1Index = (days / 2) - 1;
            int middleElement2Index = (days / 2);
            int countMode = days % 2;

            //Dictionary<int, int> inputRecords = new Dictionary<int, int>();
            // Dictionary<int, int> medianRecords = new Dictionary<int, int>();

            int[] sortExpenditure = new int[endIndex];

            SortedDictionary<int, int> keyValuePairs = new SortedDictionary<int, int>();

            for (int loop= startIndex; loop< endIndex; loop++)
            {
                //sortExpenditure[loop] = expenditure[loop];
               
               if(keyValuePairs.ContainsKey(expenditure[loop]))
                {
                    int value = keyValuePairs[expenditure[loop]];
                    keyValuePairs[expenditure[loop]] = value + 1;
                }
                else
                {
                    keyValuePairs.Add(expenditure[loop], 1);
                }

            }
            int index = 0;
            foreach (var valuePair in keyValuePairs)
            {
               
                for(int i=0; i<valuePair.Value;i++)
                {
                    sortExpenditure[index] = valuePair.Key;
                    index = index + 1;
                }
            }

            


            //List<int> listTempValue = getNumberOfLeadingDays.ToList();
            
            //int[] sortExpenditure = listTempValue.ToArray();
            // Array.Sort(sortExpenditure);
            int notificationCount = 0;
          
            for (int loopDay = 0; loopDay < expenditure.Count() - days; loopDay++)
            {
                int[] middleValue = new int[2];

                if (loopDay != 0)
                { 
                    
                        // RemoveAddOneExpenditure(ref sortExpenditure, expenditure[startIndex - 1], expenditure[endIndex-1], days);  

                        middleValue = RemoveAddOneExpenditureKey(middleElement1Index, middleElement2Index, expenditure[startIndex - 1], expenditure[endIndex - 1], ref keyValuePairs);

                   
                       
                }
                else
                {
                    middleValue = FetchMiddleValue(middleElement1Index, middleElement2Index, sortExpenditure);
                }
                   

                    getMedian = countMode==1 ? middleValue[1] : GetMedian(middleValue);

                    // notificationCount = CheckNotificationCount(expenditure[endIndex], getMedian * 2);

                    notificationCount = expenditure[endIndex] >= ( getMedian * 2) ? 1 : 0;
                

                numberOfClientNotification = numberOfClientNotification + notificationCount;

                startIndex = startIndex + 1;
                endIndex = endIndex + 1;
            }

            return numberOfClientNotification;
        }

        private static int[] RemoveAddOneExpenditureKey(int middleElement1Index, int middleElement2Index, int previousLeadingExpenditure
                                                   , int newTrailingExpenditure,ref SortedDictionary<int, int> keyValuePairs)
        {
             

            int[] middleElementValue = new int[2];

            if (keyValuePairs.ContainsKey(previousLeadingExpenditure))
            {
                int value = keyValuePairs[previousLeadingExpenditure];
                keyValuePairs[previousLeadingExpenditure] =  value - 1;
            }

            if (keyValuePairs.ContainsKey(newTrailingExpenditure))
            {
                int value = keyValuePairs[newTrailingExpenditure];
                keyValuePairs[newTrailingExpenditure] = value + 1;
            }
            else
            {
                keyValuePairs.Add(newTrailingExpenditure, 1);
            }

          
            int getKeypairValue = 0;
            bool isFirstValueFetched= false;
            foreach (var valuePair in keyValuePairs)
            {
                    getKeypairValue = getKeypairValue + valuePair.Value;
                   if (getKeypairValue > middleElement1Index)
                    {
                    int  minusValue= getKeypairValue - middleElement1Index;

                        if(minusValue > 1 )
                        {
                        if(isFirstValueFetched == false)
                            middleElementValue[0] = valuePair.Key;

                            middleElementValue[1] = valuePair.Key;
                            break;
                        }
                        else
                        {
                            isFirstValueFetched = true; 
                            middleElementValue[0] = valuePair.Key;
                        }
                        
                    


                       
                    }
                
            }

            return middleElementValue;


        }

        private static int[] FetchMiddleValue(int middleElement1Index, int middleElement2Index,int[] sortExpenditure)
        {
            int[] middleValue = new int[2];

            middleValue[0] = sortExpenditure[middleElement1Index] ;
            middleValue[1] = sortExpenditure[middleElement2Index] ;
 
            return middleValue;
        }

        private static void  RemoveAddOneExpenditure(ref int[] sortExpenditure, int previousLeadingExpenditure
                                                     ,int newTrailingExpenditure, int sortExpenditureLength)
        {
            int index =  RemoveOneExpenditure(ref sortExpenditure, previousLeadingExpenditure, newTrailingExpenditure
                                          ,   sortExpenditureLength);

            bool isPreviousIndexAvailable = true;
             
            int? previousIndexValue = null;
           
            if (index == 0)
            {
                isPreviousIndexAvailable = false; 
            }
            else
            {
                previousIndexValue = sortExpenditure[index - 1];
            }
 

            bool isMoveForward = true;

            if(isPreviousIndexAvailable && previousIndexValue > newTrailingExpenditure)
            {
                isMoveForward = false;
            }
            

            if(isMoveForward)
            {
                int iternation = sortExpenditureLength - index;
                
                for (int i = 0; i < iternation-1; i++)
                {
                    int Index1Value = sortExpenditure[index];
                    int Index2Value = 0;
                   
                     Index2Value = sortExpenditure[index + 1];

                    if (Index1Value > Index2Value)
                    {
                        sortExpenditure[index] = Index2Value;
                        sortExpenditure[index + 1] = Index1Value;
                    }
                    else
                    {
                        break;
                    }

                    //if(Index1Value == Index2Value)
                    //{
                    //    break;
                    //}
                    //else if(Index1Value > Index2Value)
                    //{
                    //    sortExpenditure[index] = Index2Value;
                    //    sortExpenditure[index + 1] = Index1Value;
                    //}
                    //else
                    //{
                    //    break;
                    //}
                    index = index + 1;
                }
            }
            else
            {
                for (int i = index; i > 0; i--)
                {
                    int Index1Value = sortExpenditure[i];
                    int Index2Value = sortExpenditure[i - 1];

                    if (Index1Value < Index2Value)
                    {
                        sortExpenditure[i] = Index2Value;
                        sortExpenditure[i - 1] = Index1Value;
                    }
                    else
                    {
                        break;
                    }

                    //if (Index1Value == Index2Value)
                    //{
                    //    break;
                    //}
                    //else if (Index1Value < Index2Value )
                    //{
                    //    sortExpenditure[i] = Index2Value;
                    //    sortExpenditure[i - 1] = Index1Value;
                    //}
                    //else
                    //{
                    //    break;
                    //}
                }
            }
 
             

        }

        private static int RemoveOneExpenditure(ref int[] sortExpenditure, int previousLeadingExpenditure
                        , int newTrailingExpenditure, int sortExpenditureLength)
        {
            int index = 0;
            for (int i = 0; i < sortExpenditureLength; i++)
            {
                if (previousLeadingExpenditure == sortExpenditure[i])
                {
                    sortExpenditure[i] = newTrailingExpenditure;
                    index = i;
                    break;
                }
            }
            return index;
        }

         

        private static decimal GetMedian(int[] middleValue)
        {
            decimal middleElement1 = middleValue[0];
           decimal middleElement2 =  middleValue[1];

            decimal medianValue = 0.00m;
            //if (countMode == 0)
            //{
                medianValue = decimal.Divide(( middleElement1 + middleElement2), 2 ) ;
            //}
            
            return medianValue;
        }
        
    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nd = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nd[0]);

        int d = Convert.ToInt32(nd[1]);

        int[] expenditure = Array.ConvertAll(Console.ReadLine().Split(' '), expenditureTemp => Convert.ToInt32(expenditureTemp))
        ;
        int result = activityNotifications(expenditure, d);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
