using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.Data
{
    [Serializable]
    public struct BlockSprite : IKey<BlockId>
    {
        public BlockId Key
        {
            get => key;
            set => key = value;
        }

        [HideLabel]
        [SerializeField]
        private BlockId key;

        [Required]
        [PreviewField(Height = 80f)]
        public Sprite Sprite;
    }
    
    [Required]
    [Serializable]
    [CreateAssetMenu(menuName = "Block/Sprites")]
    public class BlockSprites : KeyTable<BlockId, BlockSprite>
    {
        
    }
}