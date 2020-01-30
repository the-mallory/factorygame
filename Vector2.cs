﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringCorpsCS
{
    class Vector2
    {
        public float x
        {
            get { return x; }
            set { this.x = value; }
        }
        public float y
        {
            get { return y; }
            set { this.y = value; }
        }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(int[] xy)
        {
            this.x = xy[0];
            this.y = xy[1];
        }

        /// <summary>
        /// Add a Vector2 to the calling Vector2
        /// </summary>
        /// <param name="other"></param>
        public void Add(Vector2 other)
        {
            this.x += other.x;
            this.y += other.y;
        }
        
        /// <summary>
        /// Adds desired x and y components to a Vector2
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Add(float x, float y)
        {
            this.x += x;
            this.y += y;
        }

        /// <summary>
        /// Permanently rotates a Vector2 by a number of degrees around the origin
        /// </summary>
        /// <param name="rotation"></param>
        public void VRotate(float rotation)
        {
            float tempX = this.x;
            float tempY = this.y;
            this.x = (float) ( tempX * Math.Cos(rotation) - tempY * Math.Sin(rotation) );
            this.y = (float) ( tempX * Math.Sin(rotation) + tempY * Math.Cos(rotation) );
        }

        /// <summary>
        /// Returns a new Vector2 representing a rotated version of the old Vector2
        /// </summary>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public Vector2 Rotate(float rotation)
        {
            float tempX = this.x;
            float tempY = this.y;
            return new Vector2((float)(tempX * Math.Cos(rotation) - tempY * Math.Sin(rotation)), (float)(tempX * Math.Sin(rotation) + tempY * Math.Cos(rotation)));
        }

        /// <summary>
        /// Returns the magnitude of the Vector2
        /// </summary>
        /// <returns></returns>
        public float GetMagnitude()
        {
            return (float) Math.Sqrt((this.x * this.x) + (this.y * this.y));
        }

        public void VNormalize()
        {
            float mag = this.GetMagnitude();
            this.x = this.x / mag;
            this.y = this.y / mag;
        }

        /// <summary>
        /// Calculates the angle between the x axis and the Vector2
        /// </summary>
        /// <returns></returns>
        public float GetRotation()
        {
            float angle = (float) Math.Acos(this.x / this.GetMagnitude());
            if(this.y < 0)
            {
                return -angle;
            }
            else
            {
                return angle;
            }
        }

    }
}
