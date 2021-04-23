using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン制御用のスクリプトクラス
/// </summary>
public class SceneMove : MonoBehaviour
{
    /// <summary>遷移先のシーン名</summary>
    public string sceneName { get; set; } = "main";

    /// <summary>
    /// 次のシーンへ遷移する
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
