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
        private CharacterState currentState = null;
        private CapsuleCollider2D collider = null;
        private Rigidbody2D rig = null;
        private RaycastHit2D[] groundCastResult = new RaycastHit2D[2];
        private bool isGround = true;

        private const string GROUND = "Ground";
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
            if (currentState != null)
            {
                CalcVelocity(currentState.UpdatePhysics);
            }
        }

        private void GroundCheck()
        {
            var groundNow = Physics2D.RaycastNonAlloc(transform.position, Vector3.down, groundCastResult,
                groundCheckDis,
                groundLayer) > 0;

            if (isGround != groundNow)
            {
                isGround = groundNow;
                CommandDispatcher.Dispatch(isGround ? (ICommand) new ToGroundCommand() : new ToAirCommand());
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