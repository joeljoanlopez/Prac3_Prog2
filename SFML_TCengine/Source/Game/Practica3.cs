using System.Diagnostics;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    internal class Practica3 : Game
    {
        private static float MC_SCALE = 2.5f;
        private static float ENEMY_SCALE = 2.0f;

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
            transformComponent.Transform.Position = TecnoCampusEngine.Get.ViewportSize / 2;
            AnimatedSpriteComponent animatedSpriteComponent = actor.AddComponent<AnimatedSpriteComponent>("Data/Textures/ProtaIdleV2.png", 4u, 1u);
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
            _cannonComponent.BulletTextureName = "Data/Textures/Bullet.png";
            _cannonComponent.FireRate = 1.5f;



            TecnoCampusEngine.Get.Scene.AddActor(actor);
        }

        private void CreateObjectSpawner()
        {
            // EJEMPLO 8

            // Create a spawner
            Actor actor = new Actor("Spawner");
            ActorSpawnerComponent<ActorPrefab> spawner = actor.AddComponent<ActorSpawnerComponent<ActorPrefab>>();

            spawner.m_MaxPosition = TecnoCampusEngine.Get.ViewportSize - new Vector2f(ENEMY_SCALE, ENEMY_SCALE);
            spawner.m_MinPosition = new Vector2f(ENEMY_SCALE, ENEMY_SCALE);
            spawner.m_MaxTime = 6.0f;
            spawner.m_MinTime = 3.0f;
            spawner.Reset();

            List<ECollisionLayers> enemyLayers = new List<ECollisionLayers>();
            enemyLayers.Add(ECollisionLayers.Person);


            // EJEMPLOS 1, 2 y 10

            // Create Enemies
            int enemyNumber = 2;
            for (int i = 1; i <= enemyNumber; i++){
                ActorPrefab enemy = new ActorPrefab("Enemy" + i);
                TransformComponent _TransformComponent = enemy.AddComponent<TransformComponent>();
                AnimatedSpriteComponent _AnimatedSpriteComponent = enemy.AddComponent<AnimatedSpriteComponent>("Data/Textures/Topo" + i + ".png", 22u, 4u);
                _AnimatedSpriteComponent.loop = false;
                BoxCollisionComponent _BoxColComponent = enemy.AddComponent<BoxCollisionComponent>(_AnimatedSpriteComponent.GetGlobalBounds(), ECollisionLayers.Enemy);
                TimerComponent timerComponent = enemy.AddComponent<TimerComponent>(_AnimatedSpriteComponent.animationTime);
                timerComponent.DieOnTime = true;
                spawner.AddActorPrefab(enemy);
            }
            
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