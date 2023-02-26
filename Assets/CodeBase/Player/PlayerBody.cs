using CodeBase.Core;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBody : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerBody;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerRagdoll playerRagdoll;
        
        private bool isCollide;
        private int wallLayer;

        private void Start()
        {
            EventListener.Instance.OnCollideWithWall += OnCollideWithWall;

            wallLayer = LayerMask.NameToLayer("Wall");
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnCollideWithWall -= OnCollideWithWall;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((collision.transform.CompareTag("Ground") || collision.gameObject.layer == wallLayer)
                && !isCollide)
            {
                StopPlayer();
            }
        }

        public void StopPlayer()
        {
            StopPlayerMovement();
            EnableRagdoll();
            EventListener.Instance.InvokeOnGameEnd();
            isCollide = true;
        }

        private void OnCollideWithWall() => 
            playerBody.isKinematic = false;

        private void StopPlayerMovement() => 
            playerMovement.IsCanMove = false;

        private void EnableRagdoll() => 
            playerRagdoll.ChangePlayerRigidbodyState(false);
    }
}
