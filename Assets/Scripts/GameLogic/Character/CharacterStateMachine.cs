using SB.GameLogic.Command;
using SB.GameLogic.States;
using UnityEngine;

namespace SB.GameLogic.Character
{
    /// <summary>
    /// CharacterState로 돌아가는 StateMachine 관리하는 클래스
    /// </summary>
    [RequireComponent(typeof(CharacterBase))]
    public class CharacterStateMachine : MonoBehaviour, ICommandListener
    {
        private CharacterBase character = null;
        private StateMachine<CharacterState> stateMachine = null;
        
        private void Awake()
        {
            character = GetComponent<CharacterBase>();
            // state machine init
            stateMachine = new StateMachine<CharacterState>(new IdleState(character));

            stateMachine.OnEnterState = (state) => character.OnEnterState?.Invoke(state);
            stateMachine.OnExitState = (state) => character.OnExitState?.Invoke(state);

            CommandDispatcher.AddListener(this);
        }
        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
        }

        public void Listen(ICommand command)
        {
            stateMachine.Listen(command);
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }
}