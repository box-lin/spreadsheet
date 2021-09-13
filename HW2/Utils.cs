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

        public static int ConstantSpaceDistinct(List<int> rdList)
        {
            return 0;
        }

    }
}
