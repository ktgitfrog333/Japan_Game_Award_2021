using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Beans.Field
{
    /// <summary>
    /// 壁の衝突判定情報
    /// </summary>
    public struct FieldWalled
    {
        /// <summary>衝突結果</summary>
        public bool _collisionResult { get; set; }
        /// <summary>衝突オブジェクト</summary>
        public Collider _hitCollider { get; set; }
    }
}
