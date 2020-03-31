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
        float rotation = 0;
        public Player(Vector2 pos, SurfaceContainer surface, TextureContainer textureContainer)
        {
            position = pos;
            collisionBox = new BoundingBox(16, 16);
            surface.InitiateEntityInChunks(this);
            velocity = new Vector2(0, 0);
            Texture[] playerTextures = new Texture[] { textureContainer.GetTexture("orcrunning") };
            drawArray = new Drawable[] { new AnimationRotated(playerTextures, new Vector2i(128, 128), new Vector2f(0,0), new Vector2f(0, -28) , new Vector2f(1,1),8, 8, "Forward", 0.0f) };
            collisionMask = CollisionLayer.EntityPhysical | CollisionLayer.TerrainSolid;
            mapColor = Color.Magenta;
        }
        /// <summary>
        /// TODO: Add inheritance structure
        /// </summary>
        public void Update()
        {
            ///TODO: Move this
            BoundingBox.ApplyPhysicalCollision(this, velocity);
            if (velocity.x != 0 || velocity.y != 0)
            {
                rotation = velocity.GetRotation() + 180.0f;
            }
            drawArray[0].SetRotation(rotation);
            drawArray[0].Update();
            drawArray[0].SetAnimationSpeed(60/velocity.GetMagnitude());
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
            if (input.GetKeyHeld(InputBindings.moveUp, false))
            {
                velocity.Add(0, -8);
            }
            if (input.GetKeyHeld(InputBindings.moveDown, false))
            {
                velocity.Add(0, 8);
            }
            if (input.GetKeyHeld(InputBindings.moveLeft, false))
            {
                velocity.Add(-8, 0);
            }
            if(input.GetKeyHeld(InputBindings.moveRight, false))
            {
                velocity.Add(8, 0);
            }
        }
    }
}
