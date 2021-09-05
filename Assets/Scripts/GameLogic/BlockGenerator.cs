using System;
using System.Collections;
using Obstacle.Cannon;
using Platform;
using SB.Data;
using UnityEngine;

namespace SB.GameLogic
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField]
        private int generateCountPerFrame = 20;

        public void GenerateBlock(MapInfo mapInfo, Action done)
        {
            StartCoroutine(_GenerateBlock(mapInfo, done));
        }

        private IEnumerator _GenerateBlock(MapInfo mapInfo, Action done)
        {
            var blockInfoList = mapInfo.BlockInfoList;

            int count = 0;
            
            foreach (var blockInfo in blockInfoList)
            {
                var blockId = blockInfo.Id;
                var pos = blockInfo.Position;
                var block = new GameObject("Block");
                block.transform.SetParent(transform);
                block.transform.position = pos;
                var blockComp = block.AddComponent<Block>();
                blockComp.Id = blockId;
                blockComp.SetSprite();
                if (generateCountPerFrame <= count)
                {
                    count = 0;
                    yield return null;
                }
            }

            var customBlockList = mapInfo.CustomBlockInfoList;
            foreach (var block in customBlockList)
            {
                CreateCustomBlock(block);
            }
            var movablePlatformList = mapInfo.MovablePlatformList;
            foreach (var block in movablePlatformList)
            {
                var instance = CreateCustomBlock(block);
                var movablePlatform = instance.GetComponent<MovablePlatform>();
                movablePlatform.Data = block.Data;
            }
            var cannonList = mapInfo.CannonList;
            foreach (var cannon in cannonList)
            {
                var instance = CreateCustomBlock(cannon);
                var cannonCondition = instance.GetComponent<CannonCondition>();
                cannonCondition.observingDirection = cannon.Direction;
            }
            done?.Invoke();
        }

        private GameObject CreateCustomBlock(CustomBlockInfoBase blockData)
        {
            var instance = Instantiate(blockData.Prefab, transform);
            instance.transform.position = blockData.Position;
            instance.transform.rotation = blockData.Rotation;

            return instance;
        }
    }
}