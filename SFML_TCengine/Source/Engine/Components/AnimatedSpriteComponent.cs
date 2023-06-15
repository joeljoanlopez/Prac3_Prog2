using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TCEngine
{
    public class AnimatedSpriteComponent : RenderComponent
    {
        private AnimatedSprite m_Sprite;


        public bool loop
        {
            get => m_Sprite.loop;
            set => m_Sprite.loop = value;
        }

        public float frameTime
        {
            get => m_Sprite.frameTime;
            set => m_Sprite.frameTime = value;
        }


        public float animationTime
        {
            get => m_Sprite.animationTime;
        }

        public AnimatedSprite sprite
        {
            get
            {
                return m_Sprite;
            }
            set { m_Sprite = value; }
        }

        public AnimatedSpriteComponent(string _textureName, uint _numColumns, uint _numRows)
        {
            m_Sprite = new AnimatedSprite(new Texture(_textureName), _numRows, _numColumns, 0.2f);


            Initialize();
        }

        public AnimatedSpriteComponent(Texture _texture, uint _numColumns, uint _numRows)
        {
            m_Sprite = new AnimatedSprite(_texture, _numRows, _numColumns, 0.2f);

            Initialize();
        }

        public AnimatedSpriteComponent(string _textureName, int _numColumns, int _numRows)
        {
            m_Sprite = new AnimatedSprite(new Texture(_textureName), (uint)_numRows, (uint)_numColumns, 0.2f);


            Initialize();
        }

        public AnimatedSpriteComponent(Texture _texture, int _numColumns, int _numRows)
        {
            m_Sprite = new AnimatedSprite(_texture, (uint)_numRows, (uint)_numColumns, 0.2f);

            Initialize();
        }

        private void Initialize()
        {
            m_Sprite.loop = true;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
            m_Sprite.Update(_dt);
        }

        public override void DebugDraw()
        {
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            if (transformComponent != null)
            {
                FloatRect currentFrameRect = m_Sprite.GetGlobalBounds();
                //TecnoCampusEngine.Get.DebugManager.Box(transformComponent.Transform.Transform.TransformRect(currentFrameRect), Color.Magenta);
            }
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            base.Draw(_target, _states);
            Debug.Assert(m_Sprite != null);
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();

            _states.Transform *= Owner.GetWorldTransform();

            m_Sprite.Draw(_target, _states);
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();
        }

        public void Center()
        {
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null, "Trying to change the origin of the actor " + Owner.Name + " without a TransformComponent");
            FloatRect localBounds = m_Sprite.currentFrameRect;
            m_Sprite.Origin = new Vector2f(localBounds.Width * 0.5f, localBounds.Height * 0.5f);
        }

        public override object Clone()
        {
            AnimatedSpriteComponent clonedComponent = new AnimatedSpriteComponent(m_Sprite.Texture, m_Sprite.numColumns, m_Sprite.numRows);
            clonedComponent.m_RenderLayer = m_RenderLayer;

            return clonedComponent;
        }

        public FloatRect GetGlobalBounds()
        {
            return m_Sprite.GetGlobalBounds();
        }
    }
}
