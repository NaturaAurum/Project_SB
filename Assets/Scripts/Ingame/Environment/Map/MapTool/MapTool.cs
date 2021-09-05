using System;
using System.Collections;
using System.Collections.Generic;
using SB.Data;
using SB.GameLogic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace SB.Ingame.Environment.Map.MapTool
{
    public class MapTool : MonoBehaviour
    {
        public MapData Data = null;
        public MapId Id;

        public Transform BlockParent;
        public GameObject BlockPrefab;

        public Tilemap TargetTileMap;

        public BlockTiles TileData = null;

        private void Awake()
        {
            TargetTileMap.transform.parent.gameObject.SetActive(false);
        }

        private void Start()
        {
            GameManager.Instance.SetMapInfo(Data.GetValue(Id));
        }

        public BlockId FindIdByValue(Tile tile)
        {
            if (TileData == null)
                return default;

            return TileData.FindIdByValue(tile);
        }

        public Tile FindTileById(BlockId id)
        {
            if (TileData == null)
                return null;

            return TileData.GetTile(id);
        }
    }
}
