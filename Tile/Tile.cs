﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace EngineeringCorpsCS
{
    class Tile: Base
    {
        public Texture tileSheet { get; protected set; } //texture with terrain tiles
        public byte tileType { get; protected set; } //number specifying the type of the tile (corresponding to the type in the terrain array and tilemanager collection)
        public CollisionLayer collisionMask { get; protected set; } //Whatever layers are set here is what will determine its collision type

        float speedModifier; //modifier for speed of entities on this tile
        Color shade; //Apply a color to the tile
        Color mapColor; //Color that will be represented in minimap
        

        public Tile(Texture tileSheet, byte tileType, string name, float speedModifier, Color shade, Color mapColor, CollisionLayer collisionMask)
        {
            this.tileSheet = tileSheet;
            this.tileType = tileType;
            this.speedModifier = speedModifier;
            this.shade = shade;
            this.mapColor = mapColor;
            this.collisionMask = collisionMask;
            this.name = name;
        }

        public void AppendTerrainVertices(VertexArray vertexArray, ChunkManager chunkManager, int[]pos, List<byte> impassableTileTypes)
        {
            int[] cXY = new int[] { pos[0] * Props.chunkSize, pos[1] * Props.chunkSize };
            for (int i = 0; i < Props.chunkSize; i++)
            {
                for (int j = 0; j < Props.chunkSize; j++)
                {
                    //First check if tile is present at location, if it is then simply append vertices of the regular tile
                    if (chunkManager.GetTileFromWorldInt(cXY, i, j) == tileType)
                    {
                        int variantRegular = ((i * Props.chunkSize + j) * 11) % 8;
                        variantRegular = 0;
                        //Append regular tile variation
                        //first triangle
                        float oX = (cXY[0] + i) * Props.tileSize;
                        float oY = (cXY[1] + j) * Props.tileSize;
                        vertexArray.Append(new Vertex(new Vector2f(oX, oY),
                            new Vector2f(Props.tileSize * variantRegular, 0.0f))); //top left

                        vertexArray.Append(new Vertex(new Vector2f(oX + Props.tileSize, oY),
                            new Vector2f(Props.tileSize * (variantRegular + 1), 0.0f))); //top right

                        vertexArray.Append(new Vertex(new Vector2f(oX, oY + Props.tileSize),
                            new Vector2f(Props.tileSize * variantRegular, Props.tileSize))); //bottom left

                        //second triangle
                        vertexArray.Append(new Vertex(new Vector2f(oX + Props.tileSize, oY),
                            new Vector2f((Props.tileSize) * (variantRegular + 1), 0.0f))); //top right

                        vertexArray.Append(new Vertex(new Vector2f(oX + Props.tileSize, oY + Props.tileSize),
                            new Vector2f(Props.tileSize * (variantRegular + 1) , Props.tileSize))); //bottom right

                        vertexArray.Append(new Vertex(new Vector2f(oX, oY + Props.tileSize),
                            new Vector2f(Props.tileSize * (variantRegular), Props.tileSize))); //bottom left

                        continue;
                    }

                    //Next check for tile transitions
                    int value = 0;
                    int variant = ((i * Props.chunkSize + j) * 7) % 4; //variant is determine by whether the transition is into an impassable tile
                    if (impassableTileTypes.Contains(chunkManager.GetTileFromWorldInt(cXY, i, j)))
                    {
                        variant += 4;
                    }
                    value += (chunkManager.GetTileFromWorldInt(cXY, i-1, j-1) == tileType) ? 1 : 0; //top left
                    value += (chunkManager.GetTileFromWorldInt(cXY, i, j - 1) == tileType) ? 2 : 0; //top center
                    value += (chunkManager.GetTileFromWorldInt(cXY, i + 1, j - 1) == tileType) ? 4 : 0; //top right
                    value += (chunkManager.GetTileFromWorldInt(cXY, i - 1, j) == tileType) ? 8 : 0; //left
                    value += (chunkManager.GetTileFromWorldInt(cXY, i + 1, j) == tileType) ? 16 : 0; //right
                    value += (chunkManager.GetTileFromWorldInt(cXY, i - 1, j + 1) == tileType) ? 32 : 0; //bottom left
                    value += (chunkManager.GetTileFromWorldInt(cXY, i , j + 1) == tileType) ? 64 : 0; //bottom center
                    value += (chunkManager.GetTileFromWorldInt(cXY, i + 1, j + 1) == tileType) ? 128 : 0; //bottom right

                    //Virtual mapping of surrounding tiles
                    // 1 2 3
                    // 4 0 5
                    // 6 7 8
                    //bitflag values of surrounding tiles
                    // 1  2  4
                    // 8  0  16
                    // 32 64 128


                    if (value == 0)
                    {
                        return; //no surrounding tiles of tiletype
                    }
                    if ((value & 24 + value & 2) == 2)
                    {
                        //top fade
                    }
                    if ((value & 24 + value & 64) == 64)
                    {
                        //bottom fade
                    }
                    if ((value & 66 + value & 8) == 8)
                    {
                        //left fade
                    }
                    if ((value & 66 + value & 16) == 16)
                    {
                        //right fade
                    }
                    if ((value & 10) == 10)
                    {
                        //inside corner topleft
                    }
                    else if ((value & 1 + value & 10) == 1)
                    {
                        //outside corner topleft
                    }
                    if ((value & 18) == 18)
                    {
                        //inside corner topright
                    }
                    else if ((value & 4 + value & 18) == 4)
                    {
                        //outside corner topright
                    }
                    if ((value & 72) == 72)
                    {
                        //inside corner bottomleft
                    }
                    else if ((value & 32 + value & 72) == 32)
                    {
                        //outside corner bottomleft
                    }
                    if ((value & 80) == 80)
                    {
                        //inside corner bottomright
                    }
                    else if ((value & 128 + value & 80) == 128)
                    {
                        //outside corner bottomright
                    }
                }
            }
        }
    }
}