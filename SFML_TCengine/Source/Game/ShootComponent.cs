using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using TCEngine;

namespace TCGame
{
    public class ShootComponent : BaseComponent
    {
        float _CurrentTime;
        float _CoolDown;



        public ShootComponent(float cd)
        {
            _CurrentTime = 0;
            _CoolDown = cd;
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

        }
    }
}