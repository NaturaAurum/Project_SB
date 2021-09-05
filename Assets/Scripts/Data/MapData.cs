using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.Data
{
    [Serializable]
    public struct MapInfo : IKey<MapId>
    {
        public MapId Key => id;
        public IReadOnlyList<BlockInfo> BlockInfoList => blockInfoList;

        [HideLabel]
        [SerializeField] private MapId id;
        [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 2)]
        [SerializeField] private List<BlockInfo> blockInfoList;

        public void AddBlockInfo(BlockInfo blockInfo)
        {
            if (blockInfoList == null)
                blockInfoList = new List<BlockInfo>();
            
            blockInfoList.Add(blockInfo);
        }

        public void Clear()
        {
            if (blockInfoList != null)
            {
                blockInfoList.Clear();
            }
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