using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avoid
{
    class MapCollection
    {
        bool proc(float x)
        {
            return true;
        }
        Map generateRandomMap()
        {
            Random rnd = new Random();
            Map cur = new Map();
            cur.length = 6000;
            float chance = 10;
            for (int i = 1; i <= cur.length; ++i)
            {
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
                    //s

                }
            }
                return cur;

        }
        public MapCollection()
        {
            Map test1 = new Map();
            // 1 sec = 100
            test1.length = 1000;
            test1.v[0].Add(1);
            test1.v[1].Add(30);
            test1.v[2].Add(40);
        }
    }
}
