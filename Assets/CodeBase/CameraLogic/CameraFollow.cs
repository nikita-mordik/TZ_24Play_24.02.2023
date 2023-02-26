using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform following;
        [SerializeField] private float RotationAngleX;
        [SerializeField] private float RotationAngleY;
        [SerializeField] private float Distance;
        [SerializeField] private float OffsetY;
        
        [Header("Shake effect")]
        [SerializeField] private float ShakeDuration;
        [SerializeField] private float ShakeIntensity;

        private float shakeTimer;

        private void LateUpdate()
        {
            if (following == null) return;

            var rotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0);
            var position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
            position += GetShakeOffset();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void ShakeCamera() => 
            shakeTimer = ShakeDuration;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }

        private Vector3 GetShakeOffset()
        {
            if (shakeTimer <= 0f)
                return Vector3.zero;

            shakeTimer -= Time.deltaTime;
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            return new Vector3(x, y, z) * ShakeIntensity;
        }
    }
}