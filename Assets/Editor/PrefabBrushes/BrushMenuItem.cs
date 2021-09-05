using UnityEngine;

namespace UnityEditor.Tilemaps
{
    static internal partial class AssetCreation
    {
        [MenuItem("Assets/Create/2D/Brushes/Prefab Brush")]
        static void CreatePrefabBrush()
        {
            ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<PrefabBrush>(), "New Prefab Brush.asset");
        }

        [MenuItem("Assets/Create/2D/Brushes/Prefab Random Brush")]
        static void CreatePrefabRandomBrush()
        {
            ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<PrefabRandomBrush>(),
                "New Prefab Random Brush.asset");
        }

        [MenuItem("Assets/Create/2D/Brushes/Random Brush")]
        static void CreateRandomBrush()
        {
            ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<RandomBrush>(), "New Random Brush.asset");
        }

        [MenuItem("Assets/Create/2D/Brushes/MovablePlatformBrush")]
        static void CreateMovablePlatformBrush()
        {
            Create<MovablePlatformBrush>();
        }
        
        [MenuItem("Assets/Create/2D/Brushes/PeriodicSpikeBrush")]
        static void CreatePeriodicSpikeBrush()
        {
            Create<PeriodicSpikeBrush>();
        }
        
        [MenuItem("Assets/Create/2D/Brushes/CannonBrush")]
        static void CreateCannonBrush()
        {
            Create<CannonBrush>();
        }
        
        [MenuItem("Assets/Create/2D/Brushes/SpikeBrush")]
        static void CreateSpikeBrush()
        {
            Create<SpikeBrush>();
        }
        
        [MenuItem("Assets/Create/2D/Brushes/StalactiteBrush")]
        static void CreateStalactiteBrush()
        {
            Create<StalactiteBrush>();
        }

        private static void Create<T>() where T : ScriptableObject
        {
            ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<T>(), $"New {typeof(T).Name}.asset");
        }
    }
}