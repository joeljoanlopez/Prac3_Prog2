using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    class Practica3 : Game
    {
        public void Init(RenderWindow i_window)
        {
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

        private void CreateMainCharacter()
        {
            Actor actor = new Actor("Following Mouse Actor");

            // Create an arrow shape using a ConvexShape
            ConvexShape shape = new ConvexShape(4);
            shape.SetPoint(0, new Vector2f(20.0f, 0.0f));
            shape.SetPoint(1, new Vector2f(40.0f, 40.0f));
            shape.SetPoint(2, new Vector2f(20.0f, 20.0f));
            shape.SetPoint(3, new Vector2f(0.0f, 40.0f));
            shape.FillColor = Color.Transparent;
            shape.OutlineColor = Color.Green;
            shape.OutlineThickness = 2.0f;
            actor.AddComponent<ShapeComponent>(shape);

            // Add the transform component and set its position correctly
            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(600.0f, 200.0f);

            
            
           

            //////////////////////////////////////////////////

            // Add the actor to the scene
            TecnoCampusEngine.Get.Scene.AddActor(actor);
        }

        private void CreateObjectSpawner()
        {
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
