using CodeBase.Enums;
using CodeBase.ObjectPooling;
using UnityEngine;

namespace CodeBase.Level
{
    public class DissolveParticle : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ObjectType objectType;
        public ObjectType Type => objectType;
    }
}