﻿using UnityEngine;

namespace UnityExtension
{
    /// <summary>
    /// 通过配置的方式选择 UpdateMode
    /// </summary>
    public abstract class ConfigurableUpdateComponent : ScriptableComponent
    {
        [SerializeField]
        UpdateMode _updateMode = UpdateMode.Update;


        bool _registered = false;


        /// <summary>
        /// 更新模式
        /// </summary>
        public UpdateMode updateMode
        {
            get { return _updateMode; }
            set
            {
                if (_updateMode != value)
                {
                    if (_registered)
                    {
                        Utilities.RemoveUpdate(_updateMode, OnUpdate);
                        _updateMode = value;
                        Utilities.AddUpdate(_updateMode, OnUpdate);

                        #if UNITY_EDITOR
                            _addUpdateMode = _updateMode;
                        #endif
                    }
                    else _updateMode = value;
                }
            }
        }


        protected abstract void OnUpdate();


        protected virtual void OnEnable()
        {
            Utilities.AddUpdate(_updateMode, OnUpdate);
            _registered = true;

            #if UNITY_EDITOR
                _addUpdateMode = _updateMode;
            #endif
        }


        protected virtual void OnDisable()
        {
            Utilities.RemoveUpdate(_updateMode, OnUpdate);
            _registered = false;
        }


#if UNITY_EDITOR

        UpdateMode _addUpdateMode;


        protected virtual void OnValidate()
        {
            if (_registered && _addUpdateMode != _updateMode)
            {
                Utilities.RemoveUpdate(_addUpdateMode, OnUpdate);
                Utilities.AddUpdate(_addUpdateMode = _updateMode, OnUpdate);
            }
        }

#endif

    } // class ConfigurableUpdateComponent

} // namespace UnityExtension