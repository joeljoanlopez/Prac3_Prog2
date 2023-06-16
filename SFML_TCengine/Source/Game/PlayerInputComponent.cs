using System.Numerics;
using System;
using System.Diagnostics;
using SFML.System;
using SFML.Window;
using TCEngine;

namespace TCGame
{
    public class PlayerInputComponent : BaseComponent
    {
        private const float MOVEMENT_SPEED = 50f;
        private const float INITIAL_SPEED = 1f;
        Vector2f movement;
        Vector2f _MousePosition;

        float _CurrentSpeed;

        public PlayerInputComponent()
        {
            movement = new Vector2f();
            _CurrentSpeed = INITIAL_SPEED;
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.PreUpdate;
        }

        public override void Update(float _dt)
        {

            AnimatedSpriteComponent m_sprite = Owner.GetComponent<AnimatedSpriteComponent>();
            movement = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
                movement.Y -= 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
                movement.Y += 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
                movement.X -= 1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
                movement.X += 1f;

            movement = Normalize(movement);

            if (movement.X < 0)
                m_sprite.sprite.Scale = new Vector2f(-Math.Abs(m_sprite.sprite.Scale.X), m_sprite.sprite.Scale.Y);
            else
                m_sprite.sprite.Scale = new Vector2f(Math.Abs(m_sprite.sprite.Scale.X), m_sprite.sprite.Scale.Y);

            MovementComponent movementComponent = Owner.GetComponent<MovementComponent>();
            movementComponent.SetDirection(movement);
        }

        private Vector2f Normalize(Vector2f vec)
        {
            return vec.Size() != 0 ? vec / vec.Size() : vec;
        }

        public bool Moving()
        {
            return movement.Size() > 0.1f;
        }
    }
}