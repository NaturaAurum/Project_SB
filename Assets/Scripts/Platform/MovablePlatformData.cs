using System.Collections.Generic;
using Script.Platform;
using UnityEngine;

namespace Platform
{
    [CreateAssetMenu(fileName = "New MoveablePlatformData", menuName = "Data/MoveablePlatformData")]
    public class MovablePlatformData : ScriptableObject
    {
        public List<PlatformMoveData> DataList = new List<PlatformMoveData>();
    }
}