using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCEngine;

namespace TCGame
{
    public class DieComponent : BaseComponent
    {
        public DieComponent(){
            Owner.Destroy();
        }
    }
}