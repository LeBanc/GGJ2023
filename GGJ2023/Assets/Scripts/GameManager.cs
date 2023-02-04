using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject results;
    public float resultDelay = 2.0f;
    private int count = 0;

    public void CheckResults()
    {
        Frame[] frames =  FindObjectsByType<Frame>(FindObjectsSortMode.None);
        bool result = true;

        foreach(Frame f in frames)
        {
            if (f.CheckCharacter())
            {
                f.ValidFrame();
            }
            else
            {
                result = false;
            }
        }

        if (result)
        {
            string t = "";
            switch (count)
            {
                case 0:
                    t = "S";
                    break;
                case 1:
                case 2:
                    t = "A";
                    break;
                case 3:
                case 4:
                    t = "B";
                    break;
                case 5:
                case 6:
                    t = "C";
                    break;
                default:
                    t = "D";
                    break;
            }
            results.GetComponent<TMP_Text>().text = t;
            StartCoroutine(WinAnimation());
        }
        else
        {
            count++;
        }
    }

    private IEnumerator WinAnimation()
    {
        yield return new WaitForSeconds(resultDelay);
        results.SetActive(true);
        yield break;
    }
}
