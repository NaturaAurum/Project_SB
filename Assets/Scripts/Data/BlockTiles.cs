using System;
using System.Linq;
using SB.Data.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SB.Data
{
    [Serializable]
    public struct BlockTile : IKey<BlockId>
    {
        public BlockId Key
        {
            get => key;
            set => key = value;
        }
        [HideLabel]
        [SerializeField] 
        private BlockId key;

        [Required] [PreviewField(Height = 80f)]
        public Tile Tile;
    }
    
    [Required]
    [Serializable]
    [CreateAssetMenu(menuName = "Block/Tiles")]
    public class BlockTiles : KeyTable<BlockId, BlockTile>
    {
        public Tile GetTile(BlockId blockId)
        {
            var data = GetValue(blockId);
            return data.Tile;
        }

        public BlockId FindIdByValue(Tile tile) =>
            Values.Where(v => v.Tile == tile).Select(v => v.Key).FirstOrDefault();
    }
}