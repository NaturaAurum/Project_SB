using System;
using System.Collections;
using System.Collections.Generic;
using SB.GameLogic.Command;
using UnityEngine;

namespace SB.GameLogic.Character
{
    /// <summary>
    /// 캐릭터 관련 공통 부분 모아두는 Base 클래스
    /// </summary>
    public class CharacterBase : MonoBehaviour, ICommandListener
    {
        public Action<CharacterState> OnEnterState;
        public Action<CharacterState> OnExitState;

        public CharacterData CharacterData = null;
        
        public bool IsGround { get; private set; }
        public float Gravity => CharacterData.GravityPower;

        public bool CanJump => RemainJumpCount > 0;
        
        public int RemainJumpCount { get; private set; }
        public int Health { get; private set; }
        
        public Vector2 MoveDirection { get; private set; }

        private void Awake()
        {
            CommandDispatcher.AddListener(this);

            RemainJumpCount = CharacterData.MaxJumpCount;
            
            OnEnterState += InternalOnEnterState;
            OnExitState += InternalOnExitState;
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
            OnEnterState -= InternalOnEnterState;
            OnExitState -= InternalOnExitState;
        }

        private void InternalOnExitState(CharacterState state)
        {
            if (state is JumpState)
            {
                RemainJumpCount--;
            }
        }

        private void InternalOnEnterState(CharacterState state)
        {
            
        }

        public void Listen(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                IsGround = true;
                RemainJumpCount = CharacterData.MaxJumpCount;
            }
            else if (command is ToAirCommand)
            {
                IsGround = false;
            }
            else if (command is MoveCommand moveCommand)
            {
                MoveDirection = moveCommand.Direction;
            }
            else if (command is CharacterHitCommand)
            {
                Health--;
                if (Health == 0)
                {
                    Destroy(this);
                }
            }
        }
    }
}
