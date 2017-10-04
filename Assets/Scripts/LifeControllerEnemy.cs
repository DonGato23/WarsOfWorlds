using UnityEngine;
using System.Collections;

public class LifeControllerEnemy : MonoBehaviour {

    public float _currentLife;

    public void Life(float atk,float def)
    {
        _currentLife -= atk/def;
    }

    void OnEnable()
    {
        LifeEvent.OnSumLifeEnemy += SumLife;
    }

    void OnDisable()
    {
        LifeEvent.OnSumLifeEnemy -= SumLife;
    }

    void SumLife(float atk,float def)
    {
        Life(atk,def);
    }
}
