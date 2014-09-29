using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is the "god" class, and controls all game setup code such as object spawning, and keeping track of score.

public class GameController : MonoBehaviour {

    //This class spawns the new enemies and informs the player of where they are, and controls all major game events.

    public GameObject enemyPrefab;
    public GameObject player;

    private PlayerShootController _playerShootController;

    private float platform_gaps = 3.0f;
    private int current_level = 0;
    private float spawn_offset = 7.0f; //how far ahead to look before spawning objects

    private static List<GameObject> enemies;

    public GameObject destructibleBlock;
    public GameObject seeThroughBlock;
    public GameObject solidBlock;

    public const string SOLID_PLATFORM = "solid platform";
    public const string SEE_THROUGH_PLATFORM = "see through platform";
    public const string DESTRUCTIBLE_PLATFORM = "destructible platform";

    private GameObject[] blockPrefabs;
    private string[] enemyTypes = new string[] {EnemyModel.AUTO_AIM, EnemyModel.HOMING_MISSILE, EnemyModel.STRAIGHT_SHOOTER};
    private Color[] enemyColors = new Color[] { Color.magenta, Color.red, Color.cyan };

    public GUIText heightText;
    private float _maxHeight = 0.0f;

    public GUIText killCountText;
    private static int _killCount = 0;

    public GUIText GameOverMessage;
    private bool _isGameOver = false;
    private int _gameOverCooldown = 80;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();

        _playerShootController = player.GetComponent<PlayerShootController>();
        blockPrefabs = new GameObject[] { destructibleBlock, seeThroughBlock, solidBlock };
        SetUIText();
	}
	
	// Update is called once per frame
	void Update () {
        if (_isGameOver)
        {
            _gameOverCooldown--;
            if (_gameOverCooldown < 0 && Input.GetMouseButton(0))
            {
                Application.LoadLevel("game");
            }
            return;
        }

        //player's y-position triggers enemy generation
        if (player.transform.position.y > current_level * platform_gaps - spawn_offset)
        {
            SpawnEnemy();
        }

        if (player.transform.position.y > _maxHeight)
        {
            _maxHeight = player.transform.position.y;
            SetUIText();
        }
        updateKillCountText();
        checkForPlayerDeath();
	}

    private void checkForPlayerDeath()
    {
        if (player.GetComponent<PlayerModel>().health < 0)
        {
            _isGameOver = true;
            GameOverMessage.gameObject.SetActive( true );
            player.GetComponent<PlayerModel>().isAlive = false;
        }
    }

    private void SetUIText()
    {
        heightText.text = "Height: " + Mathf.Floor(_maxHeight);
    }

    private void SpawnEnemy()
    {
        current_level++;
        float x_pos;
        if (Random.value < 0.5f)
        {
            x_pos = -1.5f;
        }
        else
        {
            x_pos = 1.5f;
        }

        Vector3 spawnPosition = new Vector3(x_pos, current_level * platform_gaps, 0);
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, transform.rotation);
        enemies.Add(enemy);

        int enemyTypeIndex = Mathf.FloorToInt(Random.value * 3);

        enemy.GetComponent<EnemyModel>().enemyType = enemyTypes[enemyTypeIndex];
        enemy.GetComponent<EnemyController>().player = player;
        enemy.renderer.material.color = enemyColors[enemyTypeIndex];

        _playerShootController.enemies = enemies;

        int blockTypeIndex = Mathf.FloorToInt(Random.value * 3);

        SpawnPlatform(8, blockPrefabs[blockTypeIndex], spawnPosition + Vector3.down * 0.6f);

        //Spawn a shelter platform 40% of the time
        if (Random.value < 0.4f)
        {
            //generate a shelter platform between the enemy platforms 50/50 chance of it being destructible
            if (Random.value < 0.5f)
            {
                SpawnPlatform(6, destructibleBlock, new Vector3(0, current_level * platform_gaps + 0.5f * platform_gaps));
            }
            else
            {
                SpawnPlatform(6, solidBlock, new Vector3(0, current_level * platform_gaps + 0.5f * platform_gaps));
            }
        }
    }

    public void SpawnPlatform(int platformWidth, GameObject platformPrefab, Vector3 spawnPosition)
    {
        float platform_spacing = 0.25f;

        for (var i = 0; i < platformWidth; i++)
        {
            Vector3 position = spawnPosition + (i * platform_spacing - platformWidth * platform_spacing * 0.5f) * Vector3.right;
            GameObject platform = (GameObject)Instantiate(platformPrefab, position, Quaternion.identity);
        }
    }

    public static List<GameObject> GetEnemyList()
    {
        return enemies;
    }

    public static void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
        _killCount++;
    }

    private void updateKillCountText()
    {
        killCountText.text = "Kills: " + _killCount;
    }

    
}
