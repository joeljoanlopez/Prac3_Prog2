using System;
using System.Diagnostics;
using SFML.System;
using SFML.Window;
using TCEngine;

namespace TCGame
{
    public class PlayerMovementController : BaseComponent
    {
        private const float MOVEMENT_SPEED = 200f;
        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.PreUpdate;
        }

        public override void Update(float _dt)
        {
            Vector2f movement = new Vector2f();
            AnimatedSpriteComponent m_sprite = Owner.GetComponent<AnimatedSpriteComponent>();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                movement.Y -= 1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                movement.Y += 1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                movement.X -= 1f;
                //m_sprite.sprite.Scale = new Vector2f(-1f,1f);

            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                movement.X += 1f;
                //m_sprite.sprite.Scale = new Vector2f(1f,1f);
            }
            movement = Normalize(movement);
            Vector2f displacement = movement * MOVEMENT_SPEED * _dt;
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null);
            transformComponent.Transform.Position += displacement;

        }

        private Vector2f Normalize(Vector2f vec)
        {
            float _module = (float)Math.Sqrt(Math.Pow(vec.X, 2) + Math.Pow(vec.Y, 2));
            return _module != 0 ? vec / _module : vec;
        }

    }
}