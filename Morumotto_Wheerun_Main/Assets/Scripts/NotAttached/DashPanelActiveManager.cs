using Const.Tag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// ダッシュパネルの処理を有効・無効
    /// </summary>
    public class DashPanelActiveManager : GimmickDifferent, GimmkckActiveManager
    {
        /// <summary>ダッシュパネルの処理</summary>
        public DashPanel[] _gimmicks { get; private set; }

        /// <summary>
        /// ダッシュパネルの処理を有効・無効
        /// </summary>
        public DashPanelActiveManager()
        {
            var dash = new DashPanel();
            var workLst = GetScriptLists(TagManager.DASH_PANEL, dash.GetType().ToString(), dash);

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
                    script.enabled = false;
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
                    script.enabled = true;
                }
            }
        }
    }
}
