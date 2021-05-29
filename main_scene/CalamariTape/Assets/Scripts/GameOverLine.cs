using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ外へ落下した際に初期位置で戻すスクリプトクラス
/// </summary>
public class GameOverLine : MonoBehaviour
{
    /// <summary>各モード管理</summary>
    [SerializeField] PlayerManager _playerManager;
    /// <summary>カラマリモードの位置</summary>
    private Vector3 _replayPositionCalamari;
    /// <summary>ネンチャクモードの位置</summary>
    private Vector3 _replayPositionNenchaku;
    /// <summary>ツルツルモードの位置</summary>
    private Vector3 _replayPositionTsurutsuru;

    private void Start()
    {
        _replayPositionCalamari = new Vector3(_playerManager._calamari.transform.position.x, _playerManager._calamari.transform.position.y, _playerManager._calamari.transform.position.z);
        _replayPositionNenchaku = new Vector3(_playerManager._nenchak.transform.position.x, _playerManager._nenchak.transform.position.y, _playerManager._nenchak.transform.position.z);
        _replayPositionTsurutsuru = new Vector3(_playerManager._tsurutsuru.transform.position.x, _playerManager._tsurutsuru.transform.position.y, _playerManager._tsurutsuru.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (other.gameObject.name.Equals(_playerManager._calamari.name))
            {
                _playerManager._calamari.GetComponent<CharacterController>().enabled = false;
                _playerManager._calamari.GetComponent<CalamariMoveController>().enabled = false;
                _playerManager._calamari.transform.position = _replayPositionCalamari;
            }
            else if (other.gameObject.name.Equals(_playerManager._nenchak.name))
            {
                _playerManager._nenchak.GetComponent<CharacterController>().enabled = false;
                _playerManager._nenchak.GetComponent<NenchakMoveController>().enabled = false;
                _playerManager._nenchak.transform.position = _replayPositionNenchaku;
            }
            else if (other.gameObject.name.Equals(_playerManager._tsurutsuru.name))
            {
                _playerManager._tsurutsuru.GetComponent<TsuruTsuruMoveController>().OtherActionTsurutsuruStop();
                _playerManager._tsurutsuru.GetComponent<CharacterController>().enabled = false;
                _playerManager._tsurutsuru.GetComponent<TsuruTsuruMoveController>().enabled = false;
                _playerManager._tsurutsuru.transform.position = _replayPositionTsurutsuru;
            }

            StartCoroutine(ControllerEnabled(other.gameObject));
        }
    }

    private IEnumerator ControllerEnabled(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.2f);
        if (gameObject.name.Equals(_playerManager._calamari.name))
        {
            _playerManager._calamari.GetComponent<CharacterController>().enabled = true;
            _playerManager._calamari.GetComponent<CalamariMoveController>().enabled = true;
        }
        else if (gameObject.name.Equals(_playerManager._nenchak.name))
        {
            _playerManager._nenchak.GetComponent<CharacterController>().enabled = true;
            _playerManager._nenchak.GetComponent<NenchakMoveController>().enabled = true;
        }
        else if (gameObject.name.Equals(_playerManager._tsurutsuru.name))
        {
            _playerManager._tsurutsuru.GetComponent<CharacterController>().enabled = true;
            _playerManager._tsurutsuru.GetComponent<TsuruTsuruMoveController>().enabled = true;
        }

        StopCoroutine(ControllerEnabled(gameObject));
    }
}
