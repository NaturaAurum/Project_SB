using Script.Obstacle.Cannon;
using UnityEngine;

namespace Script.Obstacle.Stalactite
{
    public class StalactiteCondition : MonoBehaviour, ICondition
    {
        private bool IsActivated { get; set; }
        private Rigidbody2D _rigid;
        private const Direction ObservingDirection = Direction.Down;
        private const int ObservableDistance = 100;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Debug.DrawRay(_rigid.position, DirectionHelper.DirectionToVector(ObservingDirection), Color.blue);
            var raycastHit2D =
                Physics2D.Raycast(_rigid.position, DirectionHelper.DirectionToVector(ObservingDirection),
                    ObservableDistance);
            IsActivated = (raycastHit2D.collider != null) && (raycastHit2D.collider.CompareTag("Player"));
            Debug.Log(IsActivated);
        }

        public bool CheckCondition()
        {
            return IsActivated;
        }
    }
}