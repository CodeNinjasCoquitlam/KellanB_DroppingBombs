using UnityEngine;
using TMPro;

public class gamemanager : MonoBehaviour
{
    private Spawner spawner;
    public GameObject title;
    private Vector2 screenBounds;

    public GameObject splash;

    [Header("Player")]
    public GameObject playerPrefab;
    private GameObject player;
    private bool gameStarted = false;

    [Header("Score")]
    public TMP_Text scoreText;
    public int pointsWorth = 1;
    private int score;
    void ResetGame() 
    {
        spawner.active = true;
        title.SetActive(false);

        splash.SetActive(false);

        scoreText.enabled = true;
        score = 0;

        scoreText.text = "Score: " + score.ToString();

        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        gameStarted = true;
    }
    private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        scoreText.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner.active = false;
        title.SetActive(true);
        splash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) 
        {
            if (Input.anyKeyDown) 
            {
                ResetGame();
            }
        }
        else 
        {
            if (!player) 
            {
                OnPlayerKilled();
            }
        }

        if (Input.anyKeyDown) 
        {
            spawner.active = true;
            title.SetActive(false);
        }
        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");
        
            foreach (GameObject bombObject in nextBomb) 
        { 
            if (bombObject.transform.position.y < (-screenBounds.y - 12)) 
            {
                if (gameStarted) 
                {
                    score += pointsWorth;
                    scoreText.text = "Score: " + score.ToString();
                }

                Destroy(bombObject);
            }
        }
    }
    void OnPlayerKilled() 
    {
        spawner.active = false;
        gameStarted = false;

        splash.SetActive(true);
    }
}
