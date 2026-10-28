using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groundObstacles; 
    public GameObject[] airObstacles;   
    public GameObject coinPrefab;

    public float groundY = -1.57f;      
    public float lowBirdY = -1.3f;      
    public float highBirdY = -0.7f;     
    public float minDelay = 1.2f;
    public float maxDelay = 2.2f;
    [Range(0f, 1f)] public float airChance = 0.25f;
    [Range(0f, 1f)] public float coinChance = 0.35f;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = Random.Range(minDelay, maxDelay);

        float x = transform.position.x;

        if (Random.value < airChance)
        {
            float y = Random.value < 0.5f ? lowBirdY : highBirdY;
            Spawn(airObstacles, new Vector3(x, y, 0f));
        }
        else
        {
            Spawn(groundObstacles, new Vector3(x, groundY, 0f));
        }

        
        if (Random.value < coinChance)
        {
            float y = Random.value < 0.5f ? groundY + 0.4f : 0.6f;
            Instantiate(coinPrefab, new Vector3(x + 2.5f, y, 0f), Quaternion.identity);
        }
    }

    void Spawn(GameObject[] options, Vector3 pos)
    {
        GameObject prefab = options[Random.Range(0, options.Length)];
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
