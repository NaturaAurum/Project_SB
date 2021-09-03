using UnityEngine;

namespace Script.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        private IAction _action;
        private ICondition _condition;

        private void Start()
        {
            _action = GetComponent<IAction>();
            _condition = GetComponent<ICondition>();
        }

        private void Update()
        {
            if (_condition.CheckCondition()) 
            {
                _action?.DoAction();
            }
        }
    }
}