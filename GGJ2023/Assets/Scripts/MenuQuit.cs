using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuQuit : MenuAction
{
    public override void Action()
    {
        Application.Quit();
    }
}
