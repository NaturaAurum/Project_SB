using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.GameLogic.Character
{
    /// <summary>
    /// 캐릭터 관련 수치들 저장하는 ScriptableObject
    /// <para>CreateAssetMenu는 Project Window에서 우클릭해서 나오는 메뉴에서 Create 부분에 바인딩 해준다.</para>
    /// </summary>
    [CreateAssetMenu(fileName = "New CharacterData", menuName = "Data/Character/Common")]
    public class CharacterData : ScriptableObject
    {
        [FoldoutGroup("Health")]
        [LabelText("체력")]
        public float Health;
        
        [FoldoutGroup("Ground Physics")]
        [LabelText("땅에서 가속도")]
        public float GroundAcc;
        [FoldoutGroup("Ground Physics")]
        [LabelText("땅에서 최대속도")]
        public float GroundMaxSpeed;

        [FoldoutGroup("Air Physics")]
        [LabelText("공중에서 가속도")]
        public float AirAcc;
        [FoldoutGroup("Air Physics")]
        [LabelText("공중에서 최대속도")]
        public float AirMaxSpeed;
        [FoldoutGroup("Air Physics")]
        [LabelText("중력값")]
        public float GravityPower;
        [FoldoutGroup("Air Physics")]
        [LabelText("최대 낙하 속도")]
        public float FallingMaxSpeed;

        [FoldoutGroup("Jump")]
        [LabelText("점프에 줄 힘 값")]
        public float JumpPower;
        [FoldoutGroup("Jump")]
        [LabelText("최대 점프 횟수")]
        public int MaxJumpCount;
        [FoldoutGroup("Jump")]
        [LabelText("점프 횟수 회복 조건일 때 회복할 횟수")]
        public int RecoveryJumpCount;

        [FoldoutGroup("Jump")]
        [LabelText("점프 스테이트 유지 프레임")]
        public int JumpStateFrame = 3;


        // TODO : Collider 세팅 추가?
        [FoldoutGroup("Collider")]
        [LabelText("캡슐 콜라이더 중심")]
        [SerializeField]
        private Vector2 offset;
        public Vector2 OffsetScaled => offset * Scale;

        [FoldoutGroup("Collider")]
        [LabelText("캡슐 콜라이더 사이즈 값")]
        [SerializeField]
        private Vector2 size;
        public Vector2 SizeScaled => size * Scale;

        [FoldoutGroup("Collider")]
        [LabelText("캡슐 콜라이더 방향")]
        public CapsuleDirection2D Direction;

        [FoldoutGroup("Model")]
        [LabelText("모델 프리팹")]
        public GameObject ModelPrefab;
        [FoldoutGroup("Model")]
        [LabelText("모델 크기")]
        public float Scale;
    }
}