﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace EngineeringCorpsCS
{
    class MenuInventory : MenuComponent
    {
        //need functionality for holding items
        //store held item with camera
        //versus
        //store held item with player <- makes most sense as then placement mechanics would be tied to player
        //edge cases would be what if player is in vehicle?
        //so this type of inventory would require a player then?
        //what about an entity?
        //what if instead we just attach the camera to every inventory, and if the item is clicked then it can be transferred
        //to the focused entity?
        //downside -- ties camera to game functionality

        //Idea: pass in accessing player
        //fixes tying camera to game functionality.
        //makes sense, as for an inventory to appear, a player must access it
        //clicking on the inventory slot transfers the item to the player's held item
        //can create an Ingame entity to represent the player's held item, which can switch to being drawn on the menu layer
        //versus the world with its attached drawable
        //But if the player has a held item, it needs to be able to be passed back to the inventory

        ItemStack[] inventory;
        ItemStack heldItem; //TODO: move to player
        VertexArray itemFrames;
        public MenuInventory(Vector2f componentSize, ItemStack[] inventory)//, Player accessingPlayer, TextureContainer textureContainer)
        {
            this.inventory = inventory;
            this.size = componentSize;
            //Need textureContainer to draw item icons
            //Need
            for(int i = 0; i < inventory.Length; i++)
            {
                //Construct inventory frames
            }
        }

        public override void Draw(RenderTexture gui, Vector2f origin, RenderStates guiState)
        {
            base.Draw(gui, origin, guiState);
            RectangleShape frame = new RectangleShape(new Vector2f(32, 32));
            frame.FillColor = Color.Red;
            RectangleShape item = new RectangleShape(new Vector2f(16, 16));
            item.FillColor = Color.Yellow;
            for(int i = 0; i < inventory.Length; i++)
            {
                frame.Position = origin + position + new Vector2f((i * 32) % (int)size.X, ((i * 32) / (int)size.X) * 32);
                gui.Draw(frame);
                if (inventory[i] != null)
                {
                    item.Position = origin + position + new Vector2f((i * 32) % (int)size.X, (i * 32) / (int)size.X);
                    gui.Draw(item);
                }
            }
        }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);
        }
    }
}
