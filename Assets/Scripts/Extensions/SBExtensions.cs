using System;
using UnityEngine;

namespace SB.Extensions
{
    public static class SBExtensions
    {
        public static T GetOrAddComponent<T>(this Component comp) where T : Component
        {
            var targetComp = comp.GetComponent<T>();
            if (targetComp == null)
            {
                targetComp = comp.gameObject.AddComponent<T>();
            }

            return targetComp;
        }
        
        public static Transform FindDeep(this Component comp, string name, bool includeInactive = false)
        {
            return comp.gameObject.FindDeep(name, includeInactive);
        }
        
        public static Transform FindDeep(this GameObject gameObject, string name, bool includeInactive = false)
        {
            var tfs = gameObject.GetComponentsInChildren<Transform>(includeInactive);
            foreach (var transform in tfs)
            {
                if (transform.name.Equals(name))
                    return transform;
            }

            return null;
        }
    }
}
