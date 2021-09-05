using System;
using SB.GameLogic.Character;
using UnityEngine;

namespace SB.Data
{
    public static class DataContainer
    {
        public static MapData Map { get; private set; }
        public static CharacterData CharacterCommonData { get; private set; }
        public static BlockSprites BlockSpriteData { get; private set; }
        
        
        static DataContainer()
        {
            Map = Resources.Load<MapData>("ScriptAssets/Map/MapData");
            CharacterCommonData = Resources.Load<CharacterData>("Data/CharacterCommonData");
            BlockSpriteData = Resources.Load<BlockSprites>("ScriptAssets/Sprites/BlockSprites");
        }
    }
}
