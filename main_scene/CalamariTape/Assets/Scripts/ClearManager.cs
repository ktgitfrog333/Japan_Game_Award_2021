using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア画面の制御スクリプトクラス
/// </summary>
public class ClearManager : MonoBehaviour
{
    /// <summary>もう一度遊ぶUI</summary>
    [SerializeField] private GameObject _retryUI;
    /// <summary>他のステージを選ぶUI</summary>
    [SerializeField] private GameObject _selectUI;
    /// <summary>次のステージを遊ぶUI</summary>
    [SerializeField] private GameObject _nextUI;
    /// <summary>最終ステージ遷移の可否フラグ</summary>
    [SerializeField] private bool _finalStage;

    private void OnEnable()
    {
        StartCoroutine(OpenMenu());
    }

    /// <summary>
    /// クリア画面のメニューを表示
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(3f);

        _retryUI.SetActive(true);
        _selectUI.SetActive(true);

        if (_finalStage == false)
        {
            // 最終ステージでは次のステージロゴを表示させない
            _nextUI.SetActive(true);
        }

        StopCoroutine(OpenMenu());
    }
}
