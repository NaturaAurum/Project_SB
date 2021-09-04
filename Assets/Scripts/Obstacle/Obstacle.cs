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
    }
}