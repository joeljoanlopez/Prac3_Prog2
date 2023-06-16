using System.IO;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCEngine;

namespace TCGame
{
    class MovementComponent : BaseComponent
    {
        Vector2f _direction;
        float _vel;
        private float _maxSpeed;
        float _targetSpeed;

        float _increment;

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public MovementComponent()
        {
            _vel = 0;
            _targetSpeed = 0;
            _maxSpeed = 200f;
            _increment = 5;
        }

        public void SetDirection(Vector2f i_direction)
        {
            _direction = i_direction;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
            TransformComponent tranformComponent = this.Owner.GetComponent<TransformComponent>();
            if (tranformComponent != null)
            {
                // EJEMPLO 6
                if (_direction.Size() > 0f)
                {
                    _targetSpeed = _maxSpeed;
                    _increment = Math.Abs(_increment);
                }
                else
                {
                    _targetSpeed = 0;
                    _increment = -Math.Abs(_increment);
                }
                
                _vel = _vel == _targetSpeed ? _targetSpeed : _vel + _increment;
                tranformComponent.Transform.Position += _direction * _vel * _dt;
            }
        }

        public Vector2f GetDirection()
        {
            return _direction;
        }
    }
}
