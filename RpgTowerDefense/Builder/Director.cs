﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class Director
    {
        private IBuilder builder;
        public IBuilder Builder
        {
            get { return builder; }
            set { builder = value; }
        }
        public Director(IBuilder builder)
        {
            this.builder = builder;
        }
        public GameObject Construct(Vector2 position)
        {
            builder.BuildGameObject(position);
            return builder.GetResult();
        }
        //overide that allows Tile IDing 
        public GameObject Construct(Vector2 position, int id)
        {
            builder.BuildGameObject(position, id);
            return builder.GetResult();
        }
        public GameObject Construct(Vector2 position, int id, Vector2 direction)
        {
            builder.BuildGameObject(position, id, direction);
            return builder.GetResult();
        }


    }
}
