using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine.Engine.Components
{
    public interface Component
    {
        void Update(GameTime gametime);
    }
}
