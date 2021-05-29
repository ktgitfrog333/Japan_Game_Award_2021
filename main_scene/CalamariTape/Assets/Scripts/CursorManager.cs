using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カーソルを非表示にするスクリプトクラスを作成
/// </summary>
public class CursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
