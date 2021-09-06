using System;
using SB.Extensions;
using SB.GameLogic.Command;
using UnityEngine;

namespace SB.GameLogic.Character
{
    /// <summary>
    /// 캐릭터 물리 코어 로직
    /// </summary>
    [RequireComponent(typeof(CharacterBase))]
    public class CharacterPhysics : MonoBehaviour, ICommandListener
    {
        [SerializeField]
        private float groundCheckDis = 0.02f;
        
        private CharacterBase character = null;
        private CharacterData characterData = null;
        #if UNITY_EDITOR
        public CharacterState CurrentState => currentState;
        #endif
        
        private CharacterState currentState = null;
        private CapsuleCollider2D collider = null;
        private Rigidbody2D rig = null;
        private RaycastHit2D[] groundCastResult = new RaycastHit2D[2];
        private bool isGround = true;

        private const string GROUND = "Default";
        private int groundLayer => 1 << LayerMask.NameToLayer(GROUND);
        
        private delegate void ComputeVelocity(ref Vector2 vel);

        private void Awake()
        {
            character = GetComponent<CharacterBase>();
            rig = this.GetOrAddComponent<Rigidbody2D>();
            collider = this.GetOrAddComponent<CapsuleCollider2D>();
        }

        private void Start()
        {
            // CharacterBase 초기화 다 끝나고 세팅해주기 위해서 Start에서 진행
            character.OnEnterState += OnEnterState;
            character.OnExitState += OnExitState;

            currentState = new IdleState(character);

            characterData = character.CharacterData;

            rig.drag = 0f;
            rig.angularDrag = 0f;
            rig.gravityScale = 0f;
            rig.isKinematic = false;
            rig.interpolation = RigidbodyInterpolation2D.None;
            rig.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rig.constraints = RigidbodyConstraints2D.FreezeRotation;
            rig.hideFlags = HideFlags.NotEditable;

            collider.offset = characterData.OffsetScaled;
            collider.size = characterData.SizeScaled;
            collider.direction = characterData.Direction;
            collider.hideFlags = HideFlags.NotEditable;
            
            CommandDispatcher.AddListener(this);
        }

        private void OnDestroy()
        {
            character.OnEnterState -= OnEnterState;
            character.OnExitState -= OnExitState;
            CommandDispatcher.RemoveListener(this);
        }
        
        private void CalcVelocity(ComputeVelocity func)
        {
            var vel = rig.velocity;
            func?.Invoke(ref vel);
            rig.velocity = vel;
        }

        private void OnEnterState(CharacterState state)
        {
            currentState = state;
            CalcVelocity(state.OnEnterPhysics);
        }

        private void OnExitState(CharacterState state)
        {
            CalcVelocity(state.OnExitPhysics);
        }

        private void FixedUpdate()
        {
            GroundCheck();
            HangCheck();
            if (currentState != null)
            {
                CalcVelocity(currentState.UpdatePhysics);
            }
        }

        private void OnDrawGizmos()
        {
            if (collider != null)
            {
                var halfHeight = collider.size.y * 0.5f;
                var radius = collider.size.x * 0.1f;
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(
                    (transform.position + Vector3.up * halfHeight) +
                    (Vector3.down * ((halfHeight - radius) + groundCheckDis)), radius);

                Gizmos.color = Color.red;
                var dir = rig.velocity;
                dir.y = 0;
                Gizmos.DrawRay(character.CamTarget.position, dir.normalized * character.CharacterData.HangCastDistance);
            }
        }

        private void GroundCheck()
        {
            var halfHeight = collider.size.y * 0.5f;
            var radius = collider.size.x * 0.1f;

            var hit = Physics2D.CircleCast(transform.position + Vector3.up * halfHeight, radius, Vector3.down,
                (halfHeight - radius) + groundCheckDis, groundLayer);

            var groundNow = hit.collider != null;

            // var groundNow = Physics2D.RaycastNonAlloc(transform.position, Vector3.down, groundCastResult,
            //     groundCheckDis,
            //     groundLayer) > 0;

            if (isGround != groundNow)
            {
                isGround = groundNow;
                CommandDispatcher.Dispatch(isGround ? (ICommand) new ToGroundCommand() : new ToAirCommand());
            }
        }

        private void HangCheck()
        {
            if (currentState is HangState)
            {
                character.CanHang = false;
                return;
            }

            var dir = character.MoveDirection;
            dir.y = 0;

            var hit = Physics2D.Raycast(character.CamTarget.position, dir, character.CharacterData.HangCastDistance, groundLayer);
            var normal = hit.normal;

            if (hit.collider != null)
            {
                var angle = Vector3.Angle(Vector3.up, normal);
                character.CanHang = angle > 0 && angle <= 90f;
            }
            else
            {
                character.CanHang = false;
            }
        }

        public void Listen(ICommand command)
        {
            if (command is InitCommand || command is StartCommand || command is GameOverCommand)
            {
                rig.velocity = Vector2.zero;
            }
        }
    }
}