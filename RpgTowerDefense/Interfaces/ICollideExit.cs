﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    interface ICollideExit
    {
        void OnCollisionExit(Collider other);
    }
}