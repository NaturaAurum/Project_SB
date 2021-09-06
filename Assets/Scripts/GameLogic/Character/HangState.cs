using SB.GameLogic.Command;
using UnityEngine;

namespace SB.GameLogic.Character
{
    public class HangState : CharacterState
    {

        public HangState(CharacterBase character) : base(character)
        {
        }

        public override CharacterStateType Type => CharacterStateType.Hang;
        protected override void DoCommand(ICommand command)
        {
            if (command is JumpCommand)
            {
                nextState = new JumpState(character);
            }
        }

        public override void OnEnterPhysics(ref Vector2 velocity)
        {
            velocity = Vector2.zero;
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            velocity.y -= character.CharacterData.HangGravityPower * Time.fixedDeltaTime;
        }
    }
}