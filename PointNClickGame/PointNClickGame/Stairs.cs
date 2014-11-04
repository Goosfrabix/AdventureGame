using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PointNClickGame
{
    class Stairs
    {
        public List<Point> points = new List<Point>();
        Random random = new Random();
        public Stairs(Point A, Point B)
        {
            points.Add(A);
            points.Add(B);
            Farcture( 0, 1, 50,0, points);
        }
        void Farcture(int ia,int ib, int magnitude,int depth, List<Point> _points)
        {
            int distance = (_points[ia].Y + _points[ib].Y) / 2;
            if (depth < 2)
            {
                Point pmid = new Point((_points[ia].X + _points[ib].X) / 2, (_points[ia].Y + _points[ib].Y) / 2);
                int mag = magnitude;//(groundlist[p2].X - groundlist[p1].X)/5;
                Point np = new Point(pmid.X + (random.Next(0, mag) - (mag / 2)), pmid.Y);
                _points.Insert(ib, np);

                //_points[pmid].X = ((_points[p1].X + _points[p2].X) / 2) + (random.Next(0, mag) - (mag / 2));

                Farcture(ia, ib, magnitude, depth + 1, _points);
                Farcture(_points.Count - 2 - (1 * depth), _points.Count - 1 - (1 * depth), magnitude, depth + 1, _points);
            }
        }
    }
}
