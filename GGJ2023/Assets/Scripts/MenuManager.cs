using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : GameManager
{
    public Frame actionFrame;

    protected override IEnumerator WinAnimation()
    {
        if (actionFrame.currentCharItem.CompareTag("Quit"))
        {
            results.GetComponentInChildren<Image>().gameObject.SetActive(false);
            results.GetComponent<TMP_Text>().text = "Bye";
        }
        else
        {
            results.GetComponent<TMP_Text>().text = "Go";
        }
        yield return new WaitForSeconds(resultDelay);
        results.SetActive(true);
        yield return new WaitForSeconds(waitForEnd);
        Image im = fadeout.GetComponent<Image>();
        im.color = new Color(0f, 0f, 0f, 0f);
        fadeout.SetActive(true);
        for (float alpha = 0.0f; alpha < 1.0f; alpha += 0.05f)
        {
            im.color = new Color(0f, 0f, 0f, alpha);
            yield return new WaitForFixedUpdate();
        }
        im.color = new Color(0f, 0f, 0f, 1f);
        yield return new WaitForSeconds(waitForEnd);
        actionFrame.currentCharItem.GetComponent<MenuAction>().Action();
        yield break;
    }
}
