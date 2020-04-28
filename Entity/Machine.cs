﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace EngineeringCorpsCS
{
    class Machine:EntityPhysical
    {
        public enum MachineState
        {
            Idle,
            Working
        }

        MachineState machineState;
        Animation working; //This animation plays when working and stops when idle
        Recipe activeRecipe;
        int recipeProgress = 0;
        float workingSpeed = 1;
        int bufferAmount = 10;
        public ItemStack[] result { get; set; }
        public ItemStack[] input { get; set; }
        public Machine(string name, Animation working)
        {
            this.name = name;
            this.working = working;

            activeRecipe = new Recipe(new int[] { 1 }, new string[] { "Pine Sapling" }, new int[] { 1 }, new string[] { "Wood" }, 60);
            drawArray = new Drawable[] { working };
            result = new ItemStack[activeRecipe.itemsResults.Length];
            input = new ItemStack[activeRecipe.itemsRequired.Length];
            machineState = MachineState.Idle;
        }

        public override void Update(EntityCollection entityCollection, ItemCollection itemCollection)
        {
            if (machineState == MachineState.Idle)
            {
                //Check that machine has recipe
                if (activeRecipe != null)
                {
                    //Check that machine is not full
                    bool full = false;
                    for (int j = 0; j < activeRecipe.itemsResults.Length; j++)
                    {
                        if (result[j] != null && result[j].count >= bufferAmount)
                        {
                            full = true;
                        }
                    }
                    if (full == false)
                    {
                        //Check if input is valid
                        bool valid = true;
                        for (int i = 0; i < activeRecipe.itemsRequired.Length; i++)
                        {
                            if (input[i] == null || input[i].item.name != activeRecipe.itemsRequired[i] || input[i].count < activeRecipe.counts[i])
                            {
                                valid = false;
                            }
                        }
                        if (valid == true)
                        {
                            //Switch state to working and consume inputs
                            machineState = MachineState.Working;
                            for (int i = 0; i < activeRecipe.itemsRequired.Length; i++)
                            {
                                input[i] = input[i].Subtract(activeRecipe.counts[i]);
                            }
                        }
                    }
                }
            }
            if (machineState == MachineState.Working)
            {
                if (recipeProgress >= activeRecipe.recipeTime)
                {
                    //Output products
                    for(int i = 0; i < activeRecipe.itemsResults.Length; i++)
                    {
                        if(result[i] == null)
                        {
                            result[i] = new ItemStack(itemCollection.GetItem(activeRecipe.itemsResults[i]), 0);
                        }
                        result[i].Add(activeRecipe.countsResult[i]);
                    }
                    recipeProgress = 0;
                    machineState = MachineState.Idle;
                }
                else
                {
                    recipeProgress += (int)Math.Ceiling(workingSpeed);
                }
            }
        }
        public override Entity Clone()
        {
            Machine clone = new Machine(this.name, this.working.Clone());
            clone.drawingBox = new BoundingBox(this.drawingBox);
            clone.collisionBox = new BoundingBox(this.collisionBox);
            clone.selectionBox = new BoundingBox(this.selectionBox);
            clone.collisionMask = this.collisionMask;
            clone.minable = this.minable;
            clone.miningProps = this.miningProps;
            clone.mapColor = new Color(this.mapColor);
            return clone;
        }

        public override void OnClick(Entity entity, MenuFactory menuFactory)
        {
            //create menu for
            if(entity is Player)
            menuFactory.CreateMachineInterface(this, (Player) entity);
        }

        public float GetProgress(string tag)
        {
            if (activeRecipe != null)
            {
                return recipeProgress * 1.0f / activeRecipe.recipeTime;
            }
            else
            {
                return 0.0f;
            }
        }
    }
}
