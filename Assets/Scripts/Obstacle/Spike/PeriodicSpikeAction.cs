using UnityEngine;

namespace Obstacle.Spike
{
    public class PeriodicSpikeAction : MonoBehaviour, IAction
    {
        public int ActivateTime;
        public GameObject prefabObj;
        private GameObject obj;

        public void DoAction()
        {
            if (!GameObject.Find("SpikeClone"))
            {
                obj = Instantiate(prefabObj);
                obj.name = "SpikeClone";
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, 0);
                obj.transform.position = pos;
                Destroy(obj, ActivateTime);
            }
        }
    }
}