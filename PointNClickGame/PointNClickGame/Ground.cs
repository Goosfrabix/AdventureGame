using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PointNClickGame
{
    public class Ground
    {
        BasicEffect basicEffect;
        VertexPositionColor[] verticesLayer1;
        VertexPositionColor[] verticesLayer2;
        VertexPositionColor[] verticesLayer3;
        VertexPositionColor[] verticesLayer4;
        int[] triangleStripIndices;
        public List<List<GroundSegment>> groundsList = new List<List<GroundSegment>>();
        public List<GroundSegment> groundLayerOneList = new List<GroundSegment>();
        public List<GroundSegment> groundLayerTwoList = new List<GroundSegment>();
        public List<GroundSegment> groundLayerThreeList = new List<GroundSegment>();
        public List<GroundSegment> groundLayerFourList = new List<GroundSegment>();

        Random random;
        public int grwidth = 100;
        public int landwidth = 1000;
        public int beginindex;
        public int endindex;
        public float max_heigth = ((((1000 * 100) / 5)));
        public enum Biomes { Normal, Forest, Mountain, Snow, Village };

        public Ground(BasicEffect _basicEffect, Random _random)
        {
            basicEffect = _basicEffect;
            random = _random;
            landwidth = 200000 / grwidth;
            max_heigth = (((landwidth) * (grwidth)) / 5);

            groundsList.Add(groundLayerOneList);
            groundsList.Add(groundLayerTwoList);
            groundsList.Add(groundLayerThreeList);
            groundsList.Add(groundLayerFourList);

            InitializeGround(groundsList[0], (int)(max_heigth / 2) - 100);
            InitializeGround(groundsList[1], (int)(max_heigth / 2));
            InitializeGround(groundsList[2], (int)(max_heigth / 2) + 100);
            InitializeGround(groundsList[3], (int)(max_heigth / 2) + 200);

            verticesLayer1 = new VertexPositionColor[(groundsList[0].Count * 2)];
            verticesLayer2 = new VertexPositionColor[(groundsList[1].Count * 2)];
            verticesLayer3 = new VertexPositionColor[(groundsList[2].Count * 2)];
            verticesLayer4 = new VertexPositionColor[(groundsList[3].Count * 2)];
            triangleStripIndices = new int[6 * ((((groundsList[0].Count) * 2) / 2) - 1)];

            // Populate the array with references to indices in the vertex buffer.
            for (int i = 0; i < groundsList[0].Count; i++)
            {
                int point1 = i;
                int point2 = i + 1;
                int point3 = (groundsList[0].Count) + i;
                int point4 = point3 + 1;

                int ii = i * 6;
                if (ii < (triangleStripIndices.Length - 1))
                {
                    //triangleStripIndices[i] = (short)i;
                    /*1*/
                    triangleStripIndices[ii + 0] = point1;
                    /*2*/
                    triangleStripIndices[ii + 1] = point2;
                    /*3*/
                    triangleStripIndices[ii + 2] = point3;
                    /*4*/
                    triangleStripIndices[ii + 3] = point2;
                    /*5*/
                    triangleStripIndices[ii + 4] = point4;
                    /*6*/
                    triangleStripIndices[ii + 5] = point3;
                }
                else
                {
                }
            }

            GenerateGround();
        }
        public void GenerateGround()
        {
            groundsList[0][beginindex].Y = (int)(max_heigth / 2) - 100;
            groundsList[1][beginindex].Y = (int)(max_heigth / 2);
            groundsList[2][beginindex].Y = (int)(max_heigth / 2) + 100;
            groundsList[3][beginindex].Y = (int)(max_heigth / 2) + 200;
            groundsList[0][endindex].Y = (int)(max_heigth / 2) - 100;
            groundsList[1][endindex].Y = (int)(max_heigth / 2);
            groundsList[2][endindex].Y = (int)(max_heigth / 2) + 100;
            groundsList[3][endindex].Y = (int)(max_heigth / 2) + 200;

            for (int i = 0; i < groundsList.Count; i++)
            {
                if (i == 0)
                {
                    FarctureGround(beginindex, endindex, (int)max_heigth, groundsList[i]);
                }
                else 
                {
                    //FarctureChildGround(0, groundLayerOneList.Count - 1, (int)max_heigth, groundLayerOneList, groundLayerTwoList, false);
                    //FarctureChildGround(0, groundLayerThreeList.Count - 1, (int)max_heigth, groundLayerThreeList, groundLayerTwoList, true);
                    FarctureChildGround(beginindex, endindex, (int)max_heigth, groundsList[i], groundsList[i - 1], true);
                }
            }
            for (int j = 0; j < groundsList.Count; j++)
            {
                for (int jj = 0; jj < 10; jj++)
                {
                    SmoothGround(groundsList[j]);
                    SmoothGround(groundsList[j]);
                }
            }
            for (int k = 0; k < groundsList.Count; k++)
            {
                GenerateBiomes(groundsList[k]);
            }
            for (int l = 0; l < groundsList.Count; l++)
            {
                BiomesTogether(groundsList[l], 5);
            }
            InitializeVertices(groundLayerOneList, verticesLayer1);
            InitializeVertices(groundLayerTwoList, verticesLayer2);
            InitializeVertices(groundLayerThreeList, verticesLayer3);
            InitializeVertices(groundLayerFourList, verticesLayer4);
            
        }
        void InitializeGround(List<GroundSegment> groundlist, int baseY = 0)
        {
            beginindex = 3;
            groundlist.Add(new GroundSegment(-(6 * grwidth), baseY-100));
            groundlist.Add(new GroundSegment(-(5 * grwidth), baseY));
            groundlist.Add(new GroundSegment(-(grwidth), baseY));
            for (int i = 0; i < landwidth; i++)
            {
                groundlist.Add(new GroundSegment(i * grwidth, baseY));
            }
            endindex = beginindex + landwidth - 1;
            groundlist.Add(new GroundSegment((endindex * grwidth), baseY));
            groundlist.Add(new GroundSegment((endindex * grwidth) + (4 * grwidth), baseY));
            groundlist.Add(new GroundSegment((endindex * grwidth) + (5 * grwidth), baseY - 100));
            
            //groundlist[landwidth-1].Y = baseY + ((int)max_heigth / 2);
        }
        void InitializeVertices(List<GroundSegment> groundlist, VertexPositionColor[] vertices)
        {
            Color color = Color.Blue;
            for (int i = 0; i < groundlist.Count; i++)
            {
                color = getBiomeColor(groundlist[i].biome);
                // vertices[i].Position = new Vector3(groundlist[i].X, groundlist[i].Y, 0)- Gengine.Engine.GEngine.Camera.Position;
                if ((i - 1) > 0)
                {
                    color = Color.Lerp(color, vertices[i - 1].Color, 0.5f);
                }
                if (groundlist[i].Y <= (max_heigth / 2))
                {
                    if (groundlist[i].biome == (int)Biomes.Mountain)
                    {
                        color = Color.Lerp(Color.Gray, color, groundlist[i].Y / (max_heigth / 2));
                    }
                    if (groundlist[i].biome == (int)Biomes.Forest)
                    {
                        color = Color.Lerp(color, getBiomeColor((int)Biomes.Mountain), 1 - (groundlist[i].Y / (max_heigth / 2)));
                    }
                }
                if (groundlist[i].Y >= (max_heigth / 2))
                {
                    if ( groundlist[i].biome == (int)Biomes.Forest)
                    {
                        color = Color.Lerp(color, getBiomeColor((int)Biomes.Forest), ((groundlist[i].Y - (max_heigth / 2)) / ((max_heigth / 2))));
                    }
                    if (groundlist[i].biome == (int)Biomes.Normal)
                    {
                        color = Color.Lerp(getBiomeColor((int)Biomes.Forest),color, ((groundlist[i].Y - (max_heigth / 2)) / ((max_heigth / 2))));
                    }
                }

               groundlist[i].color = vertices[i].Color = color;
            }
            for (int j = 0; j < groundlist.Count; j++)
            {
                color = getBiomeColor(groundlist[j].biome);
                if ((j - 1) > 0)
                {
                    color = Color.Lerp(vertices[j - 1].Color, vertices[j].Color, 0.8f);
                }
                if ((j + 1) < groundlist.Count)
                {
                    color = Color.Lerp(color, vertices[j + 1].Color, 0.2f);
                }
                color = Color.Lerp(Color.Black, color, 0.1f);
                vertices[j + groundlist.Count].Color = color;// vertices[j].Color;// Color.Lerp(Color.Black,vertices[j].Color,0.01f);
            }
            
            //vertices[vertices.Length - 3].Color = Color.Black;
            //vertices[vertices.Length - 2].Color = Color.Black;
            //vertices[vertices.Length - 1].Color = Color.Black;

        }
        //recursive algoritme to make fracture
        void FarctureGround(int p1, int p2, int magnitude, List<GroundSegment> groundlist)
        {
            int distance = (p2 - p1);
            if (distance >= 2)
            {
                int pmid = (p1 + p2) / 2;
                int mag = magnitude;//(groundlist[p2].X - groundlist[p1].X)/5;
                groundlist[pmid].Y = ((groundlist[p1].Y + groundlist[p2].Y) / 2) + (random.Next(0, mag) - (mag / 2));
                FarctureGround(p1, pmid, magnitude / 2, groundlist);
                FarctureGround(pmid, p2, magnitude / 2, groundlist);
            }
        }
        void FarctureChildGround(int p1, int p2, int magnitude, List<GroundSegment> groundlist, List<GroundSegment> parentlist, bool up)
        {
            int distance = (p2 - p1);
            if (distance >= 2)
            {
                int pmid = (p1 + p2) / 2;
                int mag = magnitude;
                int newY = ((groundlist[p1].Y + groundlist[p2].Y) / 2) + (random.Next(0, mag) - (mag / 2));
                int parentdistance = parentlist[pmid].Y - newY;
                int min = 100;
                int max = 500;
                if ((((max * -1) <= parentdistance && parentdistance <= (min * -1)) && up))
                {
                    groundlist[pmid].Y = newY;
                    FarctureChildGround(p1, pmid, magnitude / 2, groundlist, parentlist, up);
                    FarctureChildGround(pmid, p2, magnitude / 2, groundlist, parentlist, up);

                }
                else if ((max >= parentdistance && parentdistance >= min) && !up)
                {
                    groundlist[pmid].Y = newY;
                    FarctureChildGround(p1, pmid, magnitude / 2, groundlist, parentlist, up);
                    FarctureChildGround(pmid, p2, magnitude / 2, groundlist, parentlist, up);

                }
                else
                {
                    //redo
                    FarctureChildGround(p1, p2, magnitude, groundlist, parentlist, up);
                }
            }
        }
        public void SmoothGround(List<GroundSegment> groundlist)
        {
                double angle = 0.0;
            for (int i = 1; i < groundlist.Count; i++)
            {   
                if (i >= (groundlist.Count - 1))
                {
                    angle = Gengine.ExtraMath.LawOfCosine(groundlist[i - 1].position, groundlist[i].position, groundlist[i].position - new Vector2(-100, 0));
                }
                else
                {
                    angle = Gengine.ExtraMath.LawOfCosine(groundlist[i - 1].position, groundlist[i].position, groundlist[i + 1].position);
                }
                // Console.WriteLine("i: "+i+" ängle: " + angle);
                if (angle <= 150)
                {
                    if (i >= (groundlist.Count - 1))
                    {
                        SmoothSegment(groundlist[i], groundlist[i].Y, groundlist[i - 1].Y, angle);
                    }
                    else
                    {
                        SmoothSegment(groundlist[i], groundlist[i + 1].Y, groundlist[i - 1].Y, angle);
                    }
                    /*/Console.WriteLine("YES");
                    if ((groundlist[i].Y <= ((groundlist[i + 1].Y + groundlist[i - 1].Y) / 2)))
                    {
                        //Console.WriteLine("Above");
                        groundlist[i].Y += random.Next(0, 180 - (int)angle);
                    }
                    else
                    {
                        //Console.WriteLine("Below");
                        groundlist[i].Y -= random.Next(0, 180 - (int)angle);
                    }*/
                }
            }
            angle = Gengine.ExtraMath.LawOfCosine(groundlist[0].position - new Vector2(100, 0), groundlist[0].position, groundlist[1].position);
                
            if (angle <= 150)
            {
                SmoothSegment(groundlist[0], groundlist[1].Y, groundlist[0].Y, angle);
            }
        }
        void SmoothSegment(GroundSegment ground, int previousY, int nextY,double _angle)
        {
            double midy = ((nextY + previousY) / 2);
            if (ground.Y <= midy)
            {
                //Console.WriteLine("Above");
                ground.Y += random.Next(0, (int)((midy - ground.Y)*(_angle / 180)));
            }
            else
            {
                //Console.WriteLine("Below");
                ground.Y -= random.Next(0, (int)((ground.Y - midy) * (_angle / 180)));
            }
        }
        void GenerateBiomes(List<GroundSegment> groundlist)
        {
            for (int i = 0; i < groundlist.Count; i++)
            {
                //groundLayerTwoList[i].Y
                double heightpercentage = (((groundlist[i].Y) / (max_heigth)) * 100);

                double height1 = (0);
                double height2 = (max_heigth/4);
                double height3 = (max_heigth/2);
                double height4 = (max_heigth / 2) + (max_heigth / 4);
                double height5 = (max_heigth);

                double height1percentage = (((groundlist[i].Y - (height1)) / (height2)) * 100);
                double height2percentage = (((groundlist[i].Y - (height2)) / (height2)) * 100);
                double height3percentage = (((groundlist[i].Y - (height3)) / (height2)) * 100);
                double height4percentage = (((groundlist[i].Y - (height4)) / (height2)) * 100);
                double snowchance = 0;
                double mountainchance = 0;
                double normalchance = 0;
                double forrestchance = 0;
                if (i > 0)
                {
                    if (groundlist[i].Y <= height2)
                    {
                        snowchance = 100 - height1percentage;
                        mountainchance = height1percentage;
                    }
                    else if (groundlist[i].Y <= height3)
                    {
                        mountainchance = 100 - height2percentage;
                        forrestchance = height2percentage;
                    }
                    else if (groundlist[i].Y <= height4)
                    {
                        forrestchance = (100 - height3percentage) * 2;
                        normalchance = height3percentage;
                    }
                    else
                    {
                        forrestchance = (100 - height4percentage);
                        normalchance = height4percentage*2;
                    }

                  //  normalchance += -heightpercentage;
                   // mountainchance += ( heightpercentage);
                   // snowchance += (heightpercentage);
                   // forrestchance += -(heightpercentage);
                    switch (groundlist[i - 1].biome)
                    {
                        case (int)Biomes.Normal: normalchance *= 5.0; mountainchance *= 0; snowchance *= 0; forrestchance *= 5.0; break;
                        case (int)Biomes.Forest: normalchance *= 2.5; ; mountainchance *= 2.5; snowchance *= 0; forrestchance *= 5.0; ; break;
                        case (int)Biomes.Mountain: normalchance *= 0; mountainchance *= 5.0; snowchance *= 2.5; forrestchance *= 2.5; break;
                        case (int)Biomes.Snow: normalchance *= 0; mountainchance *= 5.0; snowchance *= 5.0; forrestchance *= 0; break;
                        case (int)Biomes.Village: normalchance *= 2.5; mountainchance *= 2.5; snowchance *= 2.5; forrestchance *= 2.5; break;
                    }
                    normalchance = (normalchance / (normalchance + mountainchance + snowchance + forrestchance)) * 100;
                    mountainchance = (mountainchance / (normalchance + mountainchance + snowchance + forrestchance)) * 100;
                    snowchance = (snowchance / (normalchance + mountainchance + snowchance + forrestchance)) * 100;
                    forrestchance = (forrestchance / (normalchance + mountainchance + snowchance + forrestchance)) * 100;
                }
                else
                {
                    forrestchance = 100;
                    snowchance = normalchance = mountainchance = 0;
                }
                //snowchance = 100 - Math.Pow(heightpercentage, 2);
                //mountainchance = 100 - heightpercentage;
                //normalchance = heightpercentage;
                //forrestchance = Math.Pow(heightpercentage, 2);

                int randomnmgr = random.Next(0, 100);
                /*Console.WriteLine("[i].Y[ " + groundLayerTwoList[i].Y + " ]");
                Console.WriteLine("heightpercentage[ " + heightpercentage + " ] ");
                Console.WriteLine("sno[ " + snowchance + " ]");
                Console.WriteLine("mou[ " + mountainchance + " ]");
                Console.WriteLine("nor[ " + normalchance + " ]");
                Console.WriteLine("for[ " + forrestchance + " ]");
                Console.WriteLine("randomnmgr[ " + randomnmgr + " ]");*/

                if (randomnmgr <= snowchance)
                {
                    groundlist[i].biome = (int)Biomes.Snow;
                }
                else if ((randomnmgr <= (snowchance + mountainchance)) && (randomnmgr >= snowchance))
                {
                    groundlist[i].biome = (int)Biomes.Mountain;
                }
                else if ((randomnmgr <= (snowchance + mountainchance + forrestchance)) && (randomnmgr >= (snowchance + mountainchance)))
                {
                    groundlist[i].biome = (int)Biomes.Forest;
                }
                else if ((randomnmgr <= (snowchance + mountainchance + forrestchance + normalchance)) && (randomnmgr >= (snowchance + mountainchance + forrestchance)))
                {
                    groundlist[i].biome = (int)Biomes.Normal;
                }
                else
                {
                    groundlist[i].biome = (int)Biomes.Village;

                }
                
               // groundLayerOneList[i].biome = groundLayerThreeList[i].biome = groundLayerTwoList[i].biome;
            }
        }
        void BiomesTogether(List<GroundSegment> groundlist,int range)
        {
            for (int i = 0; i < groundlist.Count; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    if ((i + j) <= (groundlist.Count - 1))
                    {
                        if (groundlist[i + j].biome != groundlist[i].biome)
                        {
                            for (int h = j; h < range; h++)
                            {
                                if ((i + h) <= (groundlist.Count - 1))
                                {
                                    if (groundlist[i + h].biome == groundlist[i].biome)
                                    {
                                        groundlist[i + h].biome = groundlist[i + j].biome;
                                        groundlist[i + j].biome = groundlist[i].biome;
                                    }
                                }
                            }
                        }
                    }
                }
                for (int k = 0; k < range; k++)
                {
                    if ((i - k) >= 0)
                    {
                        if (groundlist[i - k].biome != groundlist[i].biome)
                        {
                            for (int l = k; l < range; l++)
                            {
                                if ((i - l) >= 0)
                                {
                                    if (groundlist[i - l].biome == groundlist[i].biome)
                                    {
                                        groundlist[i - l].biome = groundlist[i - k].biome;
                                        groundlist[i - k].biome = groundlist[i].biome;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //returns angle of B in any triangle, middle point 
        

        public void Draw()
        {
                DrawGround(groundsList[0], verticesLayer1);
                DrawGround(groundsList[1], verticesLayer2);
                DrawGround(groundsList[2], verticesLayer3);
                DrawGround(groundsList[3], verticesLayer4);
        }
        void DrawGround(List<GroundSegment> groundlist, VertexPositionColor[] vertices)
        {
            for (int i = 0; i < groundlist.Count; i++)
            {
                vertices[i].Position = (new Vector3(groundlist[i].X, groundlist[i].Y, 0) - (Gengine.Engine.GEngine.Camera.Transform.Position - new Vector3(Gengine.Engine.GEngine.Camera.ScreenMid2D.X, Gengine.Engine.GEngine.Camera.ScreenMid2D.Y, 0))) * Gengine.Engine.GEngine.Camera.Zoom;

            }
            for (int j = 0; j < groundlist.Count; j++)
            {
                vertices[j + groundlist.Count].Position = new Vector3((groundlist[j].X - Gengine.Engine.GEngine.Camera.Position.X + Gengine.Engine.GEngine.Camera.ScreenMid2D.X) * Gengine.Engine.GEngine.Camera.Zoom, Gengine.Engine.GEngine.graphicsDM.PreferredBackBufferHeight + 120, 0);
                //vertices[j + groundlist.Count].Color = Color.Black;
            }
            basicEffect.CurrentTechnique.Passes[0].Apply();
            //Gengine.Engine.GEngine.graphicsDM.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, vertices.Length-1);
            Gengine.Engine.GEngine.graphicsDM.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, vertices.Length - 1, triangleStripIndices, 0, (triangleStripIndices.Length - 2));
            
        }
        public Color getBiomeColor(int b)
        {
            Color color = Color.Blue;
            switch (b)
            {
                case (int)Biomes.Normal: color = Color.FromNonPremultiplied(94, 188, 54, 255); break;//LawnGreen
                case (int)Biomes.Forest: color = Color.FromNonPremultiplied(59, 86, 49, 255); break;//ForestGreen
                case (int)Biomes.Mountain: color = Color.FromNonPremultiplied(183, 139, 58, 255); break;//SandyBrown
                case (int)Biomes.Snow: color = Color.WhiteSmoke; break;//WhiteSmoke
                case (int)Biomes.Village: color = Color.Violet; break;//Violet
            }
            return color;
        }
    }
}
