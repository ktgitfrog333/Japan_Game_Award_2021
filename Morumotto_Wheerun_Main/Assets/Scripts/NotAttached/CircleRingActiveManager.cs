using Const.Tag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// 回転リングの処理を有効・無効
    /// </summary>
    public class CircleRingActiveManager : GimmickDifferent, GimmkckActiveManager
    {
        /// <summary>回転リングの処理</summary>
        public CircleRing[] _gimmicks { get; private set; }

        /// <summary>
        /// 回転リングの処理を有効・無効
        /// </summary>
        public CircleRingActiveManager()
        {
            // レベルデザイン内にある回し車の制御スクリプトをリスト化
            var cmp = new CircleRing();
            var workLst = GetScriptLists(TagManager.CIRCLE_RING, cmp.GetType().ToString(), cmp);

            if (0 < workLst.Count)
            {
                _gimmicks = workLst.ToArray();
            }
        }

        /// <summary>
        /// ギミックを再開
        /// </summary>
        public void StopGimmik()
        {
            if (_gimmicks != null && 0 < _gimmicks.Length)
            {
                foreach (var script in _gimmicks)
                {
                    script.enabled = false;
                }
            }
        }

        /// <summary>
        /// ギミックを停止
        /// </summary>
        public void StartGimmik()
        {
            if (_gimmicks != null && 0 < _gimmicks.Length)
            {
                foreach (var script in _gimmicks)
                {
                    script.enabled = true;
                }
            }
        }
    }
}
