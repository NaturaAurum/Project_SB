using System.Collections.Generic;
using UnityEngine;

namespace Script.Platform
{
    [CreateAssetMenu(fileName = "New MoveablePlatformData", menuName = "Data/MoveablePlatformData")]
    public class MoveablePlatformData : ScriptableObject
    {
        public List<PlatformMoveData> DataList = new List<PlatformMoveData>();
    }
}