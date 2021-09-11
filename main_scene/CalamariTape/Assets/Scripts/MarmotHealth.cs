using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回し車と扉の耐久値を管理
/// </summary>
public class MarmotHealth : MonoBehaviour
{
    /// <summary>耐久値の最大値</summary>
    public int _healthMax = 15;
    /// <summary>耐久値の最小値</summary>
    public int _healthMin { private set; get; } = 0;
    /// <summary>耐久値</summary>
    public float _health;
    /// <summary>耐久値を更新する（戻す）フラグ</summary>
    public bool _returnUpdateHealth { set; get; }
    /// <summary>回し車を制御</summary>
    [SerializeField] private MarmotWheel _wheel;
    /// <summary>開くドアを制御</summary>
    [SerializeField] private DoorOpener _opener;

    /// <summary>
    /// 耐久値を更新する
    /// </summary>
    /// <param name="ereaIn">エリア内にいるか</param>
    /// <param name="spinStart">回転を再生</param>
    /// <param name="mirror">回転方向</param>
    public void UpdateHealth(bool ereaIn, bool spinStart, bool mirror, int Level)
    {
        if (_returnUpdateHealth == false)
        {
            if (ereaIn == true)
            {
                // 回転を開始
                if (spinStart == true)
                {
                    // 回転方向
                    if (mirror == false)
                    {
                        if (_health <= _healthMax)
                        {
                            _health += (Time.deltaTime * Level);
                        }
                        else
                        {
                            _health = _healthMax;
                        }
                    }
                    else
                    {
                        if (_healthMin <= _health)
                        {
                            _health -= (Time.deltaTime * Level);
                        }
                        else
                        {
                            _health = _healthMin;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 耐久値を更新する（戻す）
    /// </summary>
    public void ReturnUpdateHealth()
    {
        if (_returnUpdateHealth == true)
        {
            if (_healthMin <= _health)
            {
                _health -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// オブジェクトに紐づく回し車とドアの有効・無効制御
    /// </summary>
    /// <param name="active">制御フラグ（有効・無効）</param>
    public void SetActiveLinkGimmick(bool active)
    {
        _wheel.enabled = active;
        _opener.enabled = active;
    }
}
