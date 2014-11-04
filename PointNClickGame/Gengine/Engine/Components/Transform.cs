using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gengine.Engine.Components
{
    public class Transform: Component
    {

        private Vector3 position_;
        private Vector3 velocity_;
        private Vector3 acceleration_;
        private float drag_ = 0.85f;

        public Transform(Vector3 position)
        {
            position_ = position;
        }
        public void Update(GameTime gametime)
        {
            velocity_ += acceleration_ * ((float)gametime.ElapsedGameTime.TotalMilliseconds / 10);
            position_ += velocity_ * ((float)gametime.ElapsedGameTime.TotalMilliseconds / 10);

            velocity_.X *= drag_;
            velocity_.Y *= drag_;
            velocity_.Z *= drag_;
            //System.Console.Write("====================================\n");
            //System.Console.Write("position_:" + position_.Length() + "\n");
            //System.Console.Write("velocity_:" + velocity_.Length() + "\n");
            //System.Console.Write("acceleration_:" + acceleration_.Length() + "\n");
            acceleration_ = Vector3.Zero;
        }
        //getter & setter
        public Vector3 Acceleration
        {
            get { return acceleration_; }
            set { acceleration_ = value; }
        }
        public Vector3 Velocity
        {
            get { return velocity_; }
            set { velocity_ = value; }
        }
        public Vector3 Position
        {
            get { return position_; }
            set { position_ = value; }
        }
        public float Drag
        {
            get { return drag_; }
            set { drag_ = value; }
        }
    }
}
