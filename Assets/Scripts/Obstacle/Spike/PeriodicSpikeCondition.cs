using UnityEngine;

namespace Obstacle.Spike
{
    public class PeriodicSpikeCondition : MonoBehaviour, ICondition
    {
        private bool IsActivated { get; set; }
        public int DeactivateTime;
        
        void Start()
        {
            InvokeRepeating(nameof(ReverseIsActivated), DeactivateTime, DeactivateTime);
        }

        void ReverseIsActivated()
        {
            IsActivated = !IsActivated;
        }
        
        public bool CheckCondition()
        {
            return IsActivated;
        }
    }
}