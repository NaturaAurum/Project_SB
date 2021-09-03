using SB.GameLogic.Command;
using UnityEngine;

namespace SB.GameLogic.Character
{
    public class RunState : CharacterState
    {
        public RunState(CharacterBase character) : base(character)
        {
        }

        public override CharacterStateType Type => CharacterStateType.Run;
        protected override void DoCommand(ICommand command)
        {
            if (CanJump(command))
            {
                nextState = new JumpState(character);
            }
        }
        
        public override void Update()
        {
            FindNextState();
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            DefaultUpdatePhysics(ref velocity, character.MoveDirection, character.CharacterData.GroundMaxSpeed,
                character.CharacterData.GroundAcc);
            base.UpdatePhysics(ref velocity);
        }
    }
}