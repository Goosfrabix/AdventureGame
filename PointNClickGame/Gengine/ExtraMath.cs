using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine
{
    public static class ExtraMath
    {
        public static void Fracture(int ia, int ib, int magnitude, int count, int depth, List<Point> _points, Random random)
        {
            int distance = (_points[ia].Y + _points[ib].Y) / 2;
            if (count < depth)
            {
                Point pmid = new Point((_points[ia].X + _points[ib].X) / 2, (_points[ia].Y + _points[ib].Y) / 2);
                int mag = magnitude;//(groundlist[p2].X - groundlist[p1].X)/5;
                Point np = new Point(pmid.X + (random.Next(0, mag) - (mag / 2)), pmid.Y);
                _points.Insert(ib, np);

                //_points[pmid].X = ((_points[p1].X + _points[p2].X) / 2) + (random.Next(0, mag) - (mag / 2));

                Fracture(ia, ib, magnitude, count + 1, depth, _points, random);
                Fracture(_points.Count - 2 - (1 * count), _points.Count - 1 - (1 * count), magnitude, count + 1, depth, _points, random);
            }
        }
        public static Point Project(Point line1, Point line2, Point toProject)
        {
            double m = (double)(line2.Y - line1.Y) / (line2.X - line1.X);
            double b = (double)line1.Y - (m * line1.X);

            double x = (m * toProject.Y + toProject.X - m * b) / (m * m + 1);
            double y = (m * m * toProject.Y + m * toProject.X + b) / (m * m + 1);

            return new Point((int)x, (int)y);
        }
        public static double LawOfCosine(Vector2 A, Vector2 B, Vector2 C)
        {
            double a = Math.Sqrt(Math.Pow((C.X - B.X), 2) + Math.Pow((C.Y - B.Y), 2));
            double b = Math.Sqrt(Math.Pow((B.X - A.X), 2) + Math.Pow((B.Y - A.Y), 2));
            double c = Math.Sqrt(Math.Pow((C.X - A.X), 2) + Math.Pow((C.Y - A.Y), 2));

            double cos = ((Math.Pow(a, 2) + Math.Pow(b, 2)) - Math.Pow(c, 2)) / ((2 * a) * (b));

            double angle = Math.Acos(cos);
            angle *= (180 / Math.PI);
            return angle;
        }
    }
}
