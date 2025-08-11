using UnityEngine;

namespace Introduction_ECVP
{    
    public class CubeSizeAnimation : MonoBehaviour
    {
        [SerializeField, Range(0, 10)]
        private float changeSizeSpeed = 5;
        [SerializeField, Range(0, 2)]
        private float minScale = 1f;
        [SerializeField, Range(0, 2)]
        private float maxScale = 1.2f;

        private Transform _myTransform;
        private bool _isIncreasing;
        private float _currentScale;

        private void Start()
        {
            _myTransform = transform;
            _isIncreasing = true;
        }

        private void Update()
        {
            // Calculate how much to change the scale this frame
            float changeAmount = changeSizeSpeed * Time.deltaTime;

            // Update the current scale depending on whether we are growing or shrinking
            if (_isIncreasing)
            {
                _currentScale += changeAmount;
            }
            else
            {
                _currentScale -= changeAmount;
            }

            // Clamp the scale to its min/max limits and reverse direction if a limit is reached
            if (_currentScale > maxScale)
            {
                _currentScale = maxScale;
                _isIncreasing = false; // start shrinking
            }
            else if (_currentScale < minScale)
            {
                _currentScale = minScale;
                _isIncreasing = true; // start growing
            }

            // Apply the updated scale uniformly to the object
            Vector3 newSize = new Vector3(_currentScale, _currentScale, _currentScale);
            _myTransform.localScale = newSize;
        }
    }
}