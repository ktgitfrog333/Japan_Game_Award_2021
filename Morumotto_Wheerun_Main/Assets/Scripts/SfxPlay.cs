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
        // ジャンプ
        if (clipToPlay.Equals("jump_1"))
        {
            _audio.clip = _clip[0];
        }
        // ジャンプ（スケール大）
        if (clipToPlay.Equals("jump_2"))
        {
            _audio.clip = _clip[1];
        }
        // メニューを開く
        if (clipToPlay.Equals("se_menu"))
        {
            _audio.clip = _clip[2];
        }
        // メニューを閉じる
        if (clipToPlay.Equals("se_close"))
        {
            _audio.clip = _clip[3];
        }
        // 項目の決定
        if (clipToPlay.Equals("se_decided"))
        {
            _audio.clip = _clip[4];
        }
        // 移動
        if (clipToPlay.Equals("se_move"))
        {
            _audio.clip = _clip[5];
        }
        // ゲームクリア
        if (clipToPlay.Equals("me_game_clear"))
        {
            _audio.clip = _clip[6];
        }
        // 耐久ゲージ減少
        if (clipToPlay.Equals("se_derable_decrease"))
        {
            _audio.clip = _clip[7];
        }
        // スケール変更（拡大）
        if (clipToPlay.Equals("se_player_expansion"))
        {
            _audio.clip = _clip[8];
        }
        // ステージセレクト
        if (clipToPlay.Equals("se_select"))
        {
            _audio.clip = _clip[9];
        }

        _audio.Play();
    }
}
