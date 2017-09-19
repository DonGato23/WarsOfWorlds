using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public GameObject[] Spawns;
    public float MaxPositionOffset = 3f;
    public float MinPositionOffset = -3f;
    public int Gold;

    GameObject GetPrefab(int index) {
        return Spawns[index];
    }

    public void NewSpawn(int index) {
        GameObject prefab=GetPrefab(index);
        //spawn.transform.position = transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset);
        float Yrot = 0;
        if (transform.rotation.y > 0) {
            Yrot = 180f;
        }
        if (prefab.GetComponent<CharacterScript>().State.GoldCost <= Gold)
        {
            Gold -= prefab.GetComponent<CharacterScript>().State.GoldCost;
            GameObject spawn = Instantiate(prefab, transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset), new Quaternion(prefab.transform.rotation.x, Yrot, prefab.transform.rotation.z, prefab.transform.rotation.w));
            spawn.transform.tag = LayerMask.LayerToName(gameObject.layer);
            spawn.GetComponent<CharacterScript>().TagSearch = GetComponent<HeroeControl>().TagSearch;
            spawn.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            spawn.layer = gameObject.layer;
            //spawn.transform.rotation = new Quaternion(spawn.transform.rotation.x, transform.rotation.y, spawn.transform.rotation.z, spawn.transform.rotation.w);
            //spawn.transform.GetChild(0).tag = spawn.tag;
            spawn.transform.GetChild(0).gameObject.layer = spawn.layer;
        }
    }

}
