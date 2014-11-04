using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Gengine.Engine;
using Gengine.Engine.Components;

namespace Gengine.Controllers
{
    public class FollowMouseController : Component
    {
        private Gengine.Engine.Objects.Object _parent;
        public FollowMouseController(Gengine.Engine.Objects.Object parent)
        {
           
            _parent = parent;
        }
        void Component.Update(GameTime gametime)
        {
            _parent.Transform.Position = new Vector3(Mouse.GetState().X * 100, Mouse.GetState().Y * 100, 0);
        }
    }
}
