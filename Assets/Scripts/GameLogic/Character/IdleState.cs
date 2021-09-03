using SB.GameLogic.Command;

namespace SB.GameLogic.Character
{
    public class IdleState : CharacterState
    {
        public IdleState(CharacterBase character) : base(character)
        {
        }

        public override CharacterStateType Type => CharacterStateType.Idle;
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
    }
}