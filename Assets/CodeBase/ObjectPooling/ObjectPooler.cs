using System.Collections.Generic;
using CodeBase.Core;
using CodeBase.Enums;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        [SerializeField] private List<ObjectInfo> objectInfos;

        private Dictionary<ObjectType, Pool> pools;

        protected override void Awake()
        {
            base.Awake();
            InitializePool();
        }

        private void InitializePool()
        {
            pools = new Dictionary<ObjectType, Pool>();
            var emptyGO = new GameObject();

            foreach (var objectInfo in objectInfos)
            {
                var container = Instantiate(emptyGO, transform, false);
                container.name = objectInfo.ObjectType.ToString();

                pools[objectInfo.ObjectType] = new Pool(container.transform);

                for (int i = 0; i < objectInfo.ObjectCount; i++)
                {
                    var go = InstantiateObject(objectInfo.ObjectType, container.transform);
                    pools[objectInfo.ObjectType].Objects.Enqueue(go);
                    
                }
            }
            
            Destroy(emptyGO);
        }

        private GameObject InstantiateObject(ObjectType type, Transform parent)
        {
            var go = Instantiate(objectInfos.Find(
                x => x.ObjectType == type)
                .ObjectPrefabs
                .GetSingle(),
                parent);
            go.SetActive(false);
            return go;
        }

        /// <summary>
        /// Get gameObject from Pool
        /// </summary>
        /// <param name="type">Type object which will be get from pool</param>
        /// <returns></returns>
        public GameObject GetObjectFromPool(ObjectType type)
        {
            var obj = pools[type].Objects.Count > 0
                               ? pools[type].Objects.Dequeue()
                               : InstantiateObject(type, pools[type].Container);
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Return gameObject to Pool
        /// </summary>
        /// <param name="gameObject">GameObject which will be returned to pool</param>
        public void BackObjectToPool(GameObject gameObject)
        {
            pools[gameObject.GetComponent<IPoolObject>().Type].Objects.Enqueue(gameObject);
            gameObject.transform.position = pools[gameObject.GetComponent<IPoolObject>().Type].Container.position;
            gameObject.transform.SetParent(transform);
            gameObject.SetActive(false);
        }
    }
}