using CodeBase.Core;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float delta = 2f;
        [SerializeField] private float limitXPos;
        
        private bool isGameEnd;

        public bool IsCanMove { get; set; }

        private void Start()
        {
            EventListener.Instance.OnGameEnd += OnGameEnd;
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnGameEnd -= OnGameEnd;
        }

        private void Update()
        {
            if (isGameEnd) return;
            
            if (!IsCanMove)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    EventListener.Instance.InvokeOnGameStart();
                    IsCanMove = true;
                }
                return;
            }
            
            Movement();
        }

        private void OnGameEnd() => 
            isGameEnd = true;

        private void Movement()
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            
            if (Input.touchCount > 0 && movementSpeed > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    var xPosition = transform.position.x + touch.deltaPosition.x * delta * Time.deltaTime;
                    transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
                    AreaLimitation();
                }
            }
        }
        
        private void AreaLimitation()
        {
            var positionX = Mathf.Clamp(transform.position.x, -limitXPos, limitXPos);
            transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        }
    }
}