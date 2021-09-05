using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.Data
{
    [Serializable]
    public struct BlockInfo
    {
        [HideLabel]
        public BlockId Id;
        public Vector2 Position;
    }
}