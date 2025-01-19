using UnityEngine;

public class T_YPlayer : MonoBehaviour
{
    public float speed = 5.0f; 

    void Update()
    {
       
        float horizontal = Input.GetAxis("Horizontal");
        
        float vertical = Input.GetAxis("Vertical");

      
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
