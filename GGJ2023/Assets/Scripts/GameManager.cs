using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject results;
    public GameObject fadeout;
    public float resultDelay = 2.0f;
    public float waitForEnd = 1.0f;
    private int count = 0;
    public string sceneToLoad;

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

    protected virtual IEnumerator WinAnimation()
    {
        yield return new WaitForSeconds(resultDelay);
        results.SetActive(true);
        yield return new WaitForSeconds(waitForEnd);
        Image im = fadeout.GetComponent<Image>();
        im.color = new Color(0f, 0f, 0f, 0f);
        fadeout.SetActive(true);
        for (float alpha = 0.0f;alpha<1.0f;alpha+=0.05f)
        {
            im.color = new Color(0f, 0f, 0f, alpha);
            yield return new WaitForFixedUpdate();
        }
        im.color = new Color(0f, 0f, 0f, 1f);
        yield return new WaitForSeconds(waitForEnd);
        SceneManager.LoadScene(sceneToLoad);
        yield break;
    }
}
