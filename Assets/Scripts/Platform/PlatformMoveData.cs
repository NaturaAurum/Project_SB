using UnityEngine;

namespace Script.Platform
{
    [System.Serializable]
    public class PlatformMoveData
    {
        public float MoveTime;
        public float WaitTime;
        public float Speed;
        public Vector3 Direction;
        public bool useTarget;
        public Vector2 TargetPosition;
    }
}