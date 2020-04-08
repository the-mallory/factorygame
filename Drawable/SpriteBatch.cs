﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EngineeringCorpsCS
{
    class SpriteBatch
    {
        RenderTexture spriteBatch;
        RenderStates currentRenderState;
        VertexArray spriteBatchArray;
        public SpriteBatch(RenderWindow window)
        {
            spriteBatch = new RenderTexture(window.Size.X, window.Size.Y);
            spriteBatchArray = new VertexArray(PrimitiveType.Triangles);
            currentRenderState = new RenderStates();
        }

        public void Initialize(View gameView)
        {
            spriteBatch.SetView(gameView);
            spriteBatch.Clear(Color.Transparent);
        }

        public void Draw(Sprite drawnSprite)
        {
            Draw(drawnSprite.Texture, drawnSprite.Position, drawnSprite.TextureRect, drawnSprite.Color, drawnSprite.Scale, drawnSprite.Origin, drawnSprite.Rotation);
        }

        public void Draw(Texture texture, Vector2f position, IntRect rec, Color color, Vector2f scale, Vector2f origin, float rotation)
        {
            if(currentRenderState.Texture == null || !ReferenceEquals(currentRenderState.Texture, texture))
            {
                SwitchTextures(texture);
            }
            /* Could implement a capacity before restarting drawing
            if (count*4 >= capacity)
            {
                display(false);
                if(capacity<MaxCapacity)
                {
                        delete[] vertices;
                        capacity *= 2;
                        if(capacity > MaxCapacity) capacity = MaxCapacity;
                        vertices = new Vertex[capacity];
                }
            }  */              
            /*
            int rot = (int)(rotation / 360 * LookupSize) & (LookupSize - 1);
            float _sin = getSin[rot];
            float _cos = getCos[rot];*/

            float _sin = 0, _cos = 1;
            if (rotation != 0)
            {
                rotation = rotation * (float)Math.PI / 180.0f;
                _sin = (float)Math.Sin(rotation);
                _cos = (float)Math.Cos(rotation);
            }

            origin.X *= scale.X;
            origin.Y *= scale.Y;
            scale.X *= rec.Width;
            scale.Y *= rec.Height;
 
            float pX = -origin.X;
            float pY = -origin.Y;
            //Top left
            spriteBatchArray.Append(new Vertex(new Vector2f(pX * _cos - pY * _sin + position.X, pX * _sin + pY * _cos + position.Y), color, new Vector2f(rec.Left, rec.Top)));
            pX += scale.X;
            //Top right
            spriteBatchArray.Append(new Vertex(new Vector2f(pX * _cos - pY * _sin + position.X, pX * _sin + pY * _cos + position.Y), color, new Vector2f(rec.Left + rec.Width, rec.Top)));
            pX -= scale.X;
            pY += scale.Y;
            //Bottom left
            spriteBatchArray.Append(new Vertex(new Vector2f(pX * _cos - pY * _sin + position.X, pX * _sin + pY * _cos + position.Y), color, new Vector2f(rec.Left, rec.Top + rec.Height)));
            pX += scale.X;
            pY -= scale.Y;
            //Top right
            spriteBatchArray.Append(new Vertex(new Vector2f(pX * _cos - pY * _sin + position.X, pX * _sin + pY * _cos + position.Y), color, new Vector2f(rec.Left + rec.Width, rec.Top)));
            pY += scale.Y;
            //Bottom right
            spriteBatchArray.Append(new Vertex(new Vector2f(pX * _cos - pY * _sin + position.X, pX * _sin + pY * _cos + position.Y), color, new Vector2f(rec.Left + rec.Width, rec.Top + rec.Height)));
            pX -= scale.X;
            spriteBatchArray.Append(new Vertex(new Vector2f(pX * _cos - pY * _sin + position.X, pX * _sin + pY * _cos + position.Y), color, new Vector2f(rec.Left, rec.Top + rec.Height)));
        }

        private void SwitchTextures(Texture texture)
        {
            spriteBatch.Draw(spriteBatchArray, currentRenderState);
            spriteBatchArray.Clear();
            currentRenderState = new RenderStates(texture);
        }

        public Sprite Finalize()
        {
            spriteBatch.Draw(spriteBatchArray, currentRenderState);
            spriteBatchArray.Clear();
            spriteBatch.Display();
            return new Sprite(spriteBatch.Texture);
        }

        public void HandleResize(Object s, SizeEventArgs e)
        {
            spriteBatch.Dispose();
            spriteBatch = new RenderTexture(e.Width, e.Height);
        }
    }
}