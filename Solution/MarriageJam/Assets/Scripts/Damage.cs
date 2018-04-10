using System;
using UnityEngine;

[Serializable]
public class Damage
{
    public string Name = "default";
    public int Value = 1;
    public bool Knockout = false;
    public bool AttackFromRight = false;
}
