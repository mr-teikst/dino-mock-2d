using UnityEngine;


public class ParallaxLayer : MonoBehaviour
{
    public float speed = 1f;     

    float width;                 
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        width = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (startPos.x - transform.position.x >= width)
            transform.position += Vector3.right * width;
    }
}