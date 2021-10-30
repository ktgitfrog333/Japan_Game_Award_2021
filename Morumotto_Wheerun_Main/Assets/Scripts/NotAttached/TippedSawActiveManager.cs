using Const.Tag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// モード強制解除の処理を有効・無効
    /// </summary>
    public class TippedSawActiveManager : GimmickDifferent, GimmkckActiveManager
    {
        /// <summary>モード強制解除の処理</summary>
        public TippedSaw[] _gimmicks { get; private set; }

        /// <summary>モード強制解除の処理を有効・無効</summary>
        public TippedSawActiveManager()
        {
            // レベルデザイン内にあるモード強制解除の制御スクリプトをリスト化
            var cmp = new TippedSaw();
            var workLst = GetScriptLists(TagManager.TIPPED_SAW, cmp.GetType().ToString(), cmp);

            if (0 < workLst.Count)
            {
                _gimmicks = workLst.ToArray();
            }
        }

        /// <summary>
        /// ギミックを停止
        /// </summary>
        public void StopGimmik()
        {
            if (_gimmicks != null && 0 < _gimmicks.Length)
            {
                foreach (var script in _gimmicks)
                {
                    script.SetActive(false);
                }
            }
        }

        /// <summary>
        /// ギミックを再開
        /// </summary>
        public void StartGimmik()
        {
            if (_gimmicks != null && 0 < _gimmicks.Length)
            {
                foreach (var script in _gimmicks)
                {
                    script.SetActive(true);
                }
            }
        }
    }
}
