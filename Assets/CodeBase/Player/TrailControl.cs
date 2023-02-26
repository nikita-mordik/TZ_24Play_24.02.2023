using UnityEngine;

namespace CodeBase.Player
{
    public class TrailControl : MonoBehaviour
    {
        [SerializeField] private Transform blocksHolder;
        [SerializeField] private TrailRenderer trailRenderer;

        private void LateUpdate()
        {
            if (blocksHolder.childCount == 0)
            {
                trailRenderer.enabled = false;
            }
        }
    }
}