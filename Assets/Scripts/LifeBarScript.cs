using UnityEngine;
using System.Collections;

public class LifeBarScript : MonoBehaviour {

    public HeroeControl player;
    public HeroeControl enemy;
    private float life;

    public GameObject lifeBar;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
            life = player.Life;
        else
            life = enemy.Life;

        DecreadEnergy();
    }

    void DecreadEnergy()
    {
        float calcLife = life / 2000;
        SetLifeBar(calcLife);
    }

    void SetLifeBar(float myLife)
    {
        lifeBar.transform.localScale = new Vector2(myLife * 14.5f, 1.5f);
    }
}
