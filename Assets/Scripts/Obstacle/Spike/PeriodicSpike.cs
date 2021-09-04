using UnityEngine;

namespace Obstacle.Spike
{
    public class PeriodicSpike : MonoBehaviour
    {
        public int ActivateTime;
        public int DeactivateTime;
        public GameObject prefabObj;

        void Start()
        {
            InvokeRepeating(nameof(SpikeUp), DeactivateTime, DeactivateTime);
        }

        void SpikeUp()
        {
            GameObject obj = Instantiate(prefabObj);
            obj.name = "clone";
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, 0);
            obj.transform.position = pos;
            Destroy(obj, ActivateTime);
        }
    }
}