using System.Collections.Generic;
using SB.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.GameLogic
{
    public class Block : MonoBehaviour
    {
        public BlockId Id;

        [SerializeField]
        [Required]
        private SpriteRenderer spriteRenderer = null;

        [SerializeField]
        [Required]
        private PolygonCollider2D collider = null;

        public void SetSprite()
        {
            var blockSprites = DataContainer.BlockSpriteData;
            var sprite = blockSprites.GetValue(Id);

            spriteRenderer.sprite = sprite.Sprite;

            var path = new List<Vector2>();

            collider.pathCount = sprite.Sprite.GetPhysicsShapeCount();
            for (var i = 0; i < collider.pathCount; i++)
            {
                path.Clear();
                sprite.Sprite.GetPhysicsShape(i, path);
                collider.SetPath(i, path.ToArray());
            }
        }
    }
}