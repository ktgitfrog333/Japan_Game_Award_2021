using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.StageIndex
{
    /// <summary>
    /// クリア状態を管理するステージ番号
    /// ・１ステージをチュートリアル
    /// ・２～４ステージを１セクション（１画面に収まる感じ）
    /// ・５～７ステージを１セクション（画面から始める感じ）
    /// ・８～１０ステージを１セクション（画面のサイズが1.5個分程）
    /// </summary>
    public enum StageClearNumber
    {
        /// <summary>チュートリアル - ステージ１</summary>
        SECTION_000_STAGE_001 = 0,
        /// <summary>セクション１ - ステージ２</summary>
        SECTION_001_STAGE_002 = 1,
        /// <summary>セクション１ - ステージ３</summary>
        SECTION_001_STAGE_003 = 2,
        /// <summary>セクション１ - ステージ４</summary>
        SECTION_001_STAGE_004 = 3,
        /// <summary>セクション２ - ステージ５</summary>
        SECTION_002_STAGE_005 = 4,
        /// <summary>セクション２ - ステージ６</summary>
        SECTION_002_STAGE_006 = 5,
        /// <summary>セクション２ - ステージ７</summary>
        SECTION_002_STAGE_007 = 6,
        /// <summary>セクション３ - ステージ８</summary>
        SECTION_003_STAGE_008 = 7,
        /// <summary>セクション３ - ステージ９</summary>
        SECTION_003_STAGE_009 = 8,
        /// <summary>セクション３ - ステージ１０</summary>
        SECTION_003_STAGE_010 = 9
    }
}
