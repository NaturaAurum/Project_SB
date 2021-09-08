using System;
using SB.GameLogic.Command;
using UnityEngine;

namespace Obstacle.Cannon.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private CannonAction shooter = null;

        private Rigidbody2D rig = null;
        
        private float _durationTimer;
        public float durationTime;

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
        }

        public void Shoot(Vector2 velocity, CannonAction shooter)
        {
            this.shooter = shooter;
            rig.AddForce(velocity, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (_durationTimer >= durationTime)
            {
                DestroyBullet();
            }
            else
            {
                _durationTimer += Time.deltaTime;
            }
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CommandDispatcher.Dispatch(new CharacterHitCommand());
                Debug.Log($"Bullet Hit! : {shooter.name}:[{shooter.transform.GetSiblingIndex()}]");
                DestroyBullet();
            }
        }
    }
}