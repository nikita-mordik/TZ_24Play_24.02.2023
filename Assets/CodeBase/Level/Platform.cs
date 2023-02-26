using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ObjectPooling;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Level
{
    public class Platform : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ObjectType objectType;
        [SerializeField] private float duration;
        [SerializeField] private List<Transform> cubeSpawnPoints;
        [SerializeField] private bool isInBeginGame;

        public ObjectType Type => objectType;

        private void Start()
        {
            if (!isInBeginGame) return;
            
            SpawnBlocks();
        }

        public void SetPosition(Vector3 to)
        {
            transform.DOMove(to, duration);
            SpawnBlocks();
        }

        private void SpawnBlocks()
        {
            foreach (Transform spawnPoint in cubeSpawnPoints)
            {
                GameObject blockObject = ObjectPooler.Instance.GetObjectFromPool(ObjectType.Block);
                blockObject.transform.position = spawnPoint.position;
                blockObject.transform.SetParent(spawnPoint);
            }
        }
    }
}