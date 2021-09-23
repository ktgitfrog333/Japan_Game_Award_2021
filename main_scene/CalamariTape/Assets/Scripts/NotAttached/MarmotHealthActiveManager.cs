using Const.Tag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// 回し車と扉、制御値の処理を有効・無効
    /// </summary>
    public class MarmotHealthActiveManager : GimmickDifferent, GimmkckActiveManager
    {
        /// <summary>回し車と扉、制御値の処理</summary>
        public MarmotHealth[] _gimmicks { get; private set; }

        /// <summary>
        /// 回し車と扉、制御値の処理を有効・無効
        /// </summary>
        public MarmotHealthActiveManager()
        {
            // レベルデザイン内にある回し車の制御スクリプトをリスト化
            var cmp = new MarmotHealth();
            var workLst = GetScriptLists(TagManager.MARMOT_HEALTH, cmp.GetType().ToString(), cmp);

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
                    script.SetActiveLinkGimmick(false);
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
                    script.SetActiveLinkGimmick(true);
                }
            }
        }
    }
}
