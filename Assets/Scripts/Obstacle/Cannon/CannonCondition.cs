using UnityEngine;

namespace Obstacle.Cannon
{
    public class CannonCondition : MonoBehaviour, ICondition
    {
        private bool IsActivated { get; set; }
        private Rigidbody2D _rigid;
        public Direction observingDirection;
        public int observableDistance;
        public bool alwaysTriggered;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (alwaysTriggered)
            {
                IsActivated = true;
            }
            else
            {
                Debug.DrawRay(_rigid.position, DirectionHelper.DirectionToVector(observingDirection), Color.blue);
                var raycastHit2D =
                    Physics2D.Raycast(_rigid.position, DirectionHelper.DirectionToVector(observingDirection),
                        observableDistance);
                IsActivated = (raycastHit2D.collider != null) && (raycastHit2D.collider.CompareTag("Player"));
            }
        }

        public bool CheckCondition()
        {
            return IsActivated;
        }
    }
}