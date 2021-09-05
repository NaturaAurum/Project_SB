using System;
using SB.Extensions;
using UnityEngine;

namespace SB.GameLogic.Character
{
    [RequireComponent(typeof(CharacterBase))]
    public class CharacterAnimator : MonoBehaviour
    {
        private CharacterBase character = null;
        private Animator animator = null;
        private SpriteRenderer spriteRenderer = null;

        private Transform camTarget = null;

        private void Awake()
        {
            character = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            var characterData = character.CharacterData;
            var prefab = characterData.ModelPrefab;
            var modelInstance = Instantiate(prefab, transform);
            modelInstance.transform.localPosition = Vector3.zero;
            modelInstance.transform.localRotation = Quaternion.identity;
            modelInstance.transform.localScale = Vector3.one * characterData.Scale;
            camTarget = modelInstance.FindDeep("CamTarget");
            character.CamTarget = camTarget;

            animator = modelInstance.GetComponent<Animator>();
            spriteRenderer = modelInstance.GetComponent<SpriteRenderer>();

            character.OnEnterState += OnEnterState;
            character.OnExitState += OnExitState;
        }

        private void OnDestroy()
        {
            character.OnEnterState -= OnEnterState;
            character.OnExitState -= OnExitState;
        }

        private void Update()
        {
            if (character.MoveDirection.magnitude > 0)
            {
                spriteRenderer.flipX = character.MoveDirection.x < 0;
            }
        }

        private void OnEnterState(CharacterState state)
        {
            animator.Play(state.Type.ToString(), 0);
        }

        private void OnExitState(CharacterState state)
        {
            
        }
    }
}