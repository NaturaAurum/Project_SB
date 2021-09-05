using SB.GameLogic.Command;
using SB.GameLogic.States;
using UnityEngine;

namespace SB.GameLogic.Character
{
    public enum CharacterStateType
    {
        Idle,
        Air,
        Run,
        Jump,
        Hang,
        HangJump,
    }


    /// <summary>
    /// 캐릭터 State 들이 상속받는 Class
    /// </summary>
    public abstract class CharacterState : IState<CharacterState>
    {
        public CharacterState(CharacterBase character)
        {
            this.character = character;
        }
        
        public abstract CharacterStateType Type { get; }
        
        protected CharacterBase character { get; }
        protected CharacterState nextState = null;
        
        public virtual CharacterState NextState() => nextState;
        
        public virtual void Update()
        {
            
        }
        
        public void Listen(ICommand command)
        {
            if (command is InitCommand)
            {
                nextState = new IdleState(character);
            }
            DoCommand(command);
        }

        protected abstract void DoCommand(ICommand command);

        protected bool CanJump(ICommand command) => command is JumpCommand && character.CanJump;

        protected void FindNextState()
        {
            if (nextState != null)
                return;
            if (!character.IsGround)
            {
                nextState = new AirState(character);
            }
            else if (character.MoveDirection.magnitude > float.Epsilon)
            {
                nextState = new RunState(character);
            }
            else
            {
                nextState = new IdleState(character);
            }
        }

        #region Physics Logic

        public virtual void UpdatePhysics(ref Vector2 velocity)
        {
            
        }

        public virtual void OnEnterPhysics(ref Vector2 velocity)
        {
            
        }

        public virtual void OnExitPhysics(ref Vector2 velocity)
        {
            
        }

        protected void DefaultUpdatePhysics(ref Vector2 velocity, Vector2 direction, float maxX, float accX)
        {
            var targetVel = direction.x * maxX;
            var diff = targetVel - velocity.x;

            float changeNow = accX * Time.fixedDeltaTime;
            if (diff > changeNow)
            {
                var add = diff * changeNow;
                velocity.x += add;
            }
            else
            {
                velocity.x = targetVel;
            }

            if (!character.IsGround)
            {
                var y = velocity.y;
                var gravity = character.Gravity;

                y -= gravity * Time.fixedDeltaTime;
                velocity.y = y;
            }
        }

        protected void UpdateKnockback(ref Vector2 velocity)
        {
            // TODO : 넉백 관련 변수 따로 빼서 계산해줘야할까?
            if (!character.IsGround)
            {
                var y = velocity.y;
                var gravity = character.Gravity;

                y -= gravity * Time.fixedDeltaTime;
                velocity.y = y;
            }
        }

        #endregion
    }
}
