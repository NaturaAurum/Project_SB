using System;
using SB.GameLogic.Command;
using UnityEngine;

namespace SB.GameLogic.Character
{
    public class JumpState : CharacterState
    {
        private int frame = 0;
        private int maxFrame = 0;
        
        public JumpState(CharacterBase character) : base(character)
        {
            frame = 0;
            maxFrame = character.CharacterData.JumpStateFrame;
        }

        public override CharacterStateType Type => CharacterStateType.Jump;
        protected override void DoCommand(ICommand command)
        {
            if (CanJump(command))
            {
                nextState = new JumpState(character);
            }
        }

        public override void Update()
        {
            frame++;
            if (frame >= maxFrame)
            {
                FindNextState();
            }
        }

        public override void OnEnterPhysics(ref Vector2 velocity)
        {
            var vel = velocity;
            vel.y = character.CharacterData.JumpPower;
            velocity = vel;
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            DefaultUpdatePhysics(ref velocity, character.MoveDirection, character.CharacterData.AirMaxSpeed,
                character.CharacterData.AirAcc);
        }
    }
}