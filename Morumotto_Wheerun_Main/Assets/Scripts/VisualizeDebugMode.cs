using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// ビルド画面用デバッグモード
/// </summary>
public class VisualizeDebugMode : MonoBehaviour
{
    /// <summary>デバッグモード有効フラグ</summary>
    [SerializeField] private bool _debug;
    /// <summary>デバッグモード有効フラグ</summary>
    public bool Debug
    {
        get
        {
            return _debug;
        }
    }
    /// <summary>デバッグUI</summary>
    [SerializeField] private GameObject _debugUI;
    /// <summary>デバッグUI</summary>
    public bool DebugUI
    {
        get
        {
            return _debugUI.activeSelf;
        }
    }
    /// <summary>デバッグログ</summary>
    [SerializeField] private Text _debuglog;

    void Start()
    {
        VisibleDebugWindows();
    }

    /// <summary>
    /// デバッグウィンドウを表示する
    /// </summary>
    private void VisibleDebugWindows()
    {
        if (_debug == true)
        {
            if (_debugUI.activeSelf == false)
            {
                _debugUI.SetActive(true);
            }
        }
        else
        {
            if (_debugUI.activeSelf == true)
            {
                _debugUI.SetActive(false);
            }
        }
    }

    /// <summary>
    /// メッセージを追記
    /// </summary>
    /// <param name="message">ログメッセージ</param>
    public void Log(string message)
    {
        if (string.IsNullOrEmpty(message) == false)
        {
            var sb = new StringBuilder(_debuglog.text);
            sb.Append("\n");
            sb.Append(message);
            _debuglog.text = sb.ToString();
        }
    }
}
