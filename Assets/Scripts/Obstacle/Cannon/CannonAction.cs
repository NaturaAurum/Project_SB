using UnityEngine;

namespace Obstacle.Cannon
{
    public class CannonAction : MonoBehaviour, IAction
    {
        public GameObject bulletObj;
        private bool _flag = true;
        private float _delayTimer;
        public float delayTime;
        private Direction _direction;

        private void Awake()
        {
            _direction = GetComponent<CannonCondition>().observingDirection;
        }
        
        public void DoAction()
        {
            if (!_flag) return;
            Shoot();
        }

        private void Shoot()
        {
            var bulletInstance = Instantiate(bulletObj, transform.position, transform.rotation);
            var bullet = bulletInstance.GetComponent<Bullet.Bullet>();
            bullet.Shoot(DirectionHelper.DirectionToVector(_direction) * 10f, this);
            _flag = false;
        }

        private void FixedUpdate()
        {
            if (_flag == false)
            {
                _delayTimer += Time.deltaTime;
            }

            if (!(_delayTimer >= delayTime)) return;
            _delayTimer = 0;
            _flag = true;
        }
    }
}