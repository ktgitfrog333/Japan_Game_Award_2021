using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 効果音を再生するスクリプトクラス
/// </summary>
public class SfxPlay : MonoBehaviour
{
    /// <summary>オーディオソース</summary>
    [SerializeField] private AudioSource _audio;
    /// <summary>効果音のクリップ</summary>
    [SerializeField] private AudioClip[] _clip;

    /// <summary>
    /// 指定されたSEを再生する
    /// </summary>
    /// <param name="clipToPlay">SEの名前</param>
    public void PlaySFX(string clipToPlay)
    {
        if (clipToPlay.Equals("jump_1"))
        {
            _audio.clip = _clip[0];
        }
        if (clipToPlay.Equals("jump_2"))
        {
            _audio.clip = _clip[1];
        }
        if (clipToPlay.Equals("se_menu"))
        {
            _audio.clip = _clip[2];
        }
        if (clipToPlay.Equals("se_close"))
        {
            _audio.clip = _clip[3];
        }
        if (clipToPlay.Equals("se_decided"))
        {
            _audio.clip = _clip[4];
        }
        if (clipToPlay.Equals("se_move"))
        {
            _audio.clip = _clip[5];
        }

        _audio.Play();
    }
}
