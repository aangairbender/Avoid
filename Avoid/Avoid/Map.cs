using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avoid
{
    class Map
    {
        public List<int>[] v = new List<int>[4];
        public int length;
        public Map()
        {
            for (int i = 0; i < 4; ++i) v[i] = new List<int>();

        }
    }
}
