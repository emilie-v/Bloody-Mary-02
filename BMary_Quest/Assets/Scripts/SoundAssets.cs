using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour

{
    static SoundAssets _i;
    public static SoundAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            return _i;
        }
    }
    public AudioClip backButtonSound;
}
