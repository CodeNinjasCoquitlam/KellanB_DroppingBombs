using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float delay = 2.0f;
    public bool active = false;
    public Vector2 delayRange = new Vector2(1, 2);

    private Vector2 screenBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetDelay();
        StartCoroutine(EnemyGenerator());

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemyGenerator() 
    {
        yield return new WaitForSeconds(delay);
        if(active) 
        {
            float randomX = Random.Range(-screenBounds.x, screenBounds.x);
            float spawnY = screenBounds.y + 1;
            Instantiate(bombPrefab, new Vector3(randomX, spawnY, 0), bombPrefab.transform.rotation);
            ResetDelay();
        }
        StartCoroutine(EnemyGenerator());
        
    }
    void ResetDelay()
    {
        delay = Random.Range(delayRange.x, delayRange.y);
    }
}
