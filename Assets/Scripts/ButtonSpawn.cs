using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawn : MonoBehaviour {

    public GameObject Spawner;
    public int Index;
    public GameObject Panel;
    public Text AtkText, DefText, CostText;
    

    private SpriteRenderer _spriteRenderer;
    private States _charState;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.Portrait;
        _charState = Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State;
    }

    private void OnMouseOver()
    {
        AtkText.text = "ATK " + _charState.minatk + "-" + _charState.maxatk;
        DefText.text = "DEF " + _charState.mindef + "-" + _charState.maxdef;
        CostText.text = "COST " + _charState.GoldCost;
        Panel.SetActive(true);
    }

    private void OnMouseExit()
    {
        Panel.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!SpawnTime.isSpawn)
        {
            Spawner.GetComponentInChildren<WaveSpawner>().NewSpawn(Index);
        }
    }
}
