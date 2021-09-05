using Obstacle.Cannon;
using Platform;
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

                if (GUILayout.Button("Clear Tiles"))
                {
                    ClearTile();
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
            mapInfo.Key = mapTool.Id;
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

            var customBlockCount = tileMap.transform.childCount;
            for (var i = 0; i < customBlockCount; i++)
            {
                var customBlock = tileMap.transform.GetChild(i);
                var prefabObject = PrefabUtility.GetCorrespondingObjectFromOriginalSource<GameObject>(customBlock.gameObject);
                var pos = customBlock.position;
                var rot = customBlock.rotation;
                if (customBlock.GetComponent<MovablePlatform>() != null)
                {
                    var movablePlatform = customBlock.GetComponent<MovablePlatform>();
                    mapInfo.AddCustomBlockInfo(new MovablePlatformInfo()
                    {
                        Prefab = prefabObject,
                        Position = pos,
                        Rotation = rot,
                        Data = movablePlatform.Data,
                    });
                }
                else if (customBlock.GetComponent<CannonCondition>() != null)
                {
                    var cannonCondition = customBlock.GetComponent<CannonCondition>();
                    mapInfo.AddCustomBlockInfo(new CannonInfo()
                    {
                        Prefab = prefabObject,
                        Position = pos,
                        Rotation = rot,
                        Direction = cannonCondition.observingDirection,
                    });
                }
                else
                {
                    mapInfo.AddCustomBlockInfo(new CustomBlockInfoBase()
                    {
                        Prefab = prefabObject,
                        Position = pos,
                        Rotation = rot,
                    });
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
            var customBlockList = mapInfo.CustomBlockInfoList;
            var movablePlatformList = mapInfo.MovablePlatformList;
            var cannonList = mapInfo.CannonList;

            foreach (var blockInfo in blockInfoList)
            {
                var tile = mapTool.FindTileById(blockInfo.Id);
                var cellPos = tileMap.WorldToCell(blockInfo.Position);
                tileMap.SetTile(cellPos, tile);
            }

            foreach (var customBlock in customBlockList)
            {
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(customBlock.Prefab);
                if (instance != null)
                {
                    instance.transform.SetParent(tileMap.transform);
                    instance.transform.position = customBlock.Position;
                    instance.transform.rotation = customBlock.Rotation;
                }
            }
            
            foreach (var movablePlatformInfo in movablePlatformList)
            {
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(movablePlatformInfo.Prefab);
                if (instance != null)
                {
                    instance.transform.SetParent(tileMap.transform);
                    instance.transform.position = movablePlatformInfo.Position;
                    instance.transform.rotation = movablePlatformInfo.Rotation;
                    instance.GetComponent<MovablePlatform>().Data = movablePlatformInfo.Data;
                }
            }
            
            foreach (var cannonInfo in cannonList)
            {
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(cannonInfo.Prefab);
                if (instance != null)
                {
                    instance.transform.SetParent(tileMap.transform);
                    instance.transform.position = cannonInfo.Position;
                    instance.transform.rotation = cannonInfo.Rotation;
                    instance.GetComponent<CannonCondition>().observingDirection = cannonInfo.Direction;
                }
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

            var childCount = tileMap.transform.childCount;
            for (var i = childCount - 1; i >= 0 ; i--)
            {
                Object.DestroyImmediate(tileMap.transform.GetChild(i).gameObject);
            }
        }
    }
}
