using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawn : MonoBehaviour {

    public GameObject Spawner;
    public int Index;
    private SpriteRenderer _spriteRenderer;
    public GameObject Panel;
    public Text AtkText, DefText, CostText;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.Portrait;
    }

    private void OnMouseDown()
    {
        if(SpawnTime.isSpawn)
            Spawner.GetComponentInChildren<WaveSpawner>().NewSpawn(Index);
    }

    private void OnMouseOver()
    {
        Panel.SetActive(true);
        AtkText.text = "ATK " + Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.minatk.ToString() + " - " + Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.maxatk.ToString();
        DefText.text = "DEF " + Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.mindef.ToString() + " - " + Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.maxdef.ToString();
        CostText.text = "COST " + Spawner.GetComponentInChildren<WaveSpawner>().Spawns[Index].GetComponent<CharacterScript>().State.GoldCost.ToString();
    }

    private void OnMouseExit()
    {
        Panel.SetActive(false);
    }

}
