using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    private GameObject _playerHero, _enemyHero;
    public GameObject PlayerSpawn, EnemySpawn;
    public LifeBarScript PlayerBar, EnemyBar;
    public Text battleOverText;
    public SpawnTime spawnTime;
    private HeroeControl _playerHeroe, _enemyHeroe;


    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            _playerHero = GameObject.Find("PlayerHero");
            GameObject Player=Instantiate(PlayerSpawn, _playerHero.transform);
            PlayerBar.player = Player.GetComponent<HeroeControl>();
            _playerHeroe = Player.GetComponent<HeroeControl>();
            Player.GetComponent<HeroeControl>().battleOverText = battleOverText;
            //_playerHero.SetActive(true);

            _enemyHero = GameObject.Find("EnemyHero");
            GameObject Enemy = Instantiate(EnemySpawn, _enemyHero.transform);
            Enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
            Enemy.GetComponent<HeroeControl>().TagSearch = "Player";
            _enemyHeroe = Enemy.GetComponent<HeroeControl>();
            Enemy.layer = 9;
            EnemyBar.enemy = Enemy.GetComponent<HeroeControl>();
            Enemy.GetComponent<HeroeControl>().battleOverText = battleOverText;
            //_enemyHeroe.SetActive(true);
            //Enemy.transform.tag = "Enemy";

            if (Player.GetComponent<HeroeControl>().state.type == Enemy.GetComponent<HeroeControl>().state.type) {
                Enemy.GetComponent<SpriteRenderer>().color = new Color32(131,131,131,255);
            }
        }
    }

    private void Update()
    {
        if (_playerHeroe.Life <= 0)
        {
            spawnTime.EndSpawn();
            _playerHeroe.Life = 0;
            _playerHeroe.Dead();

        }
        else if (_enemyHeroe.Life <= 0) {
            spawnTime.EndSpawn();
            _enemyHeroe.Life = 0;
            _enemyHeroe.Dead();
        }
    }
}
