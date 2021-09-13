using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    /// <summary>
    /// This the class of the collection of methods
    /// </summary>
    public class Utils
    {
        public static List<int> GenerateRandomList(int min, int max, int size)
        {
            Random rd = new Random();
            List<int> rdList = new List<int>();
            for (int i = 0; i<size; i++)
            {
                rdList.Add(rd.Next(min, max+1)); //[min,max+1) = [min,max] based on integer min diff = 1
            }
            return rdList;
        }

        public static int HashSetDistinct(List<int> rdList)
        {
            HashSet<int> hash = new HashSet<int>();
            foreach (int val in rdList){
                hash.Add(val);
            }
            return hash.Count;
        }

        /// <summary>
        /// This should be a O(N^2) worst time compleixty and O(1) space. 
        /// </summary>
        /// <param name="rdList"></param>
        /// <returns></returns>
        public static int ConstantSpaceDistinct(List<int> rdList)
        {
            if (rdList.Count < 2) return rdList.Count; //empty or 1 
            
            int res = rdList.Count;
            for (int i = 0; i < rdList.Count-1; i++) //O(N) is a must in best,average,worst cases
            {
                for (int j = i+1; j < rdList.Count; j++) //O(N) happens in worst case "no duplicates"
                {
                    if(rdList[i] == rdList[j]) //O(1) Cosntant
                    {
                        //when we found a duplicate in j(fast pointer) we decrease the res by 1;
                        //we also want to break and start i(slow pointer) next step because we 
                        //sure that we will eventually encounter this duplicate and will keep the 
                        //pattern that search for future duplicates
                        res--;
                        break;
                    }
                } 
            }
            return res;
        }

    }
}
