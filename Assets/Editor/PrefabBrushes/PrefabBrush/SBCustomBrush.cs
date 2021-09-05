using System.Linq;
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    public class SBCustomBrush : PrefabBrush
    {
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            var brushTargetName = brushTarget.name;
            var parent = brushTarget.transform;

            if (brushTargetName == "Grid")
            {
                parent = brushTarget.transform.GetChild(0);
            }

            var objectsInCell = GetObjectsInCell(grid, parent, position);
            var existPrefabObjectInCell = objectsInCell.Any(objectInCell =>
                PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(objectInCell) == m_Prefab);

            if (!existPrefabObjectInCell)
            {
                var prefabInstance = base.InstantiatePrefabInCell(grid, brushTarget, position, m_Prefab, m_Rotation);
                SettingPrefabInstance(prefabInstance);
            }
        }

        public virtual void SettingPrefabInstance(GameObject prefabInstance)
        {
        }
    }
}