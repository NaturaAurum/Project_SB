using UnityEngine;

namespace Obstacle.Spike
{
    public class SpikeAction : MonoBehaviour, IAction
    {
        public void DoAction()
        {
            Debug.Log("Activate!!!!");
        }
    }
}