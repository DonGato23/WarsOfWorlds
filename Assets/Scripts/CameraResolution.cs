using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour {

    public int Width, Height;

    private void Awake()
    {
        Screen.SetResolution(Width, Height, true);
        
    }
}
