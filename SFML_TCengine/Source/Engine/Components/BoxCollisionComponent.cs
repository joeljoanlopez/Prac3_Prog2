using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCEngine
{
    public enum ECollisionLayers
    {
        Player,
        Enemy,
        Person
    }

    public class BoxCollisionComponent: BaseComponent
    {
        private ECollisionLayers m_Layer;

        public ECollisionLayers Layer
        {
            get => m_Layer;
        }

        FloatRect m_bounds;
        public BoxCollisionComponent(FloatRect i_bounds, ECollisionLayers _layer)
            : base()
        {
            m_bounds = i_bounds;
            m_Layer = _layer;
        }

        public BoxCollisionComponent(float left, float top, float width, float height, ECollisionLayers _layer)
            : base()
        {
            m_bounds = new FloatRect(left, top, width, height);
            m_Layer = _layer;
        }

        public override void DebugDraw()
        {
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            if (transformComponent != null)
            {
                FloatRect bounds = GetWorldBounds();
                TecnoCampusEngine.Get.DebugManager.Box(bounds, Color.Cyan);
            }
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.PreUpdate;
        }

        
        public FloatRect GetGlobalBounds()
        {
            return m_bounds;
        }

        public FloatRect GetWorldBounds()
        {
            return Owner.GetWorldTransform().TransformRect(m_bounds);
        }


        public override object Clone()
        {
            return new BoxCollisionComponent(m_bounds, m_Layer);
        }
    }
}
