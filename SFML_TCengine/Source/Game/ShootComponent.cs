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
        static float BULLET_SPEED = 100f;

        float _CurrentTime;
        float _CoolDown;

        string _BulletTexture;

        List<ECollisionLayers> _ImpactLayers;
        TransformComponent transformComponent;

        Vector2f _Direction;



        public ShootComponent(float coolDown, List<ECollisionLayers> impactLayers, string bulletTexture)
        {
            _CurrentTime = 0;
            _CoolDown = coolDown;
            _ImpactLayers = impactLayers;
            _BulletTexture = bulletTexture;


            TecnoCampusEngine.Get.Window.MouseMoved += OnMouseMoved;
        }

        void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null);
            _Direction = transformComponent.Transform.Position - new Vector2f(e.X, e.Y);
            _Direction.Normal();
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
            Actor bullet = new Actor("Bullet");
            SpriteComponent spriteComponent = bullet.AddComponent<SpriteComponent>(_BulletTexture);
            spriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Front;

            TransformComponent bulletTransform = bullet.AddComponent<TransformComponent>();
            bulletTransform.Transform.Position = transformComponent.Transform.Position + _Direction;
            bulletTransform.Transform.Rotation = MathF.Acos(_Direction.X);

            ForwardMovementComponent bulletForward = bullet.AddComponent<ForwardMovementComponent>(BULLET_SPEED, _Direction);
            BulletComponent bulletComponent = bullet.AddComponent<BulletComponent>(_ImpactLayers);
            OutOfWindowDestructionComponent bulletDestruction = bullet.AddComponent<OutOfWindowDestructionComponent>();

            TecnoCampusEngine.Get.Scene.AddActor(bullet);
        }
    }
}