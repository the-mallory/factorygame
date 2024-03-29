﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace EngineeringCorpsCS
{
    class Tree : EntityPhysical
    {
        public enum TreeState
        {
            Chopped,    //If a tree exists in this state, it is just so it can leave a tree stump
            Dead,       //A tree in this state has no leaves
            Dying,      //A tree in this states only has some leaves
            Affected,   //A tree in this state has a moderate number of leaves
            Healthy     //A tree in this state has numerous leaves
        }
        StaticSprite trunk; //contains full tree trunk or stump
        Animation leaves; //A leaf animation that will change the leaf density based on the state of the animation
        Animation shadow; //A shadow animation containing the various shadows of the tree
        TreeState treeState;


        public Tree(string name, StaticSprite trunk, Animation leaves, Animation shadow)
        {
            this.name = name;
            this.trunk = trunk;
            this.leaves = leaves;
            this.shadow = shadow;
            leaves.currentFrame = 3;
            shadow.currentFrame = 3;
            drawArray = new Drawable[] { trunk, leaves, shadow };

        }

        public override void Update(EntityCollection entityCollection, ItemCollection itemCollection)
        {
            if (surface.GetChunk(centeredChunk, false).pollutionValue > 150)
            {
                treeState = TreeState.Dead;
            }
            else if (surface.GetChunk(centeredChunk, false).pollutionValue > 100)
            {
                treeState = TreeState.Dying;
            }
            else if (surface.GetChunk(centeredChunk, false).pollutionValue > 50)
            {
                treeState = TreeState.Affected;
            }
            
            
            EvaluateTree();
        }

        public void EvaluateTree()
        {
            switch (treeState)
            {
                case (TreeState.Healthy):
                    leaves.currentFrame = 3;
                    shadow.currentFrame = 3;
                    break;
                case (TreeState.Affected):
                    leaves.currentFrame = 2;
                    shadow.currentFrame = 2;
                    break;
                case (TreeState.Dying):
                    leaves.currentFrame = 1;
                    shadow.currentFrame = 1;
                    break;
                case (TreeState.Dead):
                    drawArray = new Drawable[] { trunk, shadow };
                    shadow.currentFrame = 0;
                    break;
                case (TreeState.Chopped):
                    break;
            }
        }

        public override Entity Clone()
        {
            //Things that must be duplicated
            Tree clone = new Tree(this.name, this.trunk.Clone(), this.leaves.Clone(), this.shadow.Clone());
            clone.drawingBox = new BoundingBox(this.drawingBox);
            clone.collisionBox = new BoundingBox(this.collisionBox);
            clone.selectionBox = new BoundingBox(this.selectionBox);
            clone.collisionMask = this.collisionMask;
            clone.minable = this.minable;
            clone.miningProps = this.miningProps;
            clone.mapColor = new Color(this.mapColor);
            clone.miningSounds = (string[])this.miningSounds.Clone();
            //Things that are retained between instances of a prototype

            return clone;
        }
    }
}
