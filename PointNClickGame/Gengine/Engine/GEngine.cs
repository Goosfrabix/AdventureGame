using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gengine.Engine.Components;
using Gengine.Engine.Graphics;
using Gengine.Engine.Physics;
namespace Gengine.Engine
{
    public class GEngine
    {
        public static ComponentManager componentManager;
        public static DrawManager drawManager;
        public static CollisionChecker collisionChecker;
        public static GraphicsDeviceManager graphicsDM;
        private static Camera camera;

        public GEngine(GraphicsDeviceManager graphics, ContentManager content)
        {
            graphicsDM = graphics;
            drawManager = new DrawManager(content);
            

        }
        public void Initialize()
        {
            componentManager = new ComponentManager();
            collisionChecker = new CollisionChecker();
            camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, -100));//200,175

        }
        public void LoadContent()
        {
            drawManager.LoadContent();

        }
        public void UnloadContent()
        {
            drawManager.UnloadContent();
        }
        public void Update(GameTime gameTime)
        {
            componentManager.Update(gameTime);
            collisionChecker.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            drawManager.Draw(gameTime);

        }
        public static Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }
    }
}
