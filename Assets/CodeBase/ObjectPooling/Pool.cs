using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    public class Pool
    {
        public Transform Container { get; private set; }

        public Queue<GameObject> Objects;

        public Pool(Transform container)
        {
            Container = container;
            Objects = new Queue<GameObject>();
        }
    }
}