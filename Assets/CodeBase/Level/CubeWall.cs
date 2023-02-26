using CodeBase.Blocks;
using CodeBase.CameraLogic;
using CodeBase.Core;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Level
{
    public class CubeWall : MonoBehaviour
    {
        private bool isCollide;
        private CameraFollow cameraFollow;

        private void OnEnable()
        {
            isCollide = false;
        }

        private void Start()
        {
            cameraFollow = Camera.main.GetComponent<CameraFollow>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isCollide) return;

            if (collision.gameObject.TryGetComponent<PlayerBody>(out var playerBody))
            {
                playerBody.StopPlayer();
                isCollide = true;
            }
            
            if (collision.gameObject.TryGetComponent<Block>(out var block))
            {
                block.UnParentBlock();
                StartCoroutine(block.BackToPool());
                EventListener.Instance.InvokeOnCollideWithWall();
                isCollide = true;
            }
            
            cameraFollow.ShakeCamera();
#if !UNITY_EDITOR
            Vibratior.Vibrate();
#endif
        }
    }
}