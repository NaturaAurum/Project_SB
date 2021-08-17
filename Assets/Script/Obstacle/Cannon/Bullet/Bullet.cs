using UnityEngine;

namespace Script.Obstacle.Cannon.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private float _durationTimer;
        public float durationTime;
        private void FixedUpdate()
        {
            if (_durationTimer >= durationTime)
            {
                Destroy(gameObject);
            }
            else
            {
                _durationTimer += Time.deltaTime;
            }
        }
        
        
    }
}