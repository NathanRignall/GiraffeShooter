using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Animation : Component
    {
        
        // animation frame struct
        public struct Frame
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public int Duration;
            
            public Frame(int x, int y, int width, int height, int duration)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Duration = duration;
            }
        }
        
        private Frame[] _frames;
        private int _currentFrame;
        private double _lastFrameTime;

        public Animation(Frame[] frames)
        {
            _frames = frames;
            _currentFrame = 0;
            _lastFrameTime = 0;
            AnimationSystem.Register(this);
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // only update if we have more than one frame
            if (_frames.Length > 1)
            {
                // if we have passed the duration of the current frame
                if (gameTime.TotalGameTime.TotalMilliseconds - _lastFrameTime > _frames[_currentFrame].Duration)
                {
                    // move to the next frame
                    _currentFrame++;
                    // if we have reached the end of the animation, loop back to the start
                    if (_currentFrame >= _frames.Length)
                    {
                        _currentFrame = 0;
                    }
                    // update the last frame time
                    _lastFrameTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // update sprite
            var sprite = entity.GetComponent<Sprite>();
            sprite.SourceRectangle = new Rectangle(_frames[_currentFrame].X, _frames[_currentFrame].Y, _frames[_currentFrame].Width, _frames[_currentFrame].Height);
        }
        
        public override void Deregister()
        {
            AnimationSystem.Deregister(this);
        }
    }
}

