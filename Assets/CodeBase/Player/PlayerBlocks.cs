using System.Collections;
using CodeBase.Enums;
using CodeBase.ObjectPooling;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBlocks : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform blocksHolder;
        [SerializeField] private Transform lastBlock;
        [SerializeField]private LayerMask pickedLayer;

        private bool canAdd;

        public bool CanAdd => canAdd;

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.magenta;
            Gizmos.DrawSphere(transform.position,0.2f);
        }

        private void FixedUpdate()
        {
            canAdd = Physics.CheckSphere(transform.position, 0.2f, pickedLayer);
        }

        public void AddBlock(Transform block)
        {
            block.parent = blocksHolder;
            Vector3 position = GetCubePosition();
            block.position = position;
            StartCoroutine(ShowParticle(position));
            lastBlock = block.transform;
            playerAnimator.Jump();
            MovePlayerUp();
        }

        private Vector3 GetCubePosition()
        {
            return new Vector3(blocksHolder.position.x, 
                lastBlock.position.y + lastBlock.lossyScale.y, 
                  blocksHolder.position.z);
        }

        private void MovePlayerUp()
        {
            var position = new Vector3(blocksHolder.localPosition.x,
                lastBlock.localPosition.y + playerTransform.localScale.y,
                blocksHolder.localPosition.z);
            playerTransform.localPosition = position;
        }

        private IEnumerator ShowParticle(Vector3 position)
        {
            ObjectPooler objectPooler = ObjectPooler.Instance;
            GameObject particle = objectPooler.GetObjectFromPool(ObjectType.Particle);
            particle.transform.position = position;
            particle.transform.SetParent(null);
            yield return new WaitForSeconds(3f);
            objectPooler.BackObjectToPool(particle);
        }
    }
}