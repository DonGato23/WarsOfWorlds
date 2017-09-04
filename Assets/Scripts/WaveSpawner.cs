using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
[System.Serializable]
public class Wave
{
	public GameObject[] EnemyPrefabs;
	public float MinSpawnTime, MaxSpawnTime;
   // public int MinAmount;
    //public int MaxAmount;
    public float WaveDuration;
    //public int IncreaseAmountToCreate;
}
*/
public class WaveSpawner : MonoBehaviour {

    //public Wave[] Waves;
    public GameObject[] Spawns;
    public float MaxPositionOffset = 3f;
    public float MinPositionOffset = -3f;
   // private static int waveIndex = -1;
    //public bool IsEnemy;


    void Start()
    {
        NewSpawn(GetPrefab(0));

        //if (!StageManager.GameOver)
            //StartSimpleWaveGame();
    }
    /*
        void StartSimpleWaveGame()
        {
            //CancelInvoke("Spawn");
            //if (!StageManager.GameOver)

                if (WaveIndex + 1 < Waves.Length)
                    WaveIndex++;
                var wave = Waves[WaveIndex];
                //ObjectPool.Instance.IncreaseAmountToCreate(wave.IncreaseAmountToCreate);

                StartWave(wave);


                Invoke("StartSimpleWaveGame", wave.WaveDuration);


        }
        */
    /*
    void StartWave(Wave wave)
    {
        //int amountToSpawn = Random.Range(wave.MinAmount, wave.MaxAmount);
        InvokeRepeating("Spawn", 0,Random.Range( wave.MinSpawnTime,wave.MaxSpawnTime));
       for (int i = 0; i < amountToSpawn; i++)
        {
            Invoke("Spawn",Random.Range(0,wave.SpawnTime));
        }}
        */

    /*
    void Spawn()
    {
        var currentWave = Waves[WaveIndex];
        if (currentWave.EnemyPrefabs.Length > 0)
        {
            int index = Random.Range(0, currentWave.EnemyPrefabs.Length);
            //TODO: Elegir que enemigos spawnear en cada wave
            GameObject enemyPrefab = currentWave.EnemyPrefabs[index];
			GameObject enemy = ObjectPool.Instance.GetFromPool (enemyPrefab);
            if (enemy != null)
            {
                enemy.transform.tag = transform.tag;
                if (IsEnemy) {
                    enemy.GetComponent<CharacterScript>().TagSearch = "Player";
                    enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
                    enemy.gameObject.layer = 9;
                    enemy.transform.rotation = new Quaternion(enemy.transform.rotation.x, -180f, enemy.transform.rotation.z, enemy.transform.rotation.w);
                }
                enemy.transform.position = transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset);
            }
			//Instantiate(enemyPrefab, transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset), Quaternion.identity);
		}
        else
        {
            Debug.LogError("Wave Spawner :: There are no enemies in the Enemy Prefab Array!!");
        }
    }
    */

    GameObject GetPrefab(int index) {
        return Spawns[index];
    }


    public void NewSpawn(GameObject prefab) {

        //spawn.transform.position = transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset);
        float Yrot = 0;
        if (transform.rotation.y > 0) {
            Yrot = 180f;
        }
        GameObject spawn = Instantiate(prefab, transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset), new Quaternion(prefab.transform.rotation.x, Yrot, prefab.transform.rotation.z, prefab.transform.rotation.w));
        spawn.transform.tag = transform.tag;
        spawn.GetComponent<CharacterScript>().TagSearch = GetComponent<CharacterScript>().TagSearch;
        spawn.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        spawn.layer = gameObject.layer;
        //spawn.transform.rotation = new Quaternion(spawn.transform.rotation.x, transform.rotation.y, spawn.transform.rotation.z, spawn.transform.rotation.w);
    }

}
