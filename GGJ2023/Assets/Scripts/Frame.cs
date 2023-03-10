using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public Character trueChar;
    public CharacterItem currentCharItem;
    public static float animSpeed = 50;

    private Mask mask;
    private bool alreadyValid = false;

    private void Start()
    {
        mask = GetComponentInChildren<Mask>();

        if (currentCharItem != null)
        {
            currentCharItem.GetComponent<DragItem>().centerPoint = GetComponent<RectTransform>().anchoredPosition;
            currentCharItem.GetComponent<DragItem>().Lock();
            currentCharItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            alreadyValid = true;
        }
        else
        {
            mask.GetComponent<RectTransform>().sizeDelta = new Vector2(0, mask.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public bool CheckCharacter()
    {
        if(currentCharItem != null)
        {
            if (alreadyValid) return true;
            if (currentCharItem.character == trueChar)
            {
                StartCoroutine(ValidationAnimation());
                currentCharItem.GetComponent<DragItem>().Lock();
                alreadyValid = true;
                return true;
            }
        }
        return false;
    }

    public void ValidFrame()
    {
        GetComponent<Image>().raycastTarget = true;
    }

    private IEnumerator ValidationAnimation()
    {
        while (mask.GetComponent<RectTransform>().sizeDelta.x < 400)
        {
            mask.GetComponent<RectTransform>().sizeDelta += new Vector2(mask.GetComponent<RectTransform>().sizeDelta.x/5 + animSpeed * Time.fixedDeltaTime, 0);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
