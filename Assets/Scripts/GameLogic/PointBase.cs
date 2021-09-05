using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.GameLogic
{
    public class PointBase : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private SpriteRenderer spriteRenderer;

        protected void Awake()
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}