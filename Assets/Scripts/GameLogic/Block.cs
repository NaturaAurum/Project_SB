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
        private SpriteRenderer spriteRenderer = null;

        [SerializeField]
        private BoxCollider2D collider = null;

        public void SetSprite()
        {
            if (spriteRenderer == null)
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var blockSprites = DataContainer.BlockSpriteData;
            var sprite = blockSprites.GetValue(Id);

            spriteRenderer.sprite = sprite.Sprite;

            var path = new List<Vector2>();

            if (collider == null)
                collider = gameObject.AddComponent<BoxCollider2D>();

            collider.size = sprite.Sprite.bounds.size;

            // collider.pathCount = sprite.Sprite.GetPhysicsShapeCount();
            // for (var i = 0; i < collider.pathCount; i++)
            // {
            //     path.Clear();
            //     sprite.Sprite.GetPhysicsShape(i, path);
            //     collider.SetPath(i, path.ToArray());
            // }
        }
    }
}