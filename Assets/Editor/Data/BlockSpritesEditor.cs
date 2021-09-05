using SB.Data.Enums;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace SB.Data
{
    [CustomEditor(typeof(BlockSprites))]
    public class BlockSpritesEditor : OdinEditor
    {
        private BlockSprites blockSprites = null;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            blockSprites = target as BlockSprites;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            blockSprites = null;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SirenixEditorGUI.BeginBox("Sprites Tool");

            if (GUILayout.Button("Load From Tiles"))
            {
                LoadSprites();    
            }
            
            SirenixEditorGUI.EndBox();
        }

        private void LoadSprites()
        {
            if (blockSprites == null)
                return;
            blockSprites.Clear();
            var path = "Assets/Resources/ScriptAssets/Block/BlockTiles.asset";
            var blockTiles = AssetDatabase.LoadAssetAtPath<BlockTiles>(path);
            foreach (var blockTile in blockTiles.Values)
            {
                blockSprites.Add(new BlockSprite()
                {
                    Key = blockTile.Key,
                    Sprite = blockTile.Tile.sprite,
                });
            }
            
            EditorUtility.SetDirty(blockSprites);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}