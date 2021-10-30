using DeadException;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.ActiveManager
{
    /// <summary>
    /// ギミック共通処理
    /// </summary>
    public class GimmickDifferent
    {
        /// <summary>
        /// 動くギミックをタグ名から取得
        /// </summary>
        /// <param name="tagName">タグ名</param>
        /// <param name="componentName">コンポーネント名</param>
        /// <param name="value">コンポーネントの型</param>
        /// <returns>動くギミックオブジェクト</returns>
        public List<T> GetScriptLists<T>(string tagName, string componentName, T value) where T : Object
        {
            var ary = GameObject.FindGameObjectsWithTag(tagName);
            var workLst = new List<T>();
            foreach (var obj in ary)
            {
                if (DeadNullReference.CheckReferencedComponent(obj, componentName) == true)
                {
                    workLst.Add(obj.GetComponent<T>());
                }
            }

            return workLst;
        }
    }
}
