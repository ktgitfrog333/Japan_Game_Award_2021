using Const.Tag;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// 動く壁の処理を有効・無効
    /// </summary>
    public class MoveWallsActiveManager : GimmickDifferent, GimmkckActiveManager
    {
        /// <summary>動く壁の処理</summary>
        public MoveWalls[] _gimmicks { get; private set; }

        /// <summary>
        /// 動く壁の処理
        /// </summary>
        public MoveWallsActiveManager()
        {
            // レベルデザイン内にある横向きの動く壁の制御スクリプトをリスト化
            var cmp = new MoveWalls();
            var workLst = GetScriptLists(TagManager.VERTICAL_WALL, cmp.GetType().ToString(), cmp);
            workLst.AddRange(GetScriptLists(TagManager.HORIZONTAL_WALL, cmp.GetType().ToString(), cmp));

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
