using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string sceneToLoad = "";

    public void LoadTheLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
