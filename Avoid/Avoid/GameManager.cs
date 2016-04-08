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
        private Random rnd = new Random();
        public const float barWidth = 20f;
        public float timems; // in ms from beginning
        public int time; //in ticks
        public float msInTick;
        public int[] p = new int[4];
        public Map map;
        public bool playing;
        public InputController inputController;
        public AudioManager audioManager;
        public int[] positions;
        public bool showingReplay;
        public GameManager(Map map, InputController inputController, float msInTick)
        {
            this.map = map;
            this.inputController = inputController;
            this.msInTick = msInTick;
            timems = 0;
            playing = false;
            audioManager = new AudioManager();
            showingReplay = false;
            positions = new int[map.length];
        }

        public void start()
        {
            timems = 0;
            time = 0;
            for (int i = 0; i < 4; ++i)
                p[i] = 0;
            playing = true;
            audioManager["background"].PlayLooping();
            showingReplay = false;
        }

        public void showReplay()
        {
            timems = 0;
            time = 0;
            for (int i = 0; i < 4; ++i)
                p[i] = 0;
            playing = true;
            audioManager["background"].PlayLooping();
            showingReplay = true;
        }
        public void showAuto()
        {
            timems = 0;
            time = 0;
            for (int i = 0; i < 4; ++i)
                p[i] = 0;

            for (int j = 0; j < map.length; ++j)
            {
                for (int i = 0; i < p.Length; ++i)
                {
                    while (p[i] < map.v[i].Count && map.v[i][p[i]] < j) p[i]++;
                }

                if (j > 0) positions[j] = positions[j - 1];
                else positions[j] = rnd.Next(0, 3);

                if (j > 0)
                {
                    if (p[positions[j]]<map.v[positions[j]].Count && map.v[positions[j]][p[positions[j]]] != j) continue;
                }

                List<Tuple<int, int>> c = new List<Tuple<int, int>>();
                for (int i = 0; i < p.Length; ++i)
                {
                    if (p[i] < map.v[i].Count) c.Add(new Tuple<int, int>(map.v[i][p[i]] - j, i));
                }
                c.Sort();
                if (c.Count > 0) positions[j] = c[c.Count - 1].Item2;
            }

            for (int i = 0; i < 4; ++i)
                p[i] = 0;
            playing = true;
            audioManager["background"].PlayLooping();
            showingReplay = true;
        }

        public void resume()
        { 
            playing = true;
        }

        public void pause()
        {
            playing = false;
            audioManager["background"].Stop();
            showingReplay = false;
        }



        public void makeIteration()
        {
            int ind = inputController.getKeyIndex();
            if (showingReplay) ind = positions[time];
            for (int i = 0; i < p.Length; ++i)
            {
                while (p[i] < map.v[i].Count && map.v[i][p[i]]*msInTick < timems) p[i]++;
            }
            if(p[ind] < map.v[ind].Count && map.v[ind][p[ind]] == time)
            {
                pause();
            }
            if (!showingReplay) positions[time] = ind;
            time++;
            if (time == map.length) pause();
        }

        public void update(float ms)
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

            if (showingReplay)
            {
                if (time < positions.Length)
                    ind = positions[time];
                else
                    pause();
            }

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
