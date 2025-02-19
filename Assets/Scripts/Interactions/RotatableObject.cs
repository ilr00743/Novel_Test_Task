using UnityEngine;

namespace Interactions
{
    public class RotatableObject : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 0.2f;
        private Vector2 _lastTouchPosition;
        private bool _isDragging;

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#endif

#if UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();
#endif
        }

        void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _lastTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }

            if (_isDragging)
            {
                Vector2 delta = (Vector2)Input.mousePosition - _lastTouchPosition;
                RotateObject(delta);
                _lastTouchPosition = Input.mousePosition;
            }
        }

        void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _isDragging = true;
                    _lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved && _isDragging)
                {
                    Vector2 delta = touch.position - _lastTouchPosition;
                    RotateObject(delta);
                    _lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _isDragging = false;
                }
            }
        }

        void RotateObject(Vector2 delta)
        {
            float rotationX = delta.y * _rotationSpeed;
            float rotationY = -delta.x * _rotationSpeed;

            transform.Rotate(rotationX, rotationY, 0, Space.World);
        }
    }
}