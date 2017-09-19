using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject _playerHero,_enemyHeroe;
    public GameObject PlayerSpawn, EnemySpawn;


	// Use this for initialization
	void Awake () {
        _playerHero = GameObject.Find("PlayerHero");
        Instantiate(PlayerSpawn, _playerHero.transform);

        _enemyHeroe= GameObject.Find("EnemyHero");
        GameObject Enemy = Instantiate(EnemySpawn, _enemyHeroe.transform);
        Enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
        Enemy.GetComponent<HeroeControl>().TagSearch = "Player";
        Enemy.layer=9;
        //Enemy.transform.tag = "Enemy";
    }

}
