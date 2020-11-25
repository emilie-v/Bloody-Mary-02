using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodPointsBar : MonoBehaviour
{
    public Slider bloodSlider;
    public Player player;

    public void Update()
    {
        bloodSlider.value = (float)player.currentBlood / (float)player.maxBlood;
    }
}
