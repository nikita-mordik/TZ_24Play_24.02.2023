using System;

namespace CodeBase.Core
{
    public class EventListener : MonoSingleton<EventListener>
    {
        private Action<int> onAddPoint;
        private Action onGameStart;
        private Action onSpawnPlatform;
        private Action onCollideWithWall;
        private Action onGameEnd;

        public event Action<int> OnAddPoint
        {
            add => onAddPoint += value;
            remove => onAddPoint -= value;
        }
        public event Action OnGameStart
        {
            add => onGameStart += value;
            remove => onGameStart -= value;
        }
        public event Action OnSpawnPlatform
        {
            add => onSpawnPlatform += value;
            remove => onSpawnPlatform -= value;
        }
        public event Action OnCollideWithWall
        {
            add => onCollideWithWall += value;
            remove => onCollideWithWall -= value;
        }
        public event Action OnGameEnd
        {
            add => onGameEnd += value;
            remove => onGameEnd -= value;
        }

        public void InvokeOnAddPoint(int point)
        {
            onAddPoint?.Invoke(point);
        }

        public void InvokeOnGameStart()
        {
            onGameStart?.Invoke();
        }

        public void InvokeOnSpawnPlatform()
        {
            onSpawnPlatform?.Invoke();
        }

        public void InvokeOnCollideWithWall()
        {
            onCollideWithWall?.Invoke();
        }

        public void InvokeOnGameEnd()
        {
            onGameEnd?.Invoke();
        }
    }
}