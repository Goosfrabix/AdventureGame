using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine.Engine.Graphics
{
    public class DrawManager: DrawComponent
    {
        private List<DrawComponent> contentList;
        private ContentManager _content;
        public static SpriteBatch spritebatch;
         public DrawManager(ContentManager content)
        {
            _content = content;
            contentList = new List<DrawComponent>();
        }
         public void LoadContent()
         {
             spritebatch = new SpriteBatch(GEngine.graphicsDM.GraphicsDevice);
         }
         public void UnloadContent()
         {
         }

         public void Add(DrawComponent component)
        {
            contentList.Add(component);
        }
         public void AddRange(params DrawComponent[] components)
        {
            contentList.AddRange(components);
        }
         public void Delete(DrawComponent component)
        {
            contentList.Remove(component);
            
        }
        public void Clear()
        {
            contentList.Clear();

        }
         public void Draw(GameTime gameTime)
        {

            DrawManager.spritebatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            foreach (var childcontent in contentList)
            {
                childcontent.Draw(gameTime);
            }

            DrawManager.spritebatch.End();
        }
    }
}
