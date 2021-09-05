using Platform;
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    [CustomGridBrush(false, true, false, nameof(MovablePlatformBrush))]
    public class MovablePlatformBrush : SBCustomBrush
    {
        public MovablePlatformData Data;

        public override void SettingPrefabInstance(GameObject prefabInstance)
        {
            base.SettingPrefabInstance(prefabInstance);

            var movablePlatform = prefabInstance.GetComponent<MovablePlatform>();
            movablePlatform.Data = Data;
        }
    }
}