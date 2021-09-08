using System;
using SB.Data;
using SB.GameLogic.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.GameLogic
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public CharacterBase CurrentPlayer => currentPlayer;
        private CharacterBase currentPlayer = null;

        [SerializeField]
        [Required]
        private BlockGenerator blockGenerator = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void SetMapInfo(MapInfo mapInfo)
        {
            if (mapInfo.Key.Id > 0)
            {
                blockGenerator.GenerateBlock(mapInfo, MapSettingDone);
            }
        }

        private void MapSettingDone()
        {
            var characterObject = new GameObject("Character");
            characterObject.tag = "Player";
            var characterBase = characterObject.AddComponent<CharacterBase>();
            characterBase.CharacterData = DataContainer.CharacterCommonData;
            characterObject.AddComponent<CharacterStateMachine>();
            characterObject.AddComponent<CharacterAnimator>();
            characterObject.AddComponent<CharacterPhysics>();

            currentPlayer = characterBase;

            CameraController.Instance.SetCharacter(characterBase);
        }
    }
}