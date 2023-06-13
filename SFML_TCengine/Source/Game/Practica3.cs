using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    class Practica3 : Game
    {
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

        private void CreateBackground(){
            Actor _Background = new Actor("Background");
            SpriteComponent _SpriteComponent = _Background.AddComponent<SpriteComponent>("Data/Textures/Fondo.jpg");
            _SpriteComponent.Sprite.Scale = new Vector2f(1.5f, 1.5f);
            TecnoCampusEngine.Get.Scene.AddActor(_Background);
        }

        private void CreateMainCharacter()
        {
            // Main Character creation
            Actor actor = new Actor("Flamenco Rabbit");

            SpriteComponent _SpriteComponent = actor.AddComponent<SpriteComponent>("Data/Textures/Conejo flamenco.png");
            _SpriteComponent.Sprite.Scale = new Vector2f(0.25f, 0.25f);
            BoxCollisionComponent _BoxColComponent = actor.AddComponent<BoxCollisionComponent>(_SpriteComponent.GetGlobalbounds(), ECollisionLayers.Player);

            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(TecnoCampusEngine.WINDOW_WIDTH, TecnoCampusEngine.WINDOW_HEIGHT)/2;

            PlayerMovementController _PlayerMovementController = actor.AddComponent<PlayerMovementController>();

            TecnoCampusEngine.Get.Scene.AddActor(actor);
        }

        private void CreateObjectSpawner()
        {

            // Create a spawner
            Actor actor = new Actor("Spawner");
            ActorSpawnerComponent<ActorPrefab>  spawner = actor.AddComponent(new ActorSpawnerComponent<ActorPrefab>());
            spawner.m_MaxPosition = TecnoCampusEngine.Get.ViewportSize;
            spawner.m_MinPosition = new Vector2f(0.0f, 0.0f);
            spawner.m_MaxTime = 3.0f;
            spawner.m_MinTime = 0.2f;
            ActorPrefab enemy = new ActorPrefab("enemy");

            
            CircleShape shape = new CircleShape(20);
            shape.OutlineColor = Color.Magenta;
            shape.FillColor = Color.Transparent;
            shape.OutlineThickness = 2.0f;
            
            enemy.AddComponent<ShapeComponent>(shape);
            enemy.AddComponent<TransformComponent>();
            

            spawner.AddActorPrefab(enemy);
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
