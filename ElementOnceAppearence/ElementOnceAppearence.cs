using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class ElementOnceAppearence
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Write efficient algorithm to get the element that appears once in the array
        /// </summary>
        /// <param name="arr">sorted array of all elements appear twice except one element </param>
        /// <param name="N">number of elements in the array</param>
        /// <returns>return the element that is appeared once </returns>
        public static int FindUniqueElement(int[] arr, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();

            /*int unique_Element = 0;
            for (int i=0;i<N;i++)
            { 

                if (i == 0)
                 {
                     if (arr[i] != arr[i+1])
                     {
                         return arr[i];
                     }
                 }
                 else if (i == N - 1)
                 {
                     if (arr[i] != arr[i-1])
                     {
                         return arr[i];
                     }
                 }
                 else if (i > 0&& i < N-1 )
                  {
                      if (arr[i] != arr[i - 1])
                      {
                          if (arr[i] != arr[i + 1])
                          {

                              unique_Element = arr[i];
                              break;
                          }
                      }
                  }
              
                //unique_Element = unique_Element ^ arr[i];
            }
            return unique_Element;*/


            int unique_Element = search(arr, 0, N - 1);
            int search(int[] A, int low, int high)
            {
                //Base Case
                if (low > high)
                {
                    return A[high];
                }
                if (low == high)
                {
                    return A[low];
                }
                if (N == 1)
                {
                    return A[0];
                }
                if (A[low] != A[low + 1])
                {
                    return A[low];
                }
                if (A[high] != A[high - 1])
                {
                    return A[high];
                }
                int mid_index = (high + low) / 2;
                if (mid_index % 2 != 0)
                {
                    if (A[mid_index] == A[mid_index - 1])
                    {
                        return search(A, mid_index + 1, high);
                    }
                    else
                    {
                        return search(A, low, mid_index - 1);
                    }
                }
                else
                {
                    if (A[mid_index] == A[mid_index + 1])
                    {
                        return search(A, mid_index + 2, high);
                    }
                    else
                    {
                        return search(A, low, mid_index);
                    }
                }
            }
            return unique_Element;
        }

        #endregion
    }
}
