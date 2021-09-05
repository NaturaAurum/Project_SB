using Obstacle.Cannon;
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    [CustomGridBrush(false, true, false, nameof(CannonBrush))]
    public class CannonBrush : SBCustomBrush
    {
        public Direction Direction;

        public override void SettingPrefabInstance(GameObject prefabInstance)
        {
            base.SettingPrefabInstance(prefabInstance);

            var cc = prefabInstance.GetComponent<CannonCondition>();
            cc.observingDirection = Direction;
        }
    }
}