using System;
using System.Collections.Generic;
using TCEngine;

namespace TCGame
{
    public class ShootComponent : BaseComponent
    {
        public bool autoFire
        {
            get => this.autoFire;
            set => this.autoFire = value;
        }

        public ShootComponent(){

        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
        }
    }
}