using SB.GameLogic.Command;
using UnityEngine;

namespace SB.GameLogic.Character
{
    public class AirState : CharacterState
    {
        public AirState(CharacterBase character) : base(character)
        {
        }

        public override CharacterStateType Type => CharacterStateType.Air;
        protected override void DoCommand(ICommand command)
        {
            if (CanHang(command))
            {
                nextState = new HangState(character);
            }
            else if (CanJump(command))
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
            DefaultUpdatePhysics(ref velocity, character.MoveDirection, character.CharacterData.AirMaxSpeed,
                character.CharacterData.AirAcc);
            base.UpdatePhysics(ref velocity);
        }
    }
}