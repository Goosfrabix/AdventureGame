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

namespace Gengine.Engine.Graphics
{
    public class Camera : Gengine.Engine.Objects.Object
    {
        public Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        public Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 1000f);

        //private Vector3 _position;
        private Vector3 _target;
        private float _zoomAmount = 1.0f;
        public Camera(Vector3 position, Vector3 target)
            : base(position)
        {
            //_position = position;
            _target = target;
            view = Matrix.CreateLookAt(this.Transform.Position, _target, CalculateUpVector());
        }
        private Vector3 CalculateUpVector()
        {
            Vector3 x = new Vector3(-1,0,0);
            Vector3 z = this.Transform.Position - _target;
            x.Normalize();
            z.Normalize();
            Vector3 y = Vector3.Cross(x, z);
            y.Normalize();
            return y;
        }
        public Vector3 Position
        {
            get { return this.Transform.Position; }
            set { this.Transform.Position = value; }
        }
        public Vector2 Positionv2
        {
            get { return new Vector2(this.Transform.Position.X, this.Transform.Position.Y); }
            set { this.Transform.Position = new Vector3(value.X, value.Y,0); }
        }
        public float Zoom
        {
            get { return _zoomAmount; }
            set { if (value > 0) { _zoomAmount = value; } }
        }
        public Vector2 ScreenMid2D
        {
            get { return (new Vector2((GEngine.graphicsDM.PreferredBackBufferWidth / 2), (GEngine.graphicsDM.PreferredBackBufferHeight / 2)) / Zoom); }
        }
    }
}
