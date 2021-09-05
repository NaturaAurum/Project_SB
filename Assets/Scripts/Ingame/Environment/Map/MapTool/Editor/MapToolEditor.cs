using SB.Data;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace SB.Ingame.Environment.Map.MapTool
{
    [CustomEditor(typeof(MapTool))]
    public class MapToolEditor : OdinEditor
    {
        private const string dataPath = "Assets/Resources/ScriptAsset/Map/MapData.asset";

        private MapTool mapTool = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            mapTool = target as MapTool;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (mapTool != null)
            {
                if (mapTool.Data == null)
                {
                    if (GUILayout.Button("Load Map Data"))
                    {
                        mapTool.Data = AssetDatabase.LoadAssetAtPath<MapData>(dataPath);
                    }

                    return;
                }

                if (GUILayout.Button("Convert"))
                {
                    Convert();
                }

                if (GUILayout.Button("Load"))
                {
                    Load();
                }
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            mapTool = null;
        }

        private void Convert()
        {
            var tileMap = mapTool.TargetTileMap;
            var cellBounds = tileMap.cellBounds;
            var data = mapTool.Data;

            if (data.Contains(mapTool.Id))
            {
                data.Remove(mapTool.Id);
            }

            var mapInfo = new MapInfo();
            mapInfo.Clear();

            foreach (var pos in cellBounds.allPositionsWithin)
            {
                if (!tileMap.HasTile(pos))
                {
                    continue;
                }

                var tile = tileMap.GetTile<Tile>(pos);
                if (tile == null)
                {
                    continue;
                }

                var id = mapTool.FindIdByValue(tile);
                if (id.Id > 0)
                {
                    var worldPos = tileMap.CellToWorld(pos);
                    var blockInfo = new BlockInfo();
                    blockInfo.Id = id;
                    blockInfo.Position = worldPos;
                    mapInfo.AddBlockInfo(blockInfo);
                }
            }

            data.Add(mapInfo);
            
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void Load()
        {
            ClearTile();

            var tileMap = mapTool.TargetTileMap;
            var data = mapTool.Data;
            var mapInfo = data.GetValue(mapTool.Id);
            var blockInfoList = mapInfo.BlockInfoList;
            
            foreach (var blockInfo in blockInfoList)
            {
                var tile = mapTool.FindTileById(blockInfo.Id);
                var cellPos = tileMap.WorldToCell(blockInfo.Position);
                tileMap.SetTile(cellPos, tile);
            }
        }
        
        private void ClearTile()
        {
            var tileMap = mapTool.TargetTileMap;
            var cellBounds = tileMap.cellBounds;
            
            foreach (var pos in cellBounds.allPositionsWithin)
            {
                if (tileMap.HasTile(pos))
                {
                    tileMap.SetTile(pos, null);
                }
            }
        }
    }
}
