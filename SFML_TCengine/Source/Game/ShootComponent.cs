using System.Diagnostics;
using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using TCEngine;

namespace TCGame
{
    public class ShootComponent : BaseComponent
    {
        static float BULLET_SPEED = 10.0f;

        float _CurrentTime;
        float _CoolDown;

        string _BulletTexture;

        List<ECollisionLayers> _ImpactLayers;
        TransformComponent transformComponent;

        Vector2f _Direction;
        Vector2f _MousePosition;


        public ShootComponent(float coolDown, List<ECollisionLayers> impactLayers, string bulletTexture)
        {
            _CurrentTime = 0;
            _CoolDown = coolDown;
            _ImpactLayers = impactLayers;
            _BulletTexture = bulletTexture;
            _MousePosition = new Vector2f();

            TecnoCampusEngine.Get.Window.MouseMoved += OnMouseMoved;
        }

        void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            _MousePosition = new Vector2f(e.X, e.Y);
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
            _CurrentTime += _dt;
            if (_CurrentTime >= _CoolDown)
            {
                _CurrentTime = 0;
                Shoot();
            }
        }

        private void Shoot()
        {
            transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null);
            _Direction = _MousePosition - transformComponent.Transform.Position;
            _Direction = Normalize(_Direction);
            
            Actor bullet = new Actor("Bullet");
            
            SpriteComponent spriteComponent = bullet.AddComponent<SpriteComponent>(_BulletTexture);
            spriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Front;
            spriteComponent.Sprite.Scale *= 0.1f;
            spriteComponent.Sprite.Origin = new Vector2f(spriteComponent.Sprite.GetLocalBounds().Width / 2.0f, spriteComponent.Sprite.GetLocalBounds().Height / 2.0f);

            TransformComponent bulletTransform = bullet.AddComponent<TransformComponent>();
            bulletTransform.Transform.Position = transformComponent.Transform.Position + _Direction * 50.0f;

            ForwardMovementComponent bulletForward = bullet.AddComponent<ForwardMovementComponent>(BULLET_SPEED, _Direction);
            BulletComponent bulletComponent = bullet.AddComponent<BulletComponent>(_ImpactLayers);
            OutOfWindowDestructionComponent bulletDestruction = bullet.AddComponent<OutOfWindowDestructionComponent>();

            TecnoCampusEngine.Get.Scene.AddActor(bullet);
        }

        private Vector2f Normalize(Vector2f vec)
        {
            float _module = (float)Math.Sqrt(Math.Pow(vec.X, 2) + Math.Pow(vec.Y, 2));
            return _module != 0 ? vec / _module : vec;
        }

        float GetAngleFromPoints(Vector2f a, Vector2f b)
        {
            return MathF.Atan2(a.Y - b.Y, a.X - b.X);
        }
    }
}