using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine.Engine.Graphics
{
    public class DrawSprite : DrawComponent
    {
        private Texture2D texture;
        private Objects.Object _parent;
        private Vector3 _position;

        private float _frameTime;
        private int _frameIndex;
        private bool _isLooping;
        private float _time;
        private bool _isPlaying;
        private int _frameWidth;
        private int _frameHeight;

        private int _frameRow = 0;
        private int _frameCol = 0;

        private bool _reverse;
        private bool _cameraFollow;

        private float _alpha = 1.0f;
        private Vector2 _size = new Vector2(10, 10);
        private float _depth = 0f;
        private float _rotation = 0.0f;
        private Vector2 _origin = new Vector2(0,0);
        private Color _color = Color.White;

        public bool test = false;

        public DrawSprite(Objects.Object parent, Texture2D content,int frameWidth,int frameHeight, float fps, bool isLooping, bool reverse,bool cameraFollow)
        {
            texture = content;
            _parent = parent;
            _position = parent.Transform.Position;

            _frameTime = 1/fps;
            _isLooping = isLooping;
            _isPlaying = true;
            Height = _frameHeight = frameHeight;
            Width = _frameWidth = frameWidth;

            _reverse = reverse;
            _cameraFollow = cameraFollow;

            if (_reverse)
            {
                _frameIndex = FrameCount;
            }
        }
        public DrawSprite(Vector3 position, Texture2D content, int frameWidth, int frameHeight, float fps, bool isLooping, bool reverse, bool cameraFollow)
        {
            texture = content;
            _position = position;

            _frameTime = 1 / fps;
            _isLooping = isLooping;
            _isPlaying = true;
            _frameHeight = frameHeight;
            _frameWidth = frameWidth;

            _reverse = reverse;
            _cameraFollow = cameraFollow;

            if (_reverse)
            {
                _frameIndex = FrameCount;
            }
        }
        public void Draw(GameTime gameTime)
        {
            if (_parent != null)
            {
                _position = _position = _parent.Transform.Position;
            }
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while ((_time > _frameTime) && _isPlaying)
            {
                _time -= _frameTime;
                if (!_reverse)
                {
                    if (_isLooping)
                    {
                        _frameIndex = (_frameIndex + 1) % FrameCount;
                    }
                    else
                    {
                        _frameIndex = Math.Min(_frameIndex + 1, FrameCount - 1);
                    }
                }
                else
                {
                    if (_isLooping)
                    {
                        if (_frameIndex <= 0)
                        {
                            _frameIndex = FrameCount;
                        }
                        _frameIndex = (_frameIndex - 1) % FrameCount;
                    }
                    else
                    {
                        _frameIndex = Math.Max(_frameIndex - 1, 0);
                    }
                }
            }
            if (!_isPlaying)
            {
                _frameIndex = 0;
                _time = 0;
            }

            _frameCol = _frameIndex % (texture.Width/_frameWidth);

            _frameRow = (_frameIndex - _frameCol) / (texture.Width / _frameWidth);

            _frameCol *= _frameWidth;
            _frameRow *= _frameHeight;
            
            Rectangle source = new Rectangle(_frameCol, _frameRow, _frameWidth, _frameHeight);
            Vector2 cameraFollow = Vector2.Zero;
            if(_cameraFollow){
                cameraFollow = GEngine.Camera.Positionv2 - GEngine.Camera.ScreenMid2D;
            }
            Vector2 spritePos = new Vector2(_position.X - cameraFollow.X, _position.Y - cameraFollow.Y);
            //if in screen draw sprite
            bool A = (spritePos.X >= (-source.Width - 10));
            bool B = (spritePos.X <= ((Gengine.Engine.GEngine.graphicsDM.PreferredBackBufferWidth / GEngine.Camera.Zoom) + 10));
            bool C = (spritePos.Y >= (-source.Height - 10));
            bool D = (spritePos.Y <= ((Gengine.Engine.GEngine.graphicsDM.PreferredBackBufferHeight / GEngine.Camera.Zoom) + 10));
            
            if(A&&B&&C&&D){
                DrawManager.spritebatch.Draw(texture, new Rectangle((int)((spritePos.X * GEngine.Camera.Zoom)), (int)(spritePos.Y * GEngine.Camera.Zoom), (int)(_size.X * GEngine.Camera.Zoom), (int)(_size.Y * GEngine.Camera.Zoom)), source, _color * _alpha, _rotation, _origin, SpriteEffects.None, _depth);
                
            }
            
 
        }
        public void ChangeModel(Texture2D newContent)
        {
            texture = newContent;
        }
        public int FrameCount
        {
            get
            {
                if (texture.Height > _frameHeight)
                {
                    int y_index = texture.Height / _frameHeight;
                    return (texture.Width / _frameWidth) * y_index;
                }
                else
                {
                    return texture.Width / _frameWidth;
                }
            }
        }
        public Vector2 Origin
        {
            get
            {
               // return new Vector2(_frameWidth / 2.0f, _frameHeight);
                return _origin;
            }
            set { _origin = value; }
        }
        public float Alpha
        {
            get { return _alpha; }
            set {_alpha = value; }

        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }

        }
        public float Layer
        {
            get { return _depth; }
            set { _depth = value/10; }

        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }

        }
        public float Angle
        {
            get { return Rotation * (float)(180 / Math.PI); }
            set { Rotation = value * (float)(Math.PI / 180); }

        }
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }

        }
        public float Width
        {
            get { return _size.X; }
            set { _size.X = value; }

        }
        public float Height
        {
            get { return _size.Y; }
            set { _size.Y = value; }

        }
    }
}
