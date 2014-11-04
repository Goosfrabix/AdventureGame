using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine.Engine.Graphics
{
    public interface DrawComponent
    {
        void Draw(GameTime gameTime);
    }
}
