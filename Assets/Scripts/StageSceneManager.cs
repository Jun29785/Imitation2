using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class StageSceneManager : MonoBehaviour
{
    public static StageSceneManager Instance;

    [Header("Current Scene")]
    private SceneType CurrentScene;

    private void Awake()
    {
        CurrentScene = SceneType.Main;
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        switch (CurrentScene)
        {
            case SceneType.Start:
                CurrentScene = SceneType.Main;
                break;
            case SceneType.Main:
                CurrentScene = SceneType.Stage_1;
                break;
            case SceneType.Stage_1:
                CurrentScene = SceneType.Stage_2;
                break;
            case SceneType.Stage_2:
                CurrentScene = SceneType.Finish;
                break;
            case SceneType.Finish:
                CurrentScene = SceneType.Main;
                break;
        }
    }
}
