using CodeBase.Core;
using UnityEngine;

namespace CodeBase.Level
{
    public class DestroyOnStart : MonoBehaviour
    {
        private void Start()
        {
            EventListener.Instance.OnGameStart += OnGameStart;
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnGameStart -= OnGameStart;
        }

        private void OnGameStart() => 
            Destroy(gameObject, 5f);
    }
}