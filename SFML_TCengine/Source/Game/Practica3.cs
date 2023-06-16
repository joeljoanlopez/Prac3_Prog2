using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    internal class Practica3 : Game
    {
        private static float MC_SCALE = 2.5f;
        private static float ENEMY_SCALE = 2.5f;

        static float SHOOTING_COOLDOWN = 2.0f;

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
            Texture _mcTexture = new Texture("Data/Textures/Fondo.jpg");
            SpriteComponent _SpriteComponent = _Background.AddComponent<SpriteComponent>(_mcTexture);
            _SpriteComponent.Sprite.Scale *= 1.5f;
            TecnoCampusEngine.Get.Scene.AddActor(_Background);
        }

        private void CreateMainCharacter()
        {
            // Main Character creation
            Actor actor = new Actor("Flamenco Rabbit");

            // EJEMPLOS 1, 2 y 10
            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(500.0f, 300.0f);
            AnimatedSpriteComponent animatedSpriteComponent = actor.AddComponent<AnimatedSpriteComponent>("Data/Textures/ProtaIdle.png", 3u, 1u);
            animatedSpriteComponent.sprite.Scale *= MC_SCALE;
            animatedSpriteComponent.Center();
            AnimationFSMComponent animationFSMComponent = actor.AddComponent<AnimationFSMComponent>(MC_SCALE);
            BoxCollisionComponent boxCollisionComponent = actor.AddComponent<BoxCollisionComponent>(animatedSpriteComponent.GetGlobalBounds(), ECollisionLayers.Player);


            List<ECollisionLayers> enemyLayers = new List<ECollisionLayers>();
            enemyLayers.Add(ECollisionLayers.Enemy);

            MovementComponent _PlayerMovement = actor.AddComponent<MovementComponent>();
            PlayerInputComponent _PlayerMovementController = actor.AddComponent<PlayerInputComponent>();

            CannonComponent _cannonComponent = actor.AddComponent<CannonComponent>(enemyLayers);
            _cannonComponent.AutomaticFire = true;
            _cannonComponent.BulletTextureName = "Data/Textures/bulletPlaceHolder.png";
            _cannonComponent.FireRate = 1.5f;



            TecnoCampusEngine.Get.Scene.AddActor(actor);
        }

        private void CreateObjectSpawner()
        {
            // Create Enemies
            // EJEMPLO 8

            // Create a spawner
            Actor actor = new Actor("Spawner");
            ActorSpawnerComponent<ActorPrefab> spawner = actor.AddComponent(new ActorSpawnerComponent<ActorPrefab>());
            spawner.m_MaxPosition = TecnoCampusEngine.Get.ViewportSize;
            spawner.m_MinPosition = new Vector2f(0.0f, 0.0f);
            spawner.m_MaxTime = 6.0f;
            spawner.m_MinTime = 3.0f;

            List<ECollisionLayers> enemyLayers = new List<ECollisionLayers>();
            enemyLayers.Add(ECollisionLayers.Person);

            // TODO Add Necessary components
            // EJEMPLOS 1, 2 y 10
            ActorPrefab enemy1 = new ActorPrefab("enemy1");
            AnimatedSpriteComponent _AnimatedComponent1 = enemy1.AddComponent<AnimatedSpriteComponent>("Data/Textures/Topo1.png", 22u, 4u);
            _AnimatedComponent1.frameTime = 0.001f;
            _AnimatedComponent1.sprite.Scale *= ENEMY_SCALE;
            TransformComponent _TransformComponent1 = enemy1.AddComponent<TransformComponent>();
            BoxCollisionComponent _BoxColComponent1 = enemy1.AddComponent<BoxCollisionComponent>(_AnimatedComponent1.GetGlobalBounds(), ECollisionLayers.Enemy);

            ActorPrefab enemy2 = new ActorPrefab("enemy2");
            AnimatedSpriteComponent _AnimatedComponent2 = enemy2.AddComponent<AnimatedSpriteComponent>("Data/Textures/Topo2.png", 18u, 5u);
            _AnimatedComponent2.frameTime = 0.001f;
            _AnimatedComponent2.sprite.Scale *= ENEMY_SCALE;
            TransformComponent _TransformComponent2 = enemy2.AddComponent<TransformComponent>();
            BoxCollisionComponent _BoxColComponent2 = enemy2.AddComponent<BoxCollisionComponent>(_AnimatedComponent2.GetGlobalBounds(), ECollisionLayers.Enemy);

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