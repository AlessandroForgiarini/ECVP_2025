using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Transform myTransform;

    // Variables used to move the gameobject toward a target
    public Transform targetTransform;
    public float movementSpeed;

    // Variables used to animate the size of the GameObject
    public float changeSizeSpeed = 1;
    public float minSize = 0.5f;
    public float maxSize = 2f;
    private bool isIncreasing = false;
    private float currentSize = 1;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        HandleMovement();
        HandleSize();
    }

    private void HandleMovement()
    {
        Vector3 myPosition = myTransform.position;
        Vector3 targetPosition = targetTransform.position;
        Vector3 direction = targetPosition - myPosition;
        Vector3 normalizedDirection = direction.normalized;
        myTransform.Translate(normalizedDirection *movementSpeed*Time.deltaTime);
    }

    private void HandleSize()
    {
        float changeAmount = changeSizeSpeed * Time.deltaTime;

        if (isIncreasing) 
        {
            currentSize = currentSize + changeAmount;
        } 
        else
        {
            currentSize = currentSize - changeAmount;
        }

        if(currentSize > maxSize)
        {
            currentSize = maxSize;
            isIncreasing = false;
        }

        if (currentSize < minSize)
        {
            currentSize = minSize;
            isIncreasing = true;
        }

        Vector3 newSize = new Vector3(currentSize, currentSize, currentSize);
        myTransform.localScale = newSize;
    }
}
