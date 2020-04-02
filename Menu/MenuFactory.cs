﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace EngineeringCorpsCS
{
    class MenuFactory
    {
        MenuContainer menuContainer;
        Renderer renderer;
        Program program;
        Font debugFont = new Font("SairaRegular.ttf");
        public MenuFactory(MenuContainer menuContainer, Renderer renderer, Program program)
        {
            this.menuContainer = menuContainer;
            this.renderer = renderer;
            this.program = program;
        }

        public void CreateMainMenu(Program program, Camera camera)
        {
            MenuPanel mainMenu = new MenuPanel(new Vector2f(0, 0), new Vector2f(200, 200));
            MenuButton startGameButton = new MenuButton(new Vector2f(25, 25), new Vector2f(150, 50), program.SwitchToIngame);
            MenuText startGameText = new MenuText(new Vector2f(0, 0), new Vector2f(150,50), debugFont, "Start Game", 24, 0.6f); 
            mainMenu.AttachComponent(startGameButton);
            startGameButton.AttachComponent(startGameText);
            MenuText endGameText = new MenuText(new Vector2f(0, 0), new Vector2f(150, 50), debugFont, "End Game", 24, 0.6f);
            MenuButton endGameButton = new MenuButton(new Vector2f(25, 75), new Vector2f(150, 50), program.SwitchToMainMenu);
            mainMenu.AttachComponent(endGameButton);
            endGameButton.AttachComponent(endGameText);
            MenuText quitGameText = new MenuText(new Vector2f(0, 0), new Vector2f(150, 50), debugFont, "Quit Game", 24, 0.6f);
            MenuButton quitGameButton = new MenuButton(new Vector2f(25, 125), new Vector2f(150, 50), program.ExitGame);
            mainMenu.AttachComponent(quitGameButton);
            quitGameButton.AttachComponent(quitGameText);

            //Component adjustment
            mainMenu.closePanelKey = InputBindings.showPauseMenu;
            mainMenu.pivot1 = "center";
            mainMenu.pivot2 = "center";
            mainMenu.SetInitialPosition(camera.GetGUIView());

            startGameText.SetInitialPosition();
            endGameText.SetInitialPosition();
            quitGameText.SetInitialPosition();

            menuContainer.AttachMenu(mainMenu);
        }

        public void CreatePauseMenu(Program program, Camera camera)
        {
            MenuPanel mainMenu = new MenuPanel(new Vector2f(0, 0), new Vector2f(300, 150));
            MenuButton startGameButton = new MenuButton(new Vector2f(25, 25), new Vector2f(100, 100), program.SwitchToIngame);
            MenuText startGameText = new MenuText(new Vector2f(0, 0), new Vector2f(100, 100), debugFont, "Start Game", 24, 0.6f);
            mainMenu.AttachComponent(startGameButton);
            startGameButton.AttachComponent(startGameText);
            MenuText endGameText = new MenuText(new Vector2f(0, 0), new Vector2f(100, 100), debugFont, "End Game", 24, 0.6f);
            MenuButton endGameButton = new MenuButton(new Vector2f(125, 25), new Vector2f(100, 100), program.SwitchToMainMenu);
            mainMenu.AttachComponent(endGameButton);
            endGameButton.AttachComponent(endGameText);
            MenuText quitGameText = new MenuText(new Vector2f(0, 0), new Vector2f(100, 100), debugFont, "Quit Game", 24, 0.6f);
            MenuButton quitGameButton = new MenuButton(new Vector2f(225, 25), new Vector2f(100, 100), program.ExitGame);
            mainMenu.AttachComponent(quitGameButton);
            quitGameButton.AttachComponent(quitGameText);
            mainMenu.closePanelKey = InputBindings.showPauseMenu;
            mainMenu.ClosePanelAction = program.SwitchToIngame;
            program.SwitchToPauseGame();
            mainMenu.pivot1 = "center";
            mainMenu.pivot2 = "center";
            mainMenu.SetInitialPosition(camera.GetGUIView());
            menuContainer.AttachMenu(mainMenu);
        }

        public void CreateDebugMenu()
        {
            MenuPanel debugMenu = new MenuPanel(new Vector2f(720, 0), new Vector2f(300, 150));
            MenuButton boundingBoxButton = new MenuButton(new Vector2f(25, 25), new Vector2f(100, 100), renderer.ToggleBoundingBoxRendering);
            MenuText boundingBoxButtonText = new MenuText(new Vector2f(0, 0), new Vector2f(100,100), debugFont, "Show/Hide boundinggggggggggggggggg boxes", 24, 0.6f);

            MenuDynamicText fps = new MenuDynamicText(new Vector2f(0, 0), debugFont, "Fps: {0}", 24, new MenuDynamicText.DynamicString[] { program.GetFPS });

            debugMenu.AttachComponent(boundingBoxButton);
            debugMenu.AttachComponent(fps);
            boundingBoxButton.AttachComponent(boundingBoxButtonText);
            debugMenu.closePanelKey = InputBindings.showDebugMenu;
            menuContainer.AttachMenu(debugMenu);
        }

        public void CreateMinimap(Camera camera)
        {
            MenuPanel minimapPanel = new MenuPanel(new Vector2f(0, 0), new Vector2f(300, 300));
            MenuWorldMap minimap = new MenuWorldMap(camera, renderer, new Vector2f(25, 25), new Vector2f(250, 250));
            MenuButton minimapPollutionToggle = new MenuButton(new Vector2f(25, 275), new Vector2f(50, 50), minimap.TogglePollution);
            minimapPanel.AttachComponent(minimap);
            minimapPanel.AttachComponent(minimapPollutionToggle);
            minimapPollutionToggle.SetInitialPosition(camera.GetGUIView());
            minimapPanel.closePanelKey = InputBindings.showMinimap;
            minimapPanel.pivot1 = "top";
            minimapPanel.pivot2 = "right";
            minimapPanel.SetInitialPosition(camera.GetGUIView());
            minimapPanel.lockedPosition = true;
            menuContainer.AttachMenu(minimapPanel);
        }

        public void CreateWorldMap(Camera camera)
        {
            MenuPanel minimapPanel = new MenuPanel(new Vector2f(0, 0), camera.GetGUIView().Size);
            MenuWorldMap minimap = new MenuWorldMap(camera, renderer, new Vector2f(25, 25), camera.GetGUIView().Size - new Vector2f(50,50));
            MenuButton minimapPollutionToggle = new MenuButton(new Vector2f(25, 275), new Vector2f(50, 50), minimap.TogglePollution);
            minimapPanel.AttachComponent(minimap);
            minimapPanel.AttachComponent(minimapPollutionToggle);
            minimap.controllable = true;
            minimapPollutionToggle.pivot1 = "bottom";
            minimapPollutionToggle.SetInitialPosition(camera.GetGUIView());
            minimapPanel.closePanelKey = InputBindings.showWorldMap;
            minimapPanel.pivot1 = "top";
            minimapPanel.pivot2 = "left";
            minimapPanel.SetInitialPosition(camera.GetGUIView());
            minimapPanel.lockedPosition = true;
            renderer.ToggleCullingMinimap();
            minimapPanel.ClosePanelAction = renderer.ToggleCullingMinimap;
            menuContainer.AttachMenu(minimapPanel);
        }


        public void CreateWorldMenu(Camera camera, SurfaceGenerator surfaceGenerator)
        {
            MenuPanel worldMenu = new MenuPanel(new Vector2f(0, 0), new Vector2f(500,500));
            string[] noiseNames = Enum.GetNames(typeof(FastNoise.NoiseType));
            int[] noiseValues = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            MenuListBox moistureNoiseType = new MenuListBox(new Vector2f(0,0), new Vector2f(150,25), noiseNames, noiseValues, surfaceGenerator.SetNoiseType, debugFont, 24, 24, 0);
            moistureNoiseType.additionalParam = "moisture";
            MenuListBox elevationNoiseType = new MenuListBox(new Vector2f(100, 0), new Vector2f(150, 25), noiseNames, noiseValues, surfaceGenerator.SetNoiseType, debugFont, 24, 24, 0);
            elevationNoiseType.additionalParam = "elevation";
            MenuListBox temperatureNoiseType = new MenuListBox(new Vector2f(200, 0), new Vector2f(150, 25), noiseNames, noiseValues, surfaceGenerator.SetNoiseType, debugFont, 24, 24, 0);
            temperatureNoiseType.additionalParam = "temperature";

            worldMenu.SetInitialPosition(camera.GetGUIView());

            worldMenu.AttachComponent(moistureNoiseType);
            worldMenu.AttachComponent(elevationNoiseType);
            worldMenu.AttachComponent(temperatureNoiseType);
            menuContainer.AttachMenu(worldMenu);
        }

        public void CreateTestField(SurfaceGenerator surfaceGenerator)
        {
            MenuField menuField = new MenuField(debugFont, surfaceGenerator.ParseString);
            menuField.tag = "surfacesize";
            menuContainer.AttachMenu(menuField);
        }
    }
}
