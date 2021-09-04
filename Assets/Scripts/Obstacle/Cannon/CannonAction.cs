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
            Debug.Log("shoot");
            Shoot();
        }

        private void Shoot()
        {
            var bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            var rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(DirectionHelper.DirectionToVector(_direction) * 10, ForceMode2D.Impulse);
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