using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "ElementOnceAppearence"; } }

        public override void TryMyCode()
        {
            int N = 0 ;
            int output, expected;

            N = 11;
            int[] arr1 = { 1, 1, 3, 3, 4, 5, 5, 7, 7, 8, 8 };
            expected = 4;
            output = ElementOnceAppearence.FindUniqueElement(arr1, N);
            PrintCase(N, arr1, output, expected);

            N = 11;
            int[] arr2 = { 1, 1, 3, 3, 4, 4, 5, 5, 7, 7, 8 };
            expected = 8;
            output = ElementOnceAppearence.FindUniqueElement(arr2, N);
            PrintCase(N, arr2, output, expected);

            N = 9;
            int[] arr3 = { 1, 1, 2, 4, 4, 5, 5, 6, 6 };
            expected = 2;
            output = ElementOnceAppearence.FindUniqueElement(arr3, N);
            PrintCase(N, arr3, output, expected);

            N = 5;
            int[] arr4 = { 4, 10, 10, 11, 11 };
            expected = 4;
            output = ElementOnceAppearence.FindUniqueElement(arr4, N);
            PrintCase(N, arr4, output, expected);
        }

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int N = 0;
            int[] arr = null;
            int output, actualResult;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);
   
            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
 
            int i = 1;
            while (testCases-- > 0)
            {
                N = br.ReadInt32();
                arr = new int[N];
                for (int j = 0; j < N; j++)
                {
                    arr[j] = br.ReadInt32();
                }
                actualResult = br.ReadInt32();

                //Console.WriteLine("N = {0}, Res = {1}", N, actualResult);
                output = 0;
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            int sum = 0;
                            int numOfRep = 10;
                            Stopwatch sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                            {
                                sum += ElementOnceAppearence.FindUniqueElement(arr, N);
                            }
                            output = sum / numOfRep;
                            sw.Stop();
                            //Console.WriteLine("N = {0}, time in ms = {1}", arr.Length, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            output = int.MinValue;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
					tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (output == actualResult)    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0)); 
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
 
        }
        public override void GenerateTestCases(HardniessLevel level, int numOfCases)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintCase(int N, int[] arr, int output, int expected)
        {
            Console.WriteLine("N: {0}", N);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Output = " + output);
            Console.WriteLine("Expected = " + expected);
            Console.WriteLine();
        }
        
        #endregion
   
    }
}
