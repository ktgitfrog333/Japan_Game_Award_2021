using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デバッグのデモ用コード一覧
/// </summary>
public interface DebugDemo
{
    /// <summary>
    /// デモ：文字列を受け取って表示する
    /// </summary>
    /// <param name="message">文字列メッセージ</param>
    void DebugDemo1(string message);
}
