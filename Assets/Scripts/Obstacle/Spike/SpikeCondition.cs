using UnityEngine;

namespace Obstacle.Spike
{
    public class SpikeCondition : MonoBehaviour, ICondition
    {
        private bool IsActivated { get; set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IsActivated = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IsActivated = false;
            }
        }


        public bool CheckCondition()
        {
            return IsActivated;
        }
    }
}