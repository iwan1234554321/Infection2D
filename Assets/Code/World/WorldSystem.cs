using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Notteam.World
{
    public abstract class WorldSystem : MonoBehaviour
    {
        internal virtual void OnCreatedEntity<TEntity>(TEntity entity) where TEntity : WorldEntity { }
        internal virtual void OnDestroyedEntity<TEntity>(TEntity entity) where TEntity : WorldEntity { }

        internal virtual void OnStartInternal() { OnStart(); }

        internal virtual void OnUpdateInternal() { OnUpdate(); }

        internal virtual void OnFinalInternal() { OnFinal(); }

        protected virtual void OnStart() { }

        protected virtual void OnUpdate() { }

        protected virtual void OnFinal() { }
    }

    public class WorldSystem<T> : WorldSystem where T : WorldEntity
    {
        protected List<T> entities;

        private bool _launchEntityCollector;

        private List<T> _prepareInitializationQueueEntities = new List<T>();

        private void PrepareEntitiesForInitialization()
        {
            if (_prepareInitializationQueueEntities.Count > 0)
            {
                for (var i = _prepareInitializationQueueEntities.Count - 1; i >= 0; i--)
                {
                    var entity = _prepareInitializationQueueEntities[i];

                    entities.Add(entity);

                    entity.OnStartInternal();

                    _prepareInitializationQueueEntities.RemoveAt(i);
                }
            }
        }

        private void UpdateEntityCollector()
        {
            if (_launchEntityCollector)
            {
                for (var i = entities.Count - 1; i >= 0; i--)
                {
                    var entity = entities[i];

                    if (entity == null)
                        entities.RemoveAt(i);
                }

                _launchEntityCollector = false;
            }
        }

        internal override void OnCreatedEntity<TEntity>(TEntity entity)
        {
            if (entity.GetType() == typeof(T) && entities != null && !entities.Contains(entity as T))
            {
                _prepareInitializationQueueEntities.Add(entity as T);
            }
        }

        internal override void OnDestroyedEntity<TEntity>(TEntity entity)
        {
            if (entity.GetType() == typeof(T))
            {
                _launchEntityCollector = true;
            }
        }

        internal override void OnStartInternal()
        {
            entities = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

            base.OnStartInternal();

            foreach (var entity in entities)
                entity.OnStartInternal();
        }

        internal override void OnUpdateInternal()
        {
            UpdateEntityCollector();

            base.OnUpdateInternal();

            foreach (var entity in entities)
            {
                if (entity == null || !entity.enabled)
                    continue;
                else
                    entity.OnUpdateInternal();
            }

            PrepareEntitiesForInitialization();
        }

        internal override void OnFinalInternal()
        {
            foreach (var entity in entities)
                entity.OnFinalInternal();

            base.OnFinalInternal();
        }
    }
}