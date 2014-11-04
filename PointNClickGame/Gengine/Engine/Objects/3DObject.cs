using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gengine.Engine.Graphics;

namespace Gengine.Engine.Objects
{
    public class _3DObject:Object
    {
        protected Model model;
        //protected DrawModel _drawModel;
        public _3DObject(Vector3 position, Model content)
            : base(position)
        {

            model = content;
            _drawComponent = new Engine.Graphics.DrawModel(this, model);
            Engine.GEngine.drawManager.Add(_drawComponent);
        }

        public Engine.Graphics.DrawModel GetDrawModel()
        {
            return (DrawModel)_drawComponent;
        }
    }
}
