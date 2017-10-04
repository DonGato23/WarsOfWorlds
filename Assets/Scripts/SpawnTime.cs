using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTime : MonoBehaviour {

    public float Duration;
    public float TimeNewSpawn;
    public float TimeElapsed;
    public Text TimeText;

    public BoxCollider2D[] SpawnButtons;

    public GameObject EnemySpawner;
    public float SpawnEnemySpeed;

    public Text GoldPlayer, GoldEnemy;
    public GameObject Player, Enemy;
    private int _playerGold, _enemyGold;
    private int _turn = 0;

	// Use this for initialization
	void Start () {
        _playerGold = Player.GetComponentInChildren<WaveSpawner>().Gold;
        _enemyGold = Enemy.GetComponentInChildren<WaveSpawner>().Gold;
        GoldPlayer.text = _playerGold.ToString();
        GoldEnemy.text = _enemyGold.ToString();
        TimeElapsed = 4f;
        Invoke("StartSpawn", 4f);
	}
	
	// Update is called once per frame
	void Update () {
        _playerGold = Player.GetComponentInChildren<WaveSpawner>().Gold;
        _enemyGold = Enemy.GetComponentInChildren<WaveSpawner>().Gold;
        GoldPlayer.text = _playerGold.ToString();
        GoldEnemy.text = _enemyGold.ToString();

        if (TimeElapsed > 0)
        {
            TimeElapsed -= Time.deltaTime;
        }
        if (TimeElapsed <= 0) {
            TimeElapsed = 0;
        }

        TimeText.text = Mathf.Round(TimeElapsed).ToString();
	}

    void StartSpawn() {

        for (int i = 0; SpawnButtons.Length > i; i++)
        {
            SpawnButtons[i].enabled = true;
        }
        InvokeRepeating("SpawnEnemy", SpawnEnemySpeed, SpawnEnemySpeed);
        TimeElapsed = Duration;
        Invoke("StopSpawn", Duration);
    }


    void SpawnEnemy() {
        EnemySpawner.GetComponentInChildren<WaveSpawner>().NewSpawn(Random.Range(0, EnemySpawner.GetComponentInChildren<WaveSpawner>().Spawns.Length));
    }

    void StopSpawn() {
        for (int i = 0; SpawnButtons.Length > i; i++)
        {
            SpawnButtons[i].enabled = false;
        }
        CancelInvoke("SpawnEnemy");
        TimeElapsed = TimeNewSpawn;
        Invoke("StartSpawn", TimeNewSpawn);
        _turn++;
        Enemy.GetComponentInChildren<WaveSpawner>().Gold = Enemy.GetComponentInChildren<WaveSpawner>().Gold + 100 + (25 * _turn);
        Player.GetComponentInChildren<WaveSpawner>().Gold = Player.GetComponentInChildren<WaveSpawner>().Gold + 100 + (25 * _turn);
    }

}
