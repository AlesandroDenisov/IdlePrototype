﻿using IdleArcade.Data;
using IdleArcade.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace IdleArcade.UI.Windows
{
    public abstract class UIWindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;
    
        protected IPersistentProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;

        public void Construct(IPersistentProgressService progressService)
        {
            ProgressService = progressService;
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Init();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(() => Destroy(gameObject));
        }

        protected virtual void Init()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}