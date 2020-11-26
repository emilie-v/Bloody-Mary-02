using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaryDialogue
{
    public string name;

    [TextArea(1, 7)]
    public string [] sentences;
}
