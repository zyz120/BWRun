using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController _instance;
    public static GameController GetInstance()
    {
        return _instance;
    }

    [HideInInspector]
    public bool _playerDead;

    public float _obsSpeed;
    public GameObject[] _obs_Prefabs;

    public Transform parent;
    public GameObject player_prefab;

    public ShaderChange[] shaders;
    public GameObject[] bounds;

    public Text scoreText;
    public BackgroundChange background;

    public GameObject rule;

    private float timer;
    private int score;
    private int obsCount;

    private bool _gameStart;

    private Vector2 bornPos;

    void Awake()
    {
        timer = 0;
        _obsSpeed = 9f;
        obsCount = 0;
        _instance = this;
        _playerDead = false;
        _gameStart = false;
        bornPos = transform.position + new Vector3(12.0f, 0.79f, 0);        
    }

    void Update()
    { 
        if(!_gameStart)
        {
            scoreText.text = "";
            return;
        }

        timer += Time.deltaTime;

        if(!_playerDead)
            score++;

        ProduceObstacles();

        UpdateScore();

        if(Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
        {
            if(_playerDead)
                RestartGame();
        }

    }

    public void StartGame()
    {
        _gameStart = true;
        rule.SetActive(false);   
    }

    public void ChangeWorld()
    {
        for (int i = 0; i < shaders.Length; i++)
        {
            if (shaders[i]._shouldSlideIn)
            {
                shaders[i]._shouldSlideIn = false;
                shaders[i]._shouldSlideOut = true;
            }
            else if (shaders[i]._shouldSlideOut)
            {
                shaders[i]._shouldSlideIn = true;
                shaders[i]._shouldSlideOut = false;
            }

            bounds[i].SetActive(!bounds[i].activeInHierarchy);
        }

    }

    void RestartGame()
    {
        GameObject player = Instantiate(player_prefab, new Vector3(-7f, 0.5f, 0f), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerController>()._isUpper = true;

        _playerDead = false;
        score = 0;
        _obsSpeed = 9f;

        for(int i = parent.childCount - 1; i >= 0;i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        shaders[0]._shouldSlideIn = false;
        shaders[0]._shouldSlideOut = true;
        shaders[1]._shouldSlideIn = true;
        shaders[1]._shouldSlideOut = false;
        bounds[0].SetActive(false);
        bounds[1].SetActive(true);

        background._changeToOne = true;

    }

    void UpdateScore()
    {
        scoreText.text = score + "";

        if (score >= 750)
        {
            background._changeToOne = false;
            background._changeToTwo = true;
        }
        if (score >= 1500)
        {
            background._changeToTwo = false;
            background._changeToThree = true;
        }

        if(score % 500 == 0)
        {
            _obsSpeed += 1f;
        }
    }

    void BornUpperObstacle(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, bornPos, Quaternion.identity) as GameObject;

        if(go.GetComponent<ObstacleInfo>() != null)
        {
            go.GetComponent<ObstacleInfo>()._isUpper = true;
        }

        go.transform.parent = parent;
    }

    void BornDownObstacle(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, new Vector2(24, 0) - bornPos, Quaternion.identity) as GameObject;
        go.transform.localScale = new Vector3(1, -1, 1);

        if (go.GetComponent<ObstacleInfo>() != null)
        {
            go.GetComponent<ObstacleInfo>()._isUpper = false;
        }

        go.transform.parent = parent;
    }

    void BornObstacle(GameObject prefab)
    {
        if(Random.Range(0, 1f) >= 0.5f)
        {
            BornUpperObstacle(prefab);
        }
        else
        {
            BornDownObstacle(prefab);
        }
    }

    void ProduceObstacles()
    {
        if (obsCount >= 8)
        {
            BornObstacle(_obs_Prefabs[2]);
            obsCount = 0;
            timer = 0f;
            return;
        }
        else if (obsCount == 4)
        {
            BornObstacle(_obs_Prefabs[1]);
            obsCount++;
            timer = -0.5f;
            return;
        }
        else if (timer >= 0.4f)
        {
            BornObstacle(_obs_Prefabs[Random.Range(0f, 1f) < 0.5f ? 3 : 0]);
            obsCount++;
            timer = 0;
            return;
        }

    }
}
