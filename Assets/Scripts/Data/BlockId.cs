using SB.Data.Enums;
using Sirenix.OdinInspector;

namespace SB.Data
{
    [System.Serializable]
    public struct BlockId
    {
        [BoxGroup("Block ID")] public int Id;
        [BoxGroup("Block ID")] public BlockType Type;
        [BoxGroup("Block ID")] public CategoryType Category;

        public static BlockId By(
            BlockType type,
            CategoryType category,
            int id
        )
        {
            var blockId = new BlockId();
            blockId.Id = id;
            blockId.Type = type;
            blockId.Category = category;
            return blockId;
        }

        public override readonly string ToString()
        {
            return $"Block.{Type}.{Category}.{Id}";
        }

        public static bool operator ==(BlockId id1, BlockId id2)
        {
            var idSame = id1.Id == id2.Id;
            var typeSame = id1.Type == id2.Type;
            var themeSame = id1.Category == id2.Category;

            return idSame && typeSame && themeSame;
        }

        public static bool operator !=(BlockId id1, BlockId id2)
        {
            return !(id1 == id2);
        }
    }
}