using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gengine.Engine;
using Gengine.Engine.Components;

namespace Gengine.Controllers
{
    class NullController : Component
    {
        public NullController()
        {
        }
        void Component.Update(GameTime gametime)
        {
            //System.Console.Write("Update NullController\n");
            //holds nothhing is a null Object
        }
    }
}
