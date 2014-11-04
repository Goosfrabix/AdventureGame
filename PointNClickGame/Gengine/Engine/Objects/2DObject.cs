using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gengine.Engine.Graphics;

namespace Gengine.Engine.Objects
{
    public class _2DObject : Object
    {
        protected Texture2D texture;
        //protected DrawSprite _drawSprite;
        public _2DObject(Vector3 position, Texture2D content, int frameWidth, int frameHeight, float frameTime, bool looped = true, bool reverse = false, bool cameraFollow = false)
            : base(position)
        {

            texture = content;
            _drawComponent = new Engine.Graphics.DrawSprite(this, texture, frameWidth, frameHeight, frameTime, looped, reverse, cameraFollow);
            Engine.GEngine.drawManager.Add(_drawComponent);
        }
       

        public Engine.Graphics.DrawSprite GetDrawSprite()
        {
            return (DrawSprite)_drawComponent;
        }
        
    }
}
