using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Graphics;

namespace GiraffeShooterClient.Entity
{
    class Aim : Component
    {
        public float Rotation { get; private set; }

        private Texture2D _aimTexture;
        private Rectangle _sourceRectangle { get; set; }
        
        public Aim()
        {
            Rotation = 0f;
            _aimTexture = AssetManager.ShootSpriteTexture;
            _sourceRectangle = new Rectangle(0, 0, _aimTexture.Width, _aimTexture.Height);
            
            AimSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            // if no touch screen
            if (!InputManager.TouchConnected && !ContextManager.Paused)
            {
                // get the mouse position
                Vector2 mousePosition = InputManager.CurrentMouseState.Position.ToVector2();
            
                // use the mouse position to calculate the rotation from 0,0
                Vector2 delta = mousePosition - ScreenManager.Size / 2;
                Rotation = (float)Math.Atan2(delta.Y, delta.X);
            }
        }
        
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
            // calculate the position of the sprite
            var physics = entity.GetComponent<Physics>();
            var cameraOffset = Camera.Offset;
            var aimOffset = new Vector2(0, -0.35f);
            
            // check what state the player is in
            var player = entity as Player;
            if (player != null)
            {
                // if the player is dead, move the aim up a bit
                if (player.PlayerState == Player.State.Shooting)
                {
                    aimOffset = new Vector2(0, -0.7f);
                }
            }
            
            // calculate the position
            var position = (new Vector2(physics.Position.X, physics.Position.Y) + aimOffset) * 32f + cameraOffset;

            // create the destination rectangle
            var destinationRectangle = new Rectangle((int)Math.Ceiling(position.X * Camera.Zoom), (int)Math.Ceiling(position.Y * Camera.Zoom), 
                (int)Math.Ceiling(_sourceRectangle.Width * Camera.Zoom), (int)Math.Ceiling(_sourceRectangle.Height * Camera.Zoom));
            
            // draw the sprite
            spriteBatch.Draw(_aimTexture, destinationRectangle, _sourceRectangle, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0f);

        }

        public void HandleEvents(List<Event> events)
        {
            if (ContextManager.Paused)
                return;
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.StickRightMove:

                        // use delta to calculate rotation
                        Vector2 delta = e.Delta;
                        
                        // set rotation
                        Rotation = ((float)Math.Atan2(delta.Y, delta.X));

                        break;
                }
            }
        }
        
        public override void Deregister()
        {
            AimSystem.Deregister(this);
        }
    }
}

