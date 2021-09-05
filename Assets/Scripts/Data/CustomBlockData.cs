using System;
using System.Collections.Generic;
using Obstacle.Cannon;
using Platform;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.Data
{
    


    public struct CustomBlockInfo : IKey<MapId>
    {
        public MapId Key
        {
            get => key;
            set => key = value;
        }

        [SerializeField]
        [HideLabel]
        private MapId key;

        
    }
    
    public class CustomBlockData
    {
        
    }
}