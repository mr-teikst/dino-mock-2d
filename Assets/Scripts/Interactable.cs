using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    public float speed = 4f;        
    public float destroyX = -8f;    

    protected virtual void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + " touched " + other.name + " (tag: " + other.tag + ")");
        if (other.CompareTag("Player"))
            Interact(other.gameObject);
    }

    protected abstract void Interact(GameObject player);
}
