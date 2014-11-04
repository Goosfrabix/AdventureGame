#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace PointNClickGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect basicEffect;
        //VertexPositionColor[] verticesLayer1;
        //VertexPositionColor[] verticesLayer2;
       // VertexPositionColor[] verticesLayer3;
       // int[] triangleStripIndices;
        public static Texture2D frametestimg;
        public static Texture2D testimg;
        public static Texture2D manwalkingimg;
        public static Texture2D walklandimg;
        public static Texture2D pathsegment;
        public static Texture2D walksegment;
        public static Texture2D rockimg;
        public static Texture2D bushimg;
        public static Texture2D bush2img;
        Player player;
        Player player2;
        //List<GroundSegment> groundLayerOneList = new List<GroundSegment>();
        //List<GroundSegment> groundLayerTwoList = new List<GroundSegment>();
       // List<GroundSegment> groundLayerThreeList = new List<GroundSegment>();
        Vector2 cameraPos = new Vector2(0,0);
        //engine.Engine.Graphics.Camera cameraPos = Gengine.Engine.GEngine.Camera;
        public static Texture2D blank;
        public enum Biomes { Normal, Forest, Mountain, Snow, Village };
        //float max_heigth = ((((1000 * 100) / 5)));
        //int grwidth = 50;
        //int mountain_magnitude = max_heigth;
        List<Point> testpoints;
        private static Ground ground;
        private static Path path;
        int minimap = 200;
        public static Random random = new Random();
        Gengine.Engine.GEngine gengine;
        public bool debug_ = true;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gengine = new Gengine.Engine.GEngine(graphics, Content);

           // this.IsMouseVisible = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
           // graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here
            gengine.Initialize();
            Gengine.Engine.GEngine.Camera.Zoom = 1.5f;
            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, graphics.GraphicsDevice.Viewport.Width,     // left, right
                graphics.GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                         // near, far plane

            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
            ground = new Ground(basicEffect, random);
            path = new Path(ground); 

            player = new Player(new Vector3(100, (float)(ground.max_heigth / 2), 0), manwalkingimg);

            player.Transform.Drag = 0.60f;
            player2 = new Player(new Vector3(100, (float)(ground.max_heigth / 2)- 200, 0), manwalkingimg);

            player2.Transform.Drag = 0.60f;

            testpoints = new List<Point>();
            testpoints.Add(new Point(100, 0));
            testpoints.Add(new Point(100, 200));
            Gengine.ExtraMath.Fracture(0, 1, 100, 0,6, testpoints, random);


            Generatelayers();
           /* for (int h = 0; h < ground.groundLayerOneList.Count; h++)
            {
                //GameObject po = new GameObject(new Vector3(50*h, 400, 0), manwalkingimg, 50, 100);
                //po.Kill();

                Gengine.Engine.Graphics.DrawSprite _drawComponent = new Gengine.Engine.Graphics.DrawSprite(new Vector3(h*20,100, 0), manwalkingimg, 50, 100, 0, true, false, true);
                _drawComponent.Layer = h-10;
                Gengine.Engine.GEngine.drawManager.Add(_drawComponent);
            }*/
           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            gengine.LoadContent();
            manwalkingimg = Content.Load<Texture2D>("manwalking.png");
            frametestimg = Content.Load<Texture2D>("framestest.png");
            testimg = Content.Load<Texture2D>("trollskin.png");
            walklandimg = Content.Load<Texture2D>("walkLand.png");
            pathsegment = Content.Load<Texture2D>("pathSegment.png");
            walksegment = Content.Load<Texture2D>("walksegment.png");
            rockimg = Content.Load<Texture2D>("rock.png");
            bushimg = Content.Load<Texture2D>("bush.png");
            bush2img = Content.Load<Texture2D>("bush2.png");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            gengine.UnloadContent();
            testimg.Dispose();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            gengine.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                debug_ = !debug_;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                Gengine.Engine.GEngine.Camera.Transform.Acceleration = new Vector3(1.4f, 0, 0);
                //Gengine.Engine.GEngine.Camera.Position += new Vector3(100,0,0);
                //cameraPos.X += 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Gengine.Engine.GEngine.Camera.Transform.Acceleration = new Vector3(-1.4f, 0, 0);
                //Gengine.Engine.GEngine.Camera.Position += new Vector3(-100, 0, 0);
                //cameraPos.X -= 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Gengine.Engine.GEngine.Camera.Transform.Acceleration = new Vector3(0, -1.4f, 0);
                //Gengine.Engine.GEngine.Camera.Position += new Vector3(0, -100, 0);
                //cameraPos.Y -= 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Gengine.Engine.GEngine.Camera.Transform.Acceleration = new Vector3(0, 1.4f, 0);
                //Gengine.Engine.GEngine.Camera.Position += new Vector3(0, 100, 0);
                //cameraPos.Y += 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightShift))
            {
                Gengine.Engine.GEngine.Camera.Zoom += -0.01f;
                //Gengine.Engine.GEngine.Camera.Position += new Vector3(0, -100, 0);
                //cameraPos.Y -= 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightControl))
            {
                Gengine.Engine.GEngine.Camera.Zoom += 0.01f;
                //Gengine.Engine.GEngine.Camera.Position += new Vector3(0, 100, 0);
                //cameraPos.Y += 100;
            }
            //Gengine.Engine.GEngine.Camera.Transform.Acceleration = ((player.Transform.Position + new Vector3(25f, 50, 0)) - (Gengine.Engine.GEngine.Camera.Transform.Position))/1000;
            Gengine.Engine.GEngine.Camera.Transform.Acceleration += ((new Vector3((Mouse.GetState().X * (ground.grwidth * (ground.landwidth / 1000))), Mouse.GetState().Y * minimap, 0) + new Vector3(12.5f, 50, 0)) - (Gengine.Engine.GEngine.Camera.Transform.Position )) / 1000;
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Gengine.Engine.GEngine.drawManager.Clear();
                Generatelayers();
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {

                ground.SmoothGround(ground.groundLayerTwoList);
                ground.SmoothGround(ground.groundLayerOneList);
                ground.SmoothGround(ground.groundLayerThreeList);
                ground.SmoothGround(ground.groundLayerFourList);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player.Transform.Acceleration = new Vector3(0, -0.4f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.Transform.Acceleration = new Vector3(0, 0.4f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.Transform.Acceleration = new Vector3(-0.4f, 0, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.Transform.Acceleration = new Vector3(0.4f, 0, 0);
            }
            cameraPos = Gengine.Engine.GEngine.Camera.Positionv2;
            // TODO: Add your update logic here
            //player ground stuff
            /*int closest_index = (int)Math.Round((player.Transform.Position.X + 25) / ground.grwidth);
            int other = 0;
            if (ground.groundLayerTwoList[closest_index].X <= (player.Transform.Position.X + 25))
            {
                other = 1;
            }
            else
            {
                other = -1;
            }
            Point newpos = Gengine.ExtraMath.Project(new Point(ground.groundLayerTwoList[closest_index].X, ground.groundLayerTwoList[closest_index].Y), new Point(ground.groundLayerTwoList[closest_index + other].X, ground.groundLayerTwoList[closest_index + other].Y), new Point((int)player.Transform.Position.X + 25, (int)player.Transform.Position.Y + 100));
            player.Transform.Position = new Vector3(player.Transform.Position.X, newpos.Y-100, 0);*/
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            ground.Draw();
            DrawPath();

            if (debug_)
            {
                  DrawDebugGround(ground.groundLayerOneList, Color.Green, Color.Green);
                  DrawDebugGround(ground.groundLayerTwoList, Color.Red, Color.Red);
                  DrawDebugGround(ground.groundLayerThreeList, Color.Yellow, Color.Yellow);
                  DrawDebugGround(ground.groundLayerFourList, Color.Yellow, Color.Yellow);
            

                   Gengine.Engine.Graphics.DrawManager.spritebatch.Begin();
                   for (int i = 1; i < testpoints.Count; i++)
                   {
                       DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Pink, new Vector2(testpoints[i-1].X, testpoints[i-1].Y), new Vector2(testpoints[i].X, testpoints[i].Y));
           
                   }
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Pink, (cameraPos / minimap) + new Vector2(12, 0), ((cameraPos + new Vector2(1024, 0)) / minimap) + new Vector2(12, 0));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Pink, (cameraPos / minimap) + new Vector2(12, 0), ((cameraPos + new Vector2(0, 768)) / minimap) + new Vector2(12, 0));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Pink, ((cameraPos + new Vector2(1024, 0)) / minimap) + new Vector2(12, 0), ((cameraPos + new Vector2(1024, 768)) / minimap) + new Vector2(12, 0));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Pink, ((cameraPos + new Vector2(0, 768)) / minimap) + new Vector2(12, 0), ((cameraPos + new Vector2(1024, 768)) / minimap) + new Vector2(12, 0));

                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Orange, new Vector2(12, 0 ), new Vector2(1012, 0 ));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Orange, new Vector2(12, 0 + (ground.max_heigth / minimap)), new Vector2(1012, 0 + (ground.max_heigth / minimap)));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Orange, new Vector2(12, (ground.max_heigth / minimap) / 2), new Vector2(1012, (ground.max_heigth / minimap) / 2));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Orange, new Vector2(12, 0 + (ground.max_heigth / minimap) / 4), new Vector2(1012, 0 + (ground.max_heigth / minimap) / 4));
                  DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Orange, new Vector2(12, 0 + ((ground.max_heigth / minimap) / 2) + ((ground.max_heigth / minimap) / 4)), new Vector2(1012, 0 + ((ground.max_heigth / minimap) / 2) + ((ground.max_heigth / minimap) / 4)));
            
                  Gengine.Engine.Graphics.DrawManager.spritebatch.End();
                  
            }
            gengine.Draw(gameTime);
            base.Draw(gameTime);
        }
        public static void DrawLine(SpriteBatch batch, Texture2D blank, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = (Vector2.Distance(point1, point2) / blank.Width);
            float nwidth = (width / blank.Height);
            bool A = (point1.X >= (0 - Vector2.Distance(point1, point2)-10));
            bool B = (point1.X <= (Gengine.Engine.GEngine.graphicsDM.PreferredBackBufferWidth+10));
            bool C = (point1.Y >= 0 - Vector2.Distance(point1, point2) - width - 10);
            bool D = (point1.Y <= (Gengine.Engine.GEngine.graphicsDM.PreferredBackBufferHeight + Vector2.Distance(point1, point2) + 10));

            if (A&&B&&C&&D)
            {
                batch.Draw(blank, point1, null, color, angle, Vector2.Zero, new Vector2(length, nwidth), SpriteEffects.None, 0);
            }
        }
        
        void DrawDebugGround(List<GroundSegment> groundlist, Color maincolor, Color minicolor)
        {
            Color color = Color.Blue;
            Gengine.Engine.Graphics.DrawManager.spritebatch.Begin();
            for (int i = 1; i < groundlist.Count; i++)
            {
                switch (groundlist[i].biome)
                {
                    case (int)Biomes.Normal: color = Color.Yellow; break;
                    case (int)Biomes.Forest: color = Color.Green; break;
                    case (int)Biomes.Mountain: color = Color.Brown; break;
                    case (int)Biomes.Snow: color = Color.White; break;
                    case (int)Biomes.Village: color = Color.Violet; break;
                }
                Vector2 extravect = Vector2.Normalize(((groundlist[i - 1].position - groundlist[i].position)));
                extravect *= 10;
                DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, color, new Vector2((groundlist[i - 1].position.X / (ground.grwidth * (ground.landwidth / 1000))), (groundlist[i - 1].position.Y / minimap)) + new Vector2(12, 0), new Vector2((groundlist[i].position.X / (ground.grwidth * (ground.landwidth / 1000))), (groundlist[i].position.Y / minimap)) + new Vector2(12, 0));
               // DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, walklandimg, 20, groundlist[i - 1].color, groundlist[i - 1].position - cameraPos+extravect , groundlist[i].position - cameraPos-extravect);

                //DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, blank, 1, Color.Blue, groundlist[i].position - cameraPos, new Vector2(groundlist[i].X, max_heigth) - cameraPos);
            }
            Gengine.Engine.Graphics.DrawManager.spritebatch.End();
        }
        void DrawPath()
        {

            Gengine.Engine.Graphics.DrawManager.spritebatch.Begin();
            for (int i = 1; i < path.path.Count; i++)
            {
                Color color = Color.Red;
                if (path.path[i].status == "stairs")
                {
                    color = Color.LightBlue;
                }
                DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, Game1.blank, 1 * Gengine.Engine.GEngine.Camera.Zoom, color, ((new Vector2(path.path[i].Position.X, path.path[i].Position.Y) - cameraPos + Gengine.Engine.GEngine.Camera.ScreenMid2D) * Gengine.Engine.GEngine.Camera.Zoom), ((new Vector2(path.path[i].Parent.Position.X, path.path[i].Parent.Position.Y) - cameraPos + Gengine.Engine.GEngine.Camera.ScreenMid2D) * Gengine.Engine.GEngine.Camera.Zoom));

            }
            for (int j = 0; j < path.openPoints.Count; j++)
            {
                DrawLine(Gengine.Engine.Graphics.DrawManager.spritebatch, Game1.blank, 1 * Gengine.Engine.GEngine.Camera.Zoom, Color.Orange, ((new Vector2(path.openPoints[j].Position.X, path.openPoints[j].Position.Y) - cameraPos + Gengine.Engine.GEngine.Camera.ScreenMid2D) * Gengine.Engine.GEngine.Camera.Zoom), ((new Vector2(path.openPoints[j].Parent.Position.X, path.openPoints[j].Parent.Position.Y) - cameraPos + Gengine.Engine.GEngine.Camera.ScreenMid2D) * Gengine.Engine.GEngine.Camera.Zoom));

            }
            Gengine.Engine.Graphics.DrawManager.spritebatch.End();

        }
        void Generatelayers()
        {
            ground.GenerateGround();
            path.CreatePath();
            for (int vv = 0; vv < path.path.Count; vv++)
            {
                Gengine.Engine.Graphics.DrawSprite _drawCompone = new Gengine.Engine.Graphics.DrawSprite(new Vector3(path.path[vv].myground.position.X, path.path[vv].myground.position.Y + 100, 0), Game1.bushimg, 21, 16, 0, true, false, true);
                _drawCompone.Alpha = 1.0f;
                _drawCompone.Color = path.path[vv].myground.color;
                _drawCompone.Layer = path.path[vv].mygroundIndexY;
                Gengine.Engine.GEngine.drawManager.Add(_drawCompone);


            }
            cameraPos = new Vector2(0, ground.max_heigth / 2 - (Gengine.Engine.GEngine.graphicsDM.PreferredBackBufferHeight / 2));
        }
        public static Ground Ground
        {
            get { return ground; }
            set { ground = value; }

        }
        public static Path Path
        {
            get { return path; }
            set { path = value; }

        }
        
        
        
    }
}
