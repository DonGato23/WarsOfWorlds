using UnityEngine;
using System.Collections;
using System;

public class LifeEvent : MonoBehaviour {

    public static Action<float,float> OnSumLifePlayer;
    public static Action<float, float> OnSumLifeEnemy;

    public static void SumEnergyPlayer(float atk, float def)
    {
        if (OnSumLifePlayer != null)
            OnSumLifePlayer(atk,def);
    }

    public static void SumEnergyEnemy(float atk,float def)
    {
        if (OnSumLifeEnemy != null)
            OnSumLifeEnemy(atk,def);
    }
}
