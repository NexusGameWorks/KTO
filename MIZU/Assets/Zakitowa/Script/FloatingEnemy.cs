using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    public float moveSpeed = 2.0f; 
    public float floatAmplitude = 0.5f; 
    public float floatFrequency = 2.0f; 

    private Vector3 startPosition;

    void Start()
    {
      
        startPosition = transform.position;
    }

    void Update()
    {
       
        Vector3 horizontalMovement = new Vector3(moveSpeed * Time.deltaTime, 0, 0);

       
        Vector3 floatOffset = new Vector3(0, Mathf.Sin(Time.time * floatFrequency) * floatAmplitude, 0);

      
        transform.position = startPosition + floatOffset + horizontalMovement;

        startPosition.x += moveSpeed * Time.deltaTime;
    }
}
