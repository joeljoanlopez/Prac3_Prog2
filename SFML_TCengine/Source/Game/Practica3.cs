using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    class Practica3 : Game
    {
        static float MC_SCALE = 2.5f;

        public void Init(RenderWindow i_window)
        {
            CreateBackground();
            CreateMainCharacter();
            CreateObjectSpawner();
            CreateHUD();
        }

        public void DeInit()
        {
        }

        public void Update(float _dt)
        {
        }

        private void CreateBackground()
        {
            Actor _Background = new Actor("Background");
            SpriteComponent _SpriteComponent = _Background.AddComponent<SpriteComponent>("Data/Textures/Fondo.jpg");
            _SpriteComponent.Sprite.Scale = new Vector2f(1.5f, 1.5f);
            TecnoCampusEngine.Get.Scene.AddActor(_Background);
        }

        private void CreateMainCharacter()
        {
            // Main Character creation
            Actor actor = new Actor("Flamenco Rabbit");

            AnimatedSpriteComponent _IdleAnimation = actor.AddComponent<AnimatedSpriteComponent>("Data/Textures/ProtaIdle.png", 3, 1);
            _IdleAnimation.sprite.Scale = _IdleAnimation.sprite.Scale * MC_SCALE;
            BoxCollisionComponent _BoxColComponent = actor.AddComponent<BoxCollisionComponent>(_IdleAnimation.GetGlobalBounds(), ECollisionLayers.Player);
            _BoxColComponent.DebugDraw();

            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(TecnoCampusEngine.WINDOW_WIDTH, TecnoCampusEngine.WINDOW_HEIGHT) / 2;

            PlayerMovementController _PlayerMovementController = actor.AddComponent<PlayerMovementController>();

            TecnoCampusEngine.Get.Scene.AddActor(actor);
        }

        private void CreateObjectSpawner()
        {

            // Create a spawner
            Actor actor = new Actor("Spawner");
            ActorSpawnerComponent<ActorPrefab> spawner = actor.AddComponent(new ActorSpawnerComponent<ActorPrefab>());
            spawner.m_MaxPosition = TecnoCampusEngine.Get.ViewportSize;
            spawner.m_MinPosition = new Vector2f(0.0f, 0.0f);
            spawner.m_MaxTime = 6.0f;
            spawner.m_MinTime = 3.0f;

            List<ECollisionLayers> enemyLayers = new List<ECollisionLayers>();
            enemyLayers.Add(ECollisionLayers.Person);


            // Create Enemies
            // TODO Add Necessary components
            ActorPrefab enemy1 = new ActorPrefab("enemy1");
            AnimatedSpriteComponent _AnimatedComponent1 = enemy1.AddComponent<AnimatedSpriteComponent>("Data/Textures/Topo1.png", 22, 4); 
            _AnimatedComponent1.frameTime = 0.001f;
            TransformComponent _TransformComponent1 = enemy1.AddComponent<TransformComponent>();
            
            ActorPrefab enemy2 = new ActorPrefab("enemy2");
            AnimatedSpriteComponent _AnimatedComponent2 = enemy2.AddComponent<AnimatedSpriteComponent>("Data/Textures/Topo2.png", 18, 5); 
            _AnimatedComponent2.frameTime = 0.001f;
            TransformComponent _TransformComponent2 = enemy2.AddComponent<TransformComponent>();
            



            spawner.AddActorPrefab(enemy1);
            spawner.AddActorPrefab(enemy2);
            // Add the actor to the scene
            TecnoCampusEngine.Get.Scene.AddActor(actor);
        }

        private void CreateHUD()
        {
            Actor actor = new Actor("HUD Actor");

            // Add the transform component and set its position correctly
            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(900.0f, 50.0f);
            actor.AddComponent<HUDComponent>("Puntos");

            // Something is missing here!!!

            TecnoCampusEngine.Get.Scene.AddActor(actor);


            //////////////////////////////////////
        }

    }
}
