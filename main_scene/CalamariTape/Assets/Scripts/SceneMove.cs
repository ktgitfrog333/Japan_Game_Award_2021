using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Const.Name;
using Const.Tag;

/// <summary>
/// シーン制御用のスクリプトクラス
/// </summary>
public class SceneMove : MonoBehaviour
{
    /// <summary>遷移元のシーン名</summary>
    private string _fromSceneName;
    /// <summary>遷移先のシーン名</summary>
    public string sceneName { get; set; } = "main";

    /// <summary>
    /// 次のシーンへ遷移する
    /// </summary>
    public void NextScene()
    {
        // 現在のシーンを遷移元シーン名としてセットする
        _fromSceneName = SceneManager.GetActiveScene().name;
        // 次のシーンへメソッドを渡す
        SceneManager.sceneLoaded += LoadedGameScene;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 次のシーンにあるオブジェクトへ値を渡す
    /// </summary>
    /// <param name="next"></param>
    /// <param name="mode"></param>
    private void LoadedGameScene(Scene next, LoadSceneMode mode)
    {
        // 次のシーンのヒエラルキーからシーン名を格納するオブジェクトを探す
        var g = GameObject.FindGameObjectWithTag(TagManager.RESPONSE_SCENE_INFO);
        if (g == null)
        {
            // 無い場合は新規に作成する
            g = new GameObject();
            g.name = NameManager.RESPONSE_SCENE_INFO;
            g.tag = TagManager.RESPONSE_SCENE_INFO;
            g.AddComponent<ResponseSceneInfo>();
        }
        // 現在のシーン名を次のシーンのオブジェクトへ遷移元のシーン情報としてセット
        g.GetComponent<ResponseSceneInfo>()._fromSceneName = _fromSceneName;

        // シーン移動の度に実行されないように消す
        SceneManager.sceneLoaded -= LoadedGameScene;
    }
}
