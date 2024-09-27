using System;
using System.Collections.Generic;
using UnityEngine;

namespace Notteam.World
{
    [DefaultExecutionOrder(0)]
    public class World : MonoBehaviour
    {
        private static World _instance;

        public static World Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindAnyObjectByType<World>();

                return _instance;
            }
        }

        private bool _initialized;

        private Dictionary<Type, WorldSystem> _systems;

        private void Awake()
        {
            var childs = GetComponentsInChildren<WorldSystem>();

            _systems = new Dictionary<Type, WorldSystem>();

            foreach (var child in childs)
                _systems.Add(child.GetType(), child);

            _initialized = true;

            foreach (var system in _systems)
                system.Value.OnStartInternal();
        }

        private void Update()
        {
            foreach (var system in _systems)
                system.Value.OnUpdateInternal();
        }

        private void OnDisable()
        {
            foreach (var system in _systems)
                system.Value.OnFinalInternal();
        }

        public T GetSystem<T>() where T : WorldSystem
        {
            if (_systems.TryGetValue(typeof(T), out var system))
                return system as T;

            return null;
        }

        public void CreatedEntity<T>(T entity) where T : WorldEntity
        {
            if (_initialized)
            {
                //Debug.Log($"CREATED ENTITY : {entity.GetType()} : NAME : {entity.name}");

                foreach (var system in _systems)
                    system.Value.OnCreatedEntity(entity);
            }
        }

        public void DestroyedEntity<T>(T entity) where T : WorldEntity
        {
            if (_initialized)
            {
                //Debug.Log($"DESTROYED ENTITY : {entity.GetType()} : NAME : {entity.name}");

                foreach (var system in _systems)
                    system.Value.OnDestroyedEntity(entity);
            }
        }
    }
}