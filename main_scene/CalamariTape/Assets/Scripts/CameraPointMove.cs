using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// プレイヤーのモード切り替えとカメラ追従制御スクリプトクラス
/// </summary>
public class CameraPointMove : MonoBehaviour
{
    /// <summary>追従元のCinamechine</summary>
    [SerializeField] CinemachineVirtualCamera _camera;

    /// <summary>追従先のプレイヤーオブジェクト</summary>
    private Transform _playerTransform;

    /// <summary>
    /// 追従先のゲームオブジェクト情報をセット
    /// </summary>
    /// <param name="gameObject">追従するプレイヤー</param>
    public void PlayerCameraLook(GameObject gameObject)
    {
        _camera.Follow = gameObject.transform;
        _camera.LookAt = gameObject.transform;
    }
}
