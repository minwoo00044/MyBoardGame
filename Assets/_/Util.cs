using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNum
{
    Start = 0,
    Lobby = 1,
    Game = 2,
}
public static class Util
{
    public static void SceneChange(SceneNum num)
    {
        SceneManager.LoadScene((int)num);
    }
}
