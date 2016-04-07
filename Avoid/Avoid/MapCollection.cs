using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avoid
{
    class MapCollection
    {
        Random rnd1 = new Random();
        bool proc(double x)
        {
            int q = Convert.ToInt32(x*100);
            int w =rnd1.Next(100);
            if(w<q)return true;
            return false;
        }
        public Map generateRandomMap(int len)
        {
            Random rnd = new Random();
            Map cur = new Map();
            cur.length = len;
            double chance = 0.1;
            for (int i = 40; i <= cur.length; ++i)
            {
                if (chance > 50) chance = 50;
                for (int j = 0; j < 4; ++j)
                {
                    if (proc(chance)) cur.v[j].Add(i);   
                }
                int minx=i;
                for (int j = 0; j < 4; ++j)
                {
                    if (cur.v[j].Count()!=0) minx = Math.Min(minx, cur.v[j].Last());
                }
                if (i - minx < 4)
                {
                    int q = rnd.Next(0, 3);
                    while (cur.v[q].Count() != 0 && cur.v[q].Last() > i - 6) cur.v[q].Remove(cur.v[q].Last());
                    //s

                }
                //chance = chance * 1.01;
            }
                return cur;

        }
        public MapCollection()
        {
            Map test1 = new Map();
            // 1 sec = 100
            test1.length = 100;
            test1.v[0].Add(1);
            test1.v[1].Add(30);
            test1.v[2].Add(40);
            Map test2 = generateRandomMap(200);
        }
    }
}
