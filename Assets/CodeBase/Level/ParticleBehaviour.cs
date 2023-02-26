using CodeBase.Core;
using UnityEngine;

namespace CodeBase.Level
{
    public class ParticleBehaviour : MonoBehaviour
    {
        [SerializeField] private ParticleSystem warpEffect;

        private void Start()
        {
            EventListener.Instance.OnGameStart += ShowParticle;
            EventListener.Instance.OnGameEnd += HideParticle;
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnGameStart += ShowParticle;
            EventListener.Instance.OnGameEnd += HideParticle;
        }

        private void ShowParticle() => 
            warpEffect.Play();

        private void HideParticle() => 
            warpEffect.Stop();
    }
}