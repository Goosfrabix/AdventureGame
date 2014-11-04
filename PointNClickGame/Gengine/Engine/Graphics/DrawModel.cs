using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine.Engine.Graphics
{
    public class DrawModel : DrawComponent
    {
        private Model model;
        private Objects.Object _parent;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        public DrawModel(Objects.Object parent, Model content)
        {
            model = content;
            _parent = parent;
        }
        public void Draw(GameTime gameTime)
        {
            world = Matrix.CreateTranslation(_parent.Transform.Position);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = GEngine.Camera.view;
                    effect.Projection = GEngine.Camera.projection;
                }

                mesh.Draw();
            }  
        }
        public void ChangeModel(Model newContent)
        {
            model = newContent;
        }
    }
}
