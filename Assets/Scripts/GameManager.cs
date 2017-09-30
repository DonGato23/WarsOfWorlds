using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private GameObject _playerHero, _enemyHeroe;
    public GameObject PlayerSpawn, EnemySpawn;
    public LifeBarScript PlayerBar, EnemyBar;
    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            _playerHero = GameObject.Find("PlayerHero");
            GameObject Player=Instantiate(PlayerSpawn, _playerHero.transform);
            PlayerBar.player = Player.GetComponent<HeroeControl>();
            //_playerHero.SetActive(true);

            _enemyHeroe = GameObject.Find("EnemyHero");
            GameObject Enemy = Instantiate(EnemySpawn, _enemyHeroe.transform);
            Enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
            Enemy.GetComponent<HeroeControl>().TagSearch = "Player";
            Enemy.layer = 9;
            EnemyBar.enemy = Enemy.GetComponent<HeroeControl>();
            //_enemyHeroe.SetActive(true);
            //Enemy.transform.tag = "Enemy";

            if (Player.GetComponent<HeroeControl>().state.type == Enemy.GetComponent<HeroeControl>().state.type) {
                Enemy.GetComponent<SpriteRenderer>().color = new Color32(131,131,131,255);
            }
        }
    }

}
