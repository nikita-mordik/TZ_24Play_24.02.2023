using System;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    [Serializable]
    public struct ObjectInfo
    {
        public ObjectType ObjectType;
        public GameObject[] ObjectPrefabs;
        public int ObjectCount;
    }
}