using CodeBase.Core;
using CodeBase.Enums;
using CodeBase.ObjectPooling;
using UnityEngine;

namespace CodeBase.Level
{
    public class PlatformGeneration : MonoBehaviour
    {
        [SerializeField] private Transform lastPlatform;

        private void Start()
        {
            EventListener.Instance.OnSpawnPlatform += SpawnPlatform;
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnSpawnPlatform -= SpawnPlatform;
        }

        private void SpawnPlatform()
        {
            GameObject platformObject = ObjectPooler.Instance.GetObjectFromPool(ObjectType.Platform);
            var platform = platformObject.GetComponent<Platform>();
            platform.transform.SetParent(null);
            var zPosition = lastPlatform.position.z + lastPlatform.lossyScale.z;
            var position = new Vector3(lastPlatform.position.x, lastPlatform.position.y, zPosition);
            platform.SetPosition(position);
            lastPlatform = platform.transform;
        }
    }
}