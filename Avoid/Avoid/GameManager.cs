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
        public const float barWidth = 10f;
        public int time; // in ms from beginning
        public int msInTick;
        public int[] p = new int[4];
        public Map map;
        public bool playing;
        public InputController inputController;
        public GameManager(Map map, InputController inputController, int msInTick)
        {
            this.map = map;
            this.inputController = inputController;
            this.msInTick = msInTick;
            time = 0;
            playing = false;
        }

        public void start()
        {
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
            if (playing == false) return;
            int ind = inputController.getKeyIndex();
            for (int i = 0; i < p.Length; ++i)
            {
                while (map.v[i][p[i]] < time) p[i]++;
            }
            if(map.v[ind][p[ind]] == time)
            {
                pause();
            }
            time++;
        }

        public void update(int ms)
        {
            for (int i = 0; i < ms / msInTick; ++i)
                makeIteration();
        }

        public void draw(Graphics g, float width, float height)
        {
            g.Clear(Color.Black);
            float cWidth = width / p.Length;
            Brush brush = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Black);
            Brush brush2 = new SolidBrush(Color.Green);
            int ind = inputController.getKeyIndex();
            for (int i = 0; i < p.Length; ++i)
            {
                float x = cWidth * i;
                for (int j = p[i]; (map.v[i][j] - time) * barWidth + barWidth < height; ++j)
                {
                    g.FillRectangle(brush, x, height - (map.v[i][j] - time) * barWidth - barWidth, cWidth, barWidth);
                }
                if (i == ind)
                {
                    g.FillRectangle(brush2, x, height - barWidth, cWidth, barWidth);
                }
                g.DrawRectangle(pen, x, height - barWidth, cWidth, barWidth);
            }
        }

    }
}
