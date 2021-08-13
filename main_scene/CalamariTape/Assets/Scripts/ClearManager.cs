using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    /// <summary>選択項目のUIスクリプト</summary>
    [SerializeField] private UIController _firstElement;
    /// <summary>選択項目のUIスクリプト</summary>
    public UIController FirstElement
    {
        set
        {
            _firstElement = value;
        }
    }
    /// <summary>選択項目のUIオブジェクト</summary>
    [SerializeField] private GameObject _firstObject;
    /// <summary>選択項目のUIオブジェクト</summary>
    public GameObject FirstObject
    {
        set
        {
            _firstObject = value;
        }
    }
    /// <summary>イベントシステム</summary>
    [SerializeField] private EventSystem _event;

    private void OnEnable()
    {
        StartCoroutine(OpenMenu());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            _event.SetSelectedGameObject(_firstObject);
            _firstElement.Selected();
        }
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

        _event.SetSelectedGameObject(_firstObject);
        _firstElement.Selected();
        StopCoroutine(OpenMenu());
    }
}
