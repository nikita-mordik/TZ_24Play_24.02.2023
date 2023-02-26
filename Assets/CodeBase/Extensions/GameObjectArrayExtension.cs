using UnityEngine;

namespace CodeBase.Extensions
{
    public static class GameObjectArrayExtension
    {
        public static GameObject GetSingle(this GameObject[] gameObjects) => 
            gameObjects[Random.Range(0, gameObjects.Length)];
    }
}