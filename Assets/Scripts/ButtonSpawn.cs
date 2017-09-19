using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawn : MonoBehaviour {

    public GameObject Spawner;
    public int Index;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.Portrait;
    }

    private void OnMouseDown()
    {
        Spawner.GetComponentInChildren<WaveSpawner>().NewSpawn(Index);
    }

}
