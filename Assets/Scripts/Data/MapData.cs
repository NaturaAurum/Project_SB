using System;
using System.Collections.Generic;
using Obstacle.Cannon;
using Platform;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.Data
{
    [Serializable]
    public class CustomBlockInfoBase
    {
        public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    [Serializable]
    public class MovablePlatformInfo : CustomBlockInfoBase
    {
        public MovablePlatformData Data;
    }

    [Serializable]
    public class CannonInfo : CustomBlockInfoBase
    {
        public Direction Direction;
    }
    
    [Serializable]
    public struct MapInfo : IKey<MapId>
    {
        public MapId Key
        {
            get => id;
            set => id = value;
        }
        public IReadOnlyList<BlockInfo> BlockInfoList => blockInfoList;
        public IReadOnlyList<CustomBlockInfoBase> CustomBlockInfoList => customBlockInfoList;
        public IReadOnlyList<MovablePlatformInfo> MovablePlatformList => movablePlatformList;

        public IReadOnlyList<CannonInfo> CannonList => cannonList;

        [HideLabel]
        [SerializeField] private MapId id;
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 2)]
        [SerializeField] private List<BlockInfo> blockInfoList;

        [SerializeField]
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 2)]
        private List<CustomBlockInfoBase> customBlockInfoList;

        [SerializeField]
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 2)]
        private List<MovablePlatformInfo> movablePlatformList;

        [SerializeField]
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 2)]
        private List<CannonInfo> cannonList;
        
        

        public void AddBlockInfo(BlockInfo blockInfo)
        {
            if (blockInfoList == null)
                blockInfoList = new List<BlockInfo>();
            
            blockInfoList.Add(blockInfo);
        }

        public void AddCustomBlockInfo(CustomBlockInfoBase info)
        {
            if (info is MovablePlatformInfo movable)
            {
                if (movablePlatformList == null)
                    movablePlatformList = new List<MovablePlatformInfo>();
                movablePlatformList.Add(movable);
            }
            else if (info is CannonInfo cannon)
            {
                if (cannonList == null)
                    cannonList = new List<CannonInfo>();
                cannonList.Add(cannon);
            }
            else
            {
                if (customBlockInfoList == null)
                    customBlockInfoList = new List<CustomBlockInfoBase>();
                customBlockInfoList.Add(info);
            }
        }

        public void Clear()
        {
            blockInfoList?.Clear();
            customBlockInfoList?.Clear();
            movablePlatformList?.Clear();
            cannonList?.Clear();
        }

        [Button]
        private void Sort()
        {
            blockInfoList.Sort((v1, v2) => (int) (v1.Position.x - v2.Position.x));
        }
    }
    
    [CreateAssetMenu(fileName = "new MapData", menuName = "Data/MapData")]
    public class MapData : KeyTable<MapId, MapInfo>
    {
        
    }
}