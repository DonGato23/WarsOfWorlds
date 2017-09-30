using UnityEngine;
using System.Collections;

public class LifeControllerPlayer : MonoBehaviour {

    public float _currentLife;

    public void Life(float atk,float def)
    {
        _currentLife -= atk/def;
    }

    void OnEnable()
    {
        LifeEvent.OnSumLifePlayer += SumLife;
    }

    void OnDisable()
    {
        LifeEvent.OnSumLifePlayer -= SumLife;
    }

    void SumLife(float atk, float def)
    {
        Life(atk,def);
    }
}
