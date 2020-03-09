﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics; //remove this later
using SFML.System;

namespace EngineeringCorpsCS
{
    class Player: Entity, IInputSubscriber
    {
        Vector2 velocity;
        public RotatedAnimation playerTest;
        float rotation = 0;
        public Player(Vector2 pos, SurfaceContainer surface, TextureContainer textureContainer)
        {
            position = pos;
            collisionBox = new BoundingBox(24, 24);
            surface.InitiateEntityInChunks(this);
            velocity = new Vector2(0, 0);
            Texture[] playerTextures = new Texture[] { textureContainer.GetTexture("orcrunning") };
            playerTest = new RotatedAnimation(playerTextures, new Vector2i(128, 128), 8, 8, "Forward", 12.0f);
            collisionMask = CollisionLayer.EntityPhysical | CollisionLayer.TerrainSolid;
        }
        /// <summary>
        /// TODO: Add inheritance structure
        /// </summary>
        public void Update()
        {
            int intervals = (int)Math.Ceiling(velocity.GetMagnitude() / Props.maxVelocity);
            for(int i = 0; i < intervals; i++)
            {
                Vector2 velocityCopy = velocity.Copy();
                velocityCopy.Scale(1.0f / intervals);
                BoundingBox.ApplyPhysicalCollision(this, velocityCopy);
                position.Add(velocityCopy);
            }
            
            rotation = velocity.GetRotation() + 180.0f;
            playerTest.SetRotation(rotation);
            playerTest.Update();
            playerTest.GetAnimationFrame().Position = new Vector2f(position.x, position.y - 16.0f);
            playerTest.SetAnimationSpeed(60/velocity.GetMagnitude());
            velocity.Set(0, 0);
        }
        override public void OnClick()
        {

        }

        public void SubscribeToInput(InputManager input)
        {
            input.AddInputSubscriber(this, false);
        }
        public void UnsubscribeToInput(InputManager input)
        {
            input.RemoveInputSubscriber(this, false);
        }
        public void HandleInput(InputManager input)
        {
            if (input.keyHeld[InputBindings.moveUp])
            {
                velocity.Add(0, -48);
            }
            if (input.keyHeld[InputBindings.moveDown])
            {
                velocity.Add(0, 48);
            }
            if (input.keyHeld[InputBindings.moveLeft])
            {
                velocity.Add(-48, 0);
            }
            if(input.keyHeld[InputBindings.moveRight])
            {
                velocity.Add(48, 0);
            }
        }
    }
}
