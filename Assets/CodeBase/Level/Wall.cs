using System.Collections;
using CodeBase.Core;
using CodeBase.ObjectPooling;
using UnityEngine;

namespace CodeBase.Level
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private GameObject platform;
        [SerializeField] private float delay;

        private bool isTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered) return;

            if (other.CompareTag("Player"))
            {
                EventListener.Instance.InvokeOnSpawnPlatform();
                if (platform != null)
                    StartCoroutine(BackToPool());
                isTriggered = true;
            }
        }
        
        private IEnumerator BackToPool()
        {
            yield return new WaitForSeconds(delay);
            ObjectPooler.Instance.BackObjectToPool(platform);
        }
    }
}