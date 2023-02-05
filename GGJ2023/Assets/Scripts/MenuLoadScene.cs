using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadScene : MenuAction
{
    public string sceneName;

    public override void Action()
    {
        SceneManager.LoadScene(sceneName);
    }
}
