using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_SDA
{
    
    public class HashTable
    {
        Dictionary<Int64, List<int[,]>> table;

        public HashTable()
        {
            table = new Dictionary<long, List<int[,]>>();
        }
        public void Add(Int64 key, int[,] mat)
        {
            List<int[,]> temp=new List<int[,]>();
            if (table.ContainsKey(key))
            {
                temp = table[key];
                temp.Add(mat);
                return;
            }
            temp.Add(mat);
            table.Add(key, temp);
        }
        public List<int[,]> getKey(Int64 key)
        {
            return table[key];
        }
        public static bool equalMatrix(int[,] x1,int[,] x2)
        {
            for (int i = 0; i < Math.Sqrt( x1.Length); i++)
                for (int j = 0; j < Math.Sqrt(x1.Length); j++)
                    if (x1[i, j] != x2[i, j])
                        return false;
            return true;
        }
        public bool checkElem(Int64 key,int[,] mat)
        {
            if(table.ContainsKey(key))
            {
                List<int[,]> temp = table[key];
                foreach(int[,] x in temp)
                {
                    if (equalMatrix(x,mat))
                        return true;
                }

            }
            return false;
        }
        public void clear()
        {
            table.Clear();
        }
    }
}
