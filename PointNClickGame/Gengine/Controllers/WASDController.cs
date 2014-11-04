using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Gengine.Engine;
using Gengine.Engine.Components;

namespace Gengine.Controllers
{
    public class WASDController : Component
    {
        private Gengine.Engine.Objects.Object _player;
        private float Xspeed = 0;
        private float Yspeed = 0;
        public WASDController(Gengine.Engine.Objects.Object player)
        {
            _player = player;
        }

        void Component.Update(GameTime gametime)
        {
            Xspeed = 0;
            Yspeed = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Xspeed = -1;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Xspeed = 1;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Yspeed = -1;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Yspeed = 1;

            _player.Transform.Acceleration = new Vector3(Xspeed, 0, Yspeed);
        }
    }
}
