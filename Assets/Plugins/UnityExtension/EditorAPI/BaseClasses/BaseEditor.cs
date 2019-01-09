﻿#if UNITY_EDITOR

using UnityEngine;

namespace UnityExtension.Editor
{
    /// <summary>
    /// BaseEditor<T>
    /// </summary>
    public class BaseEditor<T> : UnityEditor.Editor where T : Object
    {
        protected new T target => base.target as T;


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

    } // class BaseEditor<T>

} // namespace UnityExtension.Editor

#endif // UNITY_EDITOR