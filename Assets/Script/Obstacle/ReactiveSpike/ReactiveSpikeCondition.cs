using UnityEngine;

namespace Script.Obstacle.ReactiveSpike
{
    public class ReactiveSpikeCondition : MonoBehaviour, ICondition
    {
        public bool IsActivated { get; set; }

        public bool CheckCondition()
        {
            throw new System.NotImplementedException();
        }
    }
}