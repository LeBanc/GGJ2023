using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterObject", order = 1)]
public class Character : ScriptableObject
{
    public Sprite image;
    public Character parent1;
    public Character parent2;
}
