using Obstacle.Cannon;
using UnityEngine;

namespace Obstacle.Stalactite
{
    public class StalactiteAction : MonoBehaviour, IAction
    {
        private Rigidbody2D _rigid;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        public void DoAction()
        {
            _rigid.constraints = RigidbodyConstraints2D.None;
            _rigid.AddForce(DirectionHelper.DirectionToVector(Direction.Down), ForceMode2D.Impulse);
        }
    }
}