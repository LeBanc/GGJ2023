using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public Character character;
    private Image image;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = character.image;
    }
}
