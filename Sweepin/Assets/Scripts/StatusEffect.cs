using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "Scriptable Objects/Status Effect")]
public class StatusEffect : ScriptableObject
{
    public string statusEffectName;
    public string description;
    public SpriteRenderer sprite;


}
