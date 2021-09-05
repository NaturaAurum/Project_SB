using System.Collections;
using System.Collections.Generic;
using SB.Data.Enums;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SB.Data
{
    [CustomEditor(typeof(BlockTiles))]
    public class BlockTilesEditor : OdinEditor
    {
        private BlockType blockType;
        private CategoryType categoryType;

        private const string basePath = "Assets/Resources/Tilemap/";

        private BlockTiles blockTiles = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            blockTiles = target as BlockTiles;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            blockTiles = null;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SirenixEditorGUI.BeginBox("Tile Tool");

            blockType = (BlockType) SirenixEditorFields.EnumDropdown("Block Type", blockType);
            categoryType = (CategoryType) SirenixEditorFields.EnumDropdown("Category Type", categoryType);
            if (GUILayout.Button("Load Tile"))
            {
                LoadTile();
            }

            if (GUILayout.Button("Clear"))
            {
                Clear();
            }
            
            SirenixEditorGUI.EndBox();
        }
        
        private void LoadTile()
        {
            var block = blockType.ToString();
            var category = categoryType.ToString();
            var path = $"{basePath}{category}/{block}";
            int id = 1;

            var guidPaths = AssetDatabase.FindAssets("t:Tile", new[] {path});
            foreach (var guidPath in guidPaths)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guidPath);
                var tile = AssetDatabase.LoadAssetAtPath<Tile>(assetPath);
                blockTiles.Add(new BlockTile()
                {
                    Key = BlockId.By(blockType, categoryType, id),
                    Tile = tile,
                });
                id++;
            }

            EditorUtility.SetDirty(blockTiles);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void Clear()
        {
            blockTiles.Clear();
        }
    }
}
