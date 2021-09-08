using System;
using SB.GameLogic.Command;
using UnityEngine;

namespace Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        private IAction _action;
        private ICondition _condition;

        public void Start()
        {
            _action = GetComponent<IAction>();
            _condition = GetComponent<ICondition>();
        }

        public void Update()
        {
            if (_condition.CheckCondition()) 
            {
                _action?.DoAction();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log($"Player Hit! {gameObject.name}[{transform.GetSiblingIndex()}]");
                CommandDispatcher.Dispatch(new CharacterHitCommand());
            }
        }
    }
}