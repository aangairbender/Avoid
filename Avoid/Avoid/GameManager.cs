using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avoid
{
    class GameManager
    {
        public const float barWidth = 20f;
        public float timems; // in ms from beginning
        public int time; //in ticks
        public float msInTick;
        public int[] p = new int[4];
        public Map map;
        public bool playing;
        public InputController inputController;
        public GameManager(Map map, InputController inputController, float msInTick)
        {
            this.map = map;
            this.inputController = inputController;
            this.msInTick = msInTick;
            timems = 0;
            playing = false;
        }

        public void start()
        {
            timems = 0;
            time = 0;
            for (int i = 0; i < 4; ++i)
                p[i] = 0;
            playing = true;
        }

        public void resume()
        {
            playing = true;
        }

        public void pause()
        {
            playing = false;
        }

        public void makeIteration()
        {
            int ind = inputController.getKeyIndex();
            for (int i = 0; i < p.Length; ++i)
            {
                while (p[i] < map.v[i].Count && map.v[i][p[i]] < time) p[i]++;
            }
            if(p[ind] < map.v[ind].Count && map.v[ind][p[ind]] == time)
            {
                pause();
            }
            time++;
        }

        public void update(int ms)
        {
            if (playing == false) return;
            timems += ms;
            time = Convert.ToInt32(timems / msInTick);
            makeIteration();     
        }

        public void draw(Graphics g, float width, float height)
        {
            g.Clear(Color.Black);
            float cWidth = width / p.Length;
            Brush brush = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.White);
            Brush brush2 = new SolidBrush(Color.Green);
            int ind = inputController.getKeyIndex();

            float time2 = timems / msInTick;

            for (int i = 0; i < p.Length; ++i)
            {
                float x = cWidth * i;
                for (int j = p[i]; j < map.v[i].Count && (map.v[i][j] - time2) * barWidth + barWidth < height; ++j)
                {
                    g.FillRectangle(brush, x, height - (map.v[i][j] - time2) * barWidth - barWidth, cWidth, barWidth);
                }
                if (i == ind)
                {
                    g.FillRectangle(brush2, x, height - barWidth, cWidth, barWidth);
                }
            }
            g.DrawLine(pen, 0, height - barWidth, width, height - barWidth);
        }

    }
}
