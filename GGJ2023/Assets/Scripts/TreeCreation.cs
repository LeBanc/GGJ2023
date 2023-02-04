using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreation : MonoBehaviour
{
    [Range(2, 10)]
    public int treeLevel = 2;
    public GameObject frame;
    public GameObject parents;
    public Canvas canvas;

    private Vector2 imageSize = new Vector2(300, 300);
    private float screenWidth;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = canvas.GetComponent<RectTransform>().rect.width;

        GameObject child = Object.Instantiate(frame, canvas.transform);
        child.GetComponent<RectTransform>().anchoredPosition = new Vector3(screenWidth/2,135,0);


        for(int i = 1; i < treeLevel;i++)
        {
            float x = screenWidth / (int)Mathf.Pow(2,i);

            for(int j = 0; j < (int)Mathf.Pow(2,i)-1; j++)
            {
                Debug.Log(i + "," + j);
                parents = Object.Instantiate(parents, canvas.transform);
                parents.GetComponent<RectTransform>().anchoredPosition = new Vector3((j+1)*x, i*256 + 135, 0);
            }
        }


        /*
        for(int i=1;i < treeLevel;i++)
        {
            for(int j = 0; j < Mathf.Pow(2, i);j++)
            {
                float x = -Mathf.Pow(2, treeLevel - 2) - (i-1) + Mathf.Pow(2, treeLevel - i) * j;
                Debug.Log("" + i + "/" + j + " : " + -Mathf.Pow(2, treeLevel - 2) + " | " + -(i-1) + " | " + Mathf.Pow(2, treeLevel - i) * j);
                Object.Instantiate(frame, new Vector3(2 * imageSize.x*x, 2*imageSize.y*i,0), Quaternion.identity);
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
