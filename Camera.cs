﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace EngineeringCorpsCS
{
    class Camera : IInputSubscriber
    {
        public Entity focusedEntity { get; set; }
        View gameView;
        View guiView;
        float viewScale;
        public Camera()
        {
            gameView = new View(new FloatRect(0, 0, 2 * 1280, 2 * 720));
            guiView = new View(new Vector2f(640, 360), new Vector2f(1280, 720));
            viewScale = 1.0f;
        }
        public void Update()
        {
            if (focusedEntity != null)
            {
                gameView.Center = new Vector2f(focusedEntity.position.x, focusedEntity.position.y);
            }
        }
        public View GetGameView()
        {
            return gameView;
        }

        public View GetGUIView()
        {
            return guiView;
        }

        public void SubscribeToInput(InputManager input)
        {
            input.ChangeCamera(this);
        }
        public void UnsubscribeToInput(InputManager input)
        {
            input.RemoveCamera(this);
        }
        public void HandleInput(InputManager input)
        {
            //Handle zooming
            if (input.mouseScrollDelta != 0)
            {
                viewScale -= (input.mouseScrollDelta/InputBindings.scrollSensitivity);
                if (viewScale < 0.5)
                {
                    viewScale = 0.5f;
                }
                else if (viewScale > 8)
                {
                    viewScale = 8.0f;
                }

                gameView.Size = new SFML.System.Vector2f(viewScale * 1280, viewScale * 720);
            }
            //Handle clicking on things that are in view here with consideration to focusedentity
        }
    }
}
