using System;
using SFML.System;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    // TODO (4): Create the ForwardMovementComponent
    //  - Although you can be creative, it should have at least these two memebers:
    //    - float m_Speed and Vector2f m_Forward
    //  - The main idea of this component is to move the actor in the m_Forward direction and the speed defined
    //    by the m_Speed member

    class ForwardMovementComponent : BaseComponent
    {
        private float m_Speed;
        private Vector2f m_Forward;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }
        public Vector2f Forward
        {
            get { return m_Forward; }
            set { m_Forward = value; }
        }

        public ForwardMovementComponent(float speed, Vector2f fw)
        {
            m_Speed = speed;
            m_Forward = fw;

        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }
        public override void Update(float deltaTime)
        {
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            if (transformComponent != null)
            {
                transformComponent.Transform.Position += m_Forward * m_Speed * deltaTime;
            }
        }
        public override object Clone()
        {
            ForwardMovementComponent clonedComponent = new ForwardMovementComponent(m_Speed, m_Forward);
            return clonedComponent;
        }

    }
}
