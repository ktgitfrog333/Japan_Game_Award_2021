using Const.Tag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// ベルトコンベアの処理を有効・無効
    /// </summary>
    public class ConveyorMoveCharacterActiveManager : GimmickDifferent, GimmkckActiveManager
    {
        /// <summary>ベルトコンベアの処理</summary>
        public ConveyorMoveCharacter[] _gimmicks { get; private set; }

        /// <summary>
        /// ベルトコンベアの処理を有効・無効
        /// </summary>
        public ConveyorMoveCharacterActiveManager()
        {
            // レベルデザイン内にある回し車の制御スクリプトをリスト化
            var cmp = new ConveyorMoveCharacter();
            var workLst = GetScriptLists(TagManager.CONVEYOR, cmp.GetType().ToString(), cmp);

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
