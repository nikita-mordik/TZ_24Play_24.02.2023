using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerRagdoll : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Rigidbody[] allPlayerRigidbody;

        private void Awake()
        {
            ChangePlayerRigidbodyState(true);
        }

        public void ChangePlayerRigidbodyState(bool state)
        {
            playerAnimator.enabled = state;
            
            for (int i = 0; i < allPlayerRigidbody.Length; i++)
            {
                allPlayerRigidbody[i].isKinematic = state;
            }
        }
    }
}