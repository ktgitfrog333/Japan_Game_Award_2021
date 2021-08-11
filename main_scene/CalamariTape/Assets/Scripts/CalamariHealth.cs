using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの耐久値
/// </summary>
public class CalamariHealth : MonoBehaviour
{
    /// <summary>ネンチャクモードの状態</summary>
    [SerializeField] private CalamariState _state;
    /// <summary>透明度の初期値</summary>
    public float _defaultAlpha { get; set; }
    /// <summary>アルファ値</summary>
    public float _alpha;
    /// <summary>マテリアルを点滅フラグ</summary>
    public bool _blinkingMaterialStart { get; set; }

    /// <summary>パラメータ</summary>
    public float Parameter
    {
        get
        {
            return _state._value._parameter;
        }
        set
        {
            _state._value._parameter = value;
        }
    }

    /// <summary>粘着フラグ</summary>
    public bool Adhesive {
        get
        {
            return _state._value._adhesive;
        }
        set
        {
            _state._value._adhesive = value;
        }
    }

    /// <summary>
    /// カラー情報を取得
    /// </summary>
    /// <returns></returns>
    public Color ReadMaterial()
    {
        return _state._renderer.material.color;
    }

    /// <summary>
    /// マテリアルに色を反映
    /// </summary>
    /// <param name="red">赤</param>
    /// <param name="green">緑</param>
    /// <param name="blue">青</param>
    /// <param name="alpha">透明</param>
    public void ReflectMaterial(float red, float green, float blue, float alpha)
    {
        var color = new Color(red, green, blue, alpha);
        _state._renderer.material.color = color;
    }

    /// <summary>
    /// マテリアルに色を反映
    /// </summary>
    /// <param name="color">カラー情報</param>
    public void ReflectMaterial(Color color)
    {
        _state._renderer.material.color = color;
    }

    /// <summary>
    /// アルファ値を計算
    /// </summary>
    /// <param name="parameter">耐久値</param>
    /// <returns>アルファ値</returns>
    public float CalcAlpha(float parameter)
    {
        return _defaultAlpha + ((1 - _defaultAlpha) * ((10 - parameter) / 10));
    }

    /// <summary>
    /// マテリアルを点滅させる
    /// </summary>
    /// <returns></returns>
    public IEnumerator BlinkingMaterial()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (0f < _state._renderer.material.color.a)
            {
                _alpha = _state._renderer.material.color.a;
                var c = _state._renderer.material.color;
                _state._renderer.material.color = new Color(c.r, c.g, c.b, 0f);
            }
            else
            {
                var c = _state._renderer.material.color;
                _state._renderer.material.color = new Color(c.r, c.g, c.b, _alpha);
            }
        }
    }
}
