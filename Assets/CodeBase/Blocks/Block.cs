using System.Collections;
using CodeBase.Core;
using CodeBase.Enums;
using CodeBase.ObjectPooling;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Blocks
{
    public class Block : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Rigidbody blockBody;
        [SerializeField] private ObjectType objectType;
        [SerializeField] private float delay;

        private bool isCollideWithPlayer;
        private bool isGameEnd;

        private int pickedLayer;
        private int pickUpLayer;

        public ObjectType Type => objectType;

        private void Start()
        {
            pickUpLayer = LayerMask.NameToLayer("PickUp");
            pickedLayer = LayerMask.NameToLayer("Picked");
            EventListener.Instance.OnGameEnd += OnGameEnd;
        }
        
        private void OnDestroy()
        {
            EventListener.Instance.OnGameEnd -= OnGameEnd;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent<PlayerBlocks>(out var playerBlocks) && !isCollideWithPlayer)
            {
                if (!playerBlocks.CanAdd) return;
                ChangeConstrains(false);
                playerBlocks.AddBlock(transform);
                ChangeLayer(pickedLayer);
                AddPoint();
                isCollideWithPlayer = true;
            }
        }

        public void UnParentBlock() => 
            transform.parent = null;

        public IEnumerator BackToPool()
        {
            yield return new WaitForSeconds(delay);
            if (isGameEnd) yield break;
            ChangeLayer(pickUpLayer);
            ChangeConstrains(true);
            isCollideWithPlayer = false;
            ObjectPooler.Instance.BackObjectToPool(gameObject);
        }

        private void OnGameEnd() => 
            isGameEnd = true;

        private void ChangeLayer(int layer) => 
            gameObject.layer = layer;

        private void AddPoint() => 
            EventListener.Instance.InvokeOnAddPoint(1);

        private void ChangeConstrains(bool isFreeze)
        {
            switch (isFreeze)
            {
                case true:
                    blockBody.constraints = RigidbodyConstraints.FreezeAll;
                    break;
                case false:
                    blockBody.constraints = RigidbodyConstraints.FreezeRotation | 
                                            RigidbodyConstraints.FreezePositionX |
                                            RigidbodyConstraints.FreezePositionZ;
                    break;
            }
        }
    }
}