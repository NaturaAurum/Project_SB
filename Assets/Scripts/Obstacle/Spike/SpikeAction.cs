using UnityEngine;

namespace Script.Obstacle.Spike
{
    public class SpikeAction : MonoBehaviour, IAction
    {
        public void DoAction()
        {
            Debug.Log("Activate!!!!");
        }
    }
}