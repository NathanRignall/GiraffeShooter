using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

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
            public bool End;
            
            public Frame(int x, int y, int width, int height, int duration, bool end = false)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Duration = duration;
                End = end;
            }
        }
        
        public Frame[] Frames { get; private set; }
        private int _currentFrame;
        private double _lastFrameTime;
        public bool Finished { get; private set; }

        public Animation(Frame[] frames)
        {
            Frames = frames;
            _currentFrame = 0;
            _lastFrameTime = 0;
            Finished = false;
            AnimationSystem.Register(this);
        }
        
        public void SetFrames(Frame[] frames, bool Reset = true)
        {
            if (Frames != frames | !Reset)
            {
                _currentFrame = 0;
                _lastFrameTime = 0;
                Finished = false;
            }
            Frames = frames;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ContextManager.Paused)
                return;
            
            // only update if we have more than one frame
            if (Frames.Length > 1 && !Finished)
            {
                // if we have passed the duration of the current frame
                if (gameTime.TotalGameTime.TotalMilliseconds - _lastFrameTime > Frames[_currentFrame].Duration)
                {
                    // move to the next frame
                    _currentFrame++;

                    // if we have reached the end of the animation, loop back to the start
                    if (_currentFrame >= Frames.Length)
                    {
                        _currentFrame = 0;
                    }
                    
                    // if the animation is not set to loop, set the finished flag
                    if (Frames[_currentFrame].End)
                    {
                        Finished = true;
                    }
                    
                    // update the last frame time
                    _lastFrameTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // update sprite
            var sprite = entity.GetComponent<Sprite>();
            sprite.SourceRectangle = new Rectangle(Frames[_currentFrame].X, Frames[_currentFrame].Y, Frames[_currentFrame].Width, Frames[_currentFrame].Height);
        }
        
        public override void Deregister()
        {
            AnimationSystem.Deregister(this);
        }
    }
}

