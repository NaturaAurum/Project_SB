using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using SB.GameLogic.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.GameLogic
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        [SerializeField]
        [Required]
        private CinemachineVirtualCamera virtualCam = null;

        [SerializeField]
        [Required]
        private CinemachineTargetGroup targetGroup = null;
        private CinemachineFramingTransposer transposer = null;

        private CinemachineBrain coreLogic = null;

        private List<CinemachineTargetGroup.Target> targetList = new List<CinemachineTargetGroup.Target>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            var comps = virtualCam.GetComponentPipeline();
            foreach (var comp in comps)
            {
                if (comp is CinemachineFramingTransposer transposer)
                {
                    this.transposer = transposer;
                }
            }
        }

        public void SetCharacter(CharacterBase character)
        {
            var camTarget = character.CamTarget;
            var target = new CinemachineTargetGroup.Target();
            target.target = camTarget;
            target.weight = 1f;
            targetList.Add(target);
            targetGroup.m_Targets = targetList.ToArray();
        }

        public void Shake(float power)
        {
            
        }
    }
}
