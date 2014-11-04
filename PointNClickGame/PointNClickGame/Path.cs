using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PointNClickGame
{
    public class Path
    {
        public List<PathPoint> path = new List<PathPoint>();
        public List<PathPoint> openPoints = new List<PathPoint>();
        Ground ground;
        Random random = Game1.random;
        public Path(Ground _ground)
        {
            ground = _ground;
        }
        public void CreatePath()
        {

            foreach (var p in path)
            {
                p.Kill();
            }
            path.Clear();
            openPoints.Clear();

            for (int t = 0; t < ground.groundsList.Count; t++)
            {
                for (int r = 0; r < ground.groundsList[t].Count; r++)
                {
                    ground.groundsList[t][r].on_path = false;
                }
            }
            //Makes the beginingbit
            CreateBeginPath();
            //PathCreating
            OpenPointsCheck(100);

            //CreateFracturestairs
            int lenght = path.Count;
            for (int c = 1; c < lenght; c++)
            {
                if (path[c].status == "stairs")
                {
                    Console.WriteLine("Fracking it up!");
                    List<Point> newpoints = new List<Point>();
                    newpoints.Add(path[c].Parent.Position);
                    newpoints.Add(path[c].Position);
                    int mag = path[c].Parent.Position.Y - path[c].Position.Y;
                    if (mag < 0)
                        mag *= -1;
                    Gengine.ExtraMath.Fracture(0, 1, mag / 3, 0, 3, newpoints, random);
                    for (int np = 1; np < newpoints.Count - 1; np++)
                    {
                        PathPoint kelly = new PathPoint(newpoints[np]);
                        kelly.Parent = path[c].Parent;
                        kelly.status = "stairs";
                        kelly.mygroundIndexX = path[c].mygroundIndexX;
                        kelly.mygroundIndexY = path[c].mygroundIndexY;
                        kelly.myground = path[c].myground;
                        path[c].Parent._children.Remove(path[c]);
                        path[c].Parent._children.Add(kelly);
                        kelly._children.Add(path[c]);
                        path[c].Parent = kelly;
                        path.Add(kelly);
                    }
                }
            }
            //drawstuff
            for (int b = 1; b < path.Count; b++)
            {
                Color color = Color.Lerp(ground.groundsList[path[b].mygroundIndexY][path[b].mygroundIndexX].color, Color.SandyBrown, 0.5f);
                Gengine.Engine.Graphics.DrawSprite _drawComponent = new Gengine.Engine.Graphics.DrawSprite(new Vector3(path[b].Position.X - 25, path[b].Position.Y - 1, 0), Game1.walksegment, 100, 7, 0, true, false, true);
                _drawComponent.Alpha = 1.0f;
                _drawComponent.Color = color;
                _drawComponent.Layer = (float)path[b].mygroundIndexY;
                _drawComponent.Width = 50;
                _drawComponent.Height = 4;
                Gengine.Engine.GEngine.drawManager.Add(_drawComponent);
                path[b].spritelist.Add(_drawComponent);
                CreatePathSegment(path[b], path[b].Position, path[b].Parent.Position,(float)path[b].mygroundIndexY, color);
                /*Color color = Color.Lerp(ground.groundsList[path[b].mygroundIndexY][path[b].mygroundIndexX].color, Color.SandyBrown, 0.5f);
                if (path[b].status == "stairs")
                {
                    Gengine.Engine.Graphics.DrawSprite _drawC = new Gengine.Engine.Graphics.DrawSprite(new Vector3(path[b].Position.X - 20, path[b].Position.Y - 1, 0), Game1.pathsegment, 40, 6, 0, true, false, true);
                    _drawC.Alpha = 1.0f;
                    _drawC.Color = color;
                    Gengine.Engine.GEngine.drawManager.Add(_drawC);
                    path[b].spritelist.Add(_drawC);
                    CreatePathSegment(path[b], path[b].Position, path[b].Parent.Position, color);
                }
                else
                {
                    Gengine.Engine.Graphics.DrawSprite _drawComponent = new Gengine.Engine.Graphics.DrawSprite(new Vector3(((path[b].Position.X + path[b].Parent.Position.X) / 2), ((path[b].Position.Y + path[b].Parent.Position.Y) / 2), 0), Game1.walksegment, 100, 7, 0, true, false, true);
                    _drawComponent.Origin = new Vector2(50, 1);
                    _drawComponent.Alpha = 1.0f;
                    _drawComponent.Color = color;
                    double rot = 0.0;
                    if ((path[b].Position.X > path[b].Parent.Position.X))
                    {
                        rot = Math.Atan2((path[b].Position.Y - path[b].Parent.Position.Y), (path[b].Position.X - path[b].Parent.Position.X));
                    }
                    else
                    {
                        rot = Math.Atan2((path[b].Parent.Position.Y - path[b].Position.Y), (path[b].Parent.Position.X - path[b].Position.X));
                    }
                    _drawComponent.Rotation = (float)rot;
                    Gengine.Engine.GEngine.drawManager.Add(_drawComponent);
                    path[b].spritelist.Add(_drawComponent);
                }*/
            }

        }
        void CreatePathSegment(PathPoint pa, Point A, Point B,float layer, Color color)
        {
            double distance = Math.Sqrt( Math.Pow((B.X-A.X),2)+Math.Pow((B.Y-A.Y),2));
            double distanceY = (B.Y - A.Y);
            if (distanceY < 0)
            {
                distanceY *= -1;
            }
            if (distance >= 20)
            {
                Point mid = new Point((B.X + A.X) / 2, (B.Y + A.Y) / 2);

                Gengine.Engine.Graphics.DrawSprite _drawComponent = new Gengine.Engine.Graphics.DrawSprite(new Vector3(mid.X - 25, mid.Y - 1, 0), Game1.walksegment, 100, 7, 0, true, false, true);
                _drawComponent.Alpha = 1.0f;
                _drawComponent.Color = color;
                _drawComponent.Layer = layer;
                _drawComponent.Width = 50;
                _drawComponent.Height = 4;
                Gengine.Engine.GEngine.drawManager.Add(_drawComponent);
                pa.spritelist.Add(_drawComponent);
                CreatePathSegment(pa, A, mid,layer, color);
                CreatePathSegment(pa, mid, B,layer, color);
            }
        }
        void CheckNeighbour(int nx, int ny, PathPoint parent)
        {
            bool A = (parent.mygroundIndexX + nx > ground.beginindex);
            bool B = (parent.mygroundIndexX + nx < ground.endindex);
            bool C = (parent.mygroundIndexY + ny >= 0);
            bool D = (parent.mygroundIndexY + ny < ground.groundsList.Count);
            if (A && B && C && D)
            {
                if (!ground.groundsList[parent.mygroundIndexY + ny][parent.mygroundIndexX + nx].on_path)
                {
                    GroundSegment nextground = ground.groundsList[parent.mygroundIndexY + ny][parent.mygroundIndexX + nx];
                    PathPoint next = new PathPoint(new Point((int)nextground.position.X, (int)nextground.position.Y));
                    next.mygroundIndexY = parent.mygroundIndexY + ny;
                    next.mygroundIndexX = parent.mygroundIndexX + nx;
                    next.myground = nextground;
                    if (next.mygroundIndexY != parent.mygroundIndexY)
                    {
                        next.status = "stairs";
                    }
                    next.Parent = parent;
                    parent._children.Add(next);
                    openPoints.Add(next);
                    nextground.on_path = true;
                }
            }
        }
        void OpenPointsCheck(int iiii)
        {
            for (int i = 0; i < openPoints.Count; )
            {

                Console.WriteLine("openPoints.Count [" + openPoints.Count);
                if (openPoints.Count >= 1)
                {
                    float chance = ((100 / openPoints.Count) * 2);
                    PathPoint current = openPoints[0];
                    openPoints.Remove(current);
                    path.Add(current);
                    if (random.Next(0, 100) <= chance)
                    {
                        if (random.Next(0, 100) <= chance)
                        {
                            CheckNeighbour(1, 0, current);
                        }
                        if (current.Parent.mygroundIndexY >= current.mygroundIndexY)
                        {
                            if (random.Next(0, 100) <= 12)
                            {
                                CheckNeighbour(0, 1, current);
                            }
                        }
                        if (current.Parent.mygroundIndexY <= current.mygroundIndexY)
                        {
                            if (random.Next(0, 100) <= 12)
                            {
                                CheckNeighbour(0, -1, current);
                            }
                        }
                        if (random.Next(0, 100) <= 25)
                        {
                            CheckNeighbour(-1, 0, current);
                        }
                    }
                    else
                    {
                        if (current.status == "stairs")
                        {

                            path.Remove(current);
                        }
                    }
                }
            }
            bool made_end = false;
            for (int j = path.Count - 1; j > 0; j--)
            {
                if (path[j].mygroundIndexX == ground.endindex)
                {
                    made_end = true;
                    break;
                }
            }
            if (!made_end)
            {
                Console.WriteLine("nope");
                bool foundLast = false;
                for (int g = ground.endindex; g > ground.beginindex; g--)
                {

                    for (int h = path.Count - 1; h > 0; h--)
                    {
                        if (!foundLast)
                        {
                            if (path[h].mygroundIndexX == g)
                            {
                                if (path[h].Parent.mygroundIndexX < path[h].mygroundIndexX)
                                {
                                    PathPoint a = path[h];
                                    path.Remove(a);
                                    openPoints.Add(a);
                                    foundLast = true;
                                }
                            }
                        }
                    }
                }
                if (iiii > 0)
                {
                    OpenPointsCheck(iiii - 1);
                }
            }
            else
            {
                for (int h = path.Count - 1; h > 0; h--)
                {
                    if (path[h]._children.Count == 0)
                    {
                        if (path[h].mygroundIndexX == path[h].Parent.mygroundIndexX)
                        {
                            PathPoint o = path[4];
                            path[h].Parent._children.Remove(o);
                            path.Remove(o);
                        }
                    }
                }
            }
        }
        void CreateBeginPath()
        {
            path.Add(new PathPoint(new Point(ground.groundsList[1][ground.beginindex - 1].X, ground.groundsList[1][ground.beginindex - 1].Y)));
            path[0].myground = ground.groundsList[1][ground.beginindex - 1];
            path[0].mygroundIndexX = ground.beginindex - 1;
            path[0].myground.on_path = true;
            path[0].mygroundIndexY = 1;

            openPoints.Add(new PathPoint(new Point(ground.groundsList[path[0].mygroundIndexY][path[0].mygroundIndexX + 1].X, ground.groundsList[path[0].mygroundIndexY][path[0].mygroundIndexX + 1].Y)));
            openPoints[0].Parent = path[0];
            openPoints[0].myground = ground.groundsList[openPoints[0].Parent.mygroundIndexY][openPoints[0].Parent.mygroundIndexX + 1];
            openPoints[0].mygroundIndexX = openPoints[0].Parent.mygroundIndexX + 1;
            openPoints[0].mygroundIndexY = openPoints[0].Parent.mygroundIndexY;
            openPoints[0].myground.on_path = true;
            path[0]._children.Add(openPoints[0]);
            openPoints.Add(new PathPoint(new Point(ground.groundsList[path[0].mygroundIndexY+1][path[0].mygroundIndexX].X, ground.groundsList[path[0].mygroundIndexY+1][path[0].mygroundIndexX].Y)));
            openPoints[1].Parent = path[0];
            openPoints[1].myground = ground.groundsList[openPoints[1].Parent.mygroundIndexY+1][openPoints[1].Parent.mygroundIndexX ];
            openPoints[1].mygroundIndexX = openPoints[1].Parent.mygroundIndexX;
            openPoints[1].mygroundIndexY = openPoints[1].Parent.mygroundIndexY+1;
            openPoints[1].myground.on_path = true;
            openPoints[1].status = "stairs";
            path[0]._children.Add(openPoints[1]);
            /*//0
            path.Add(new PathPoint(new Point(ground.groundsList[1][ground.beginindex - 2].X, ground.groundsList[1][ground.beginindex - 2].Y)));
            //1
            path.Add(new PathPoint(new Point(ground.groundsList[1][ground.beginindex - 2].X + 200, (ground.groundsList[1][ground.beginindex - 2].Y + ground.groundsList[1][ground.beginindex - 1].Y) / 2)));
            path[1].Parent = path[0];

            //2
            path.Add(new PathPoint(new Point(ground.groundsList[1][ground.beginindex - 1].X - 100, ground.groundsList[1][ground.beginindex - 1].Y - 50)));
            path[2].Parent = path[1];

            //3
            path.Add(new PathPoint(new Point(ground.groundsList[0][ground.beginindex - 1].X, ground.groundsList[0][ground.beginindex - 1].Y)));
            path[3].Parent = path[2];

            //4
            path.Add(new PathPoint(new Point(ground.groundsList[1][ground.beginindex - 1].X, ground.groundsList[1][ground.beginindex - 1].Y)));
            path[4].Parent = path[1];

            //5
            path.Add(new PathPoint(new Point(ground.groundsList[2][ground.beginindex - 1].X - 50, ((ground.groundsList[1][ground.beginindex - 1].Y) + ground.groundsList[2][ground.beginindex - 1].Y) / 2)));
            path[5].Parent = path[1];

            //6
            path.Add(new PathPoint(new Point(ground.groundsList[2][ground.beginindex - 1].X, ground.groundsList[2][ground.beginindex - 1].Y)));
            path[6].Parent = path[5];

            //7
            path.Add(new PathPoint(new Point(ground.groundsList[2][ground.beginindex - 1].X, ground.groundsList[2][ground.beginindex - 1].Y)));
            path[7].Parent = path[6];

            //8
            path.Add(new PathPoint(new Point(ground.groundsList[3][ground.beginindex - 1].X - 75, ((ground.groundsList[2][ground.beginindex - 1].Y) + ground.groundsList[3][ground.beginindex - 1].Y) / 2)));
            path[8].Parent = path[7];

            //9
            path.Add(new PathPoint(new Point(ground.groundsList[3][ground.beginindex - 1].X, ground.groundsList[3][ground.beginindex - 1].Y)));
            path[9].Parent = path[8];



            openPoints.Add(new PathPoint(new Point(ground.groundsList[0][ground.beginindex].X, ground.groundsList[0][ground.beginindex].Y)));
            openPoints[0].Parent = path[3];
            openPoints[0].mygroundIndexX = ground.beginindex;
            openPoints[0].mygroundIndexY = 0;
            openPoints.Add(new PathPoint(new Point(ground.groundsList[1][ground.beginindex].X, ground.groundsList[1][ground.beginindex].Y)));
            openPoints[1].Parent = path[4];
            openPoints[1].mygroundIndexX = ground.beginindex;
            openPoints[1].mygroundIndexY = 1;
            openPoints.Add(new PathPoint(new Point(ground.groundsList[2][ground.beginindex].X, ground.groundsList[2][ground.beginindex].Y)));
            openPoints[2].Parent = path[7];
            openPoints[2].mygroundIndexX = ground.beginindex;
            openPoints[2].mygroundIndexY = 2;
            openPoints.Add(new PathPoint(new Point(ground.groundsList[3][ground.beginindex].X, ground.groundsList[3][ground.beginindex].Y)));
            openPoints[3].Parent = path[9];
            openPoints[3].mygroundIndexX = ground.beginindex;
            openPoints[3].mygroundIndexY = 3;

            ground.groundsList[0][ground.beginindex - 1].on_path = true;
            ground.groundsList[1][ground.beginindex - 1].on_path = true;
            ground.groundsList[2][ground.beginindex - 1].on_path = true;
            ground.groundsList[3][ground.beginindex - 1].on_path = true;
            ground.groundsList[0][ground.beginindex].on_path = true;
            ground.groundsList[1][ground.beginindex].on_path = true;
            ground.groundsList[2][ground.beginindex].on_path = true;
            ground.groundsList[3][ground.beginindex].on_path = true;*/
        }
        void CreateEndPath()
        {
        }
        
    }
}
