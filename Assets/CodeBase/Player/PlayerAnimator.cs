using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;

        private readonly int jumpHash = Animator.StringToHash("Jump");

        public void Jump() => 
            playerAnimator.SetTrigger(jumpHash);
    }
}