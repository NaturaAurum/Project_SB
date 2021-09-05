using System.Threading.Tasks;
using Cinemachine;
using SB.GameLogic.Command;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SB.GameLogic
{
    public class CameraShake : MonoBehaviour, ICommandListener
    {
        [SerializeField]
        [Required]
        private CinemachineVirtualCamera virtualCam = null;

        private CinemachineBasicMultiChannelPerlin noiseModule = null;

        private void Awake()
        {
            var comps = virtualCam.GetComponentPipeline();
            foreach (var comp in comps)
            {
                if (comp is CinemachineBasicMultiChannelPerlin noiseModule)
                {
                    this.noiseModule = noiseModule;
                }
            }
            
            CommandDispatcher.AddListener(this);
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
        }

        public void Listen(ICommand command)
        {
            if (command is CameraShakeCommand shake)
            {
                Shake(shake.Strength, shake.Time);
            }
        }

        public async void Shake(float noise, float time)
        {
            if (noiseModule == null)
                return;
            noiseModule.m_AmplitudeGain = noise;
            await Task.Delay((int) (time * 1000));
            noiseModule.m_AmplitudeGain = 0;
        }
    }
}