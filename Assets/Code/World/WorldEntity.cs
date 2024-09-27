using UnityEngine;

namespace Notteam.World
{
    public abstract class WorldEntity : MonoBehaviour
    {
        private bool _initialized;

        internal bool Initialized => _initialized;

        internal void OnStartInternal() { OnStart(); _initialized = true; }
        internal void OnUpdateInternal() { OnUpdate(); }
        internal void OnFinalInternal() { OnFinal(); }

        private void Awake()
        {
            if (World.Instance)
                World.Instance.CreatedEntity(this);
        }

        private void OnDestroy()
        {
            if (World.Instance)
                World.Instance.DestroyedEntity(this);
        }

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnFinal() { }
    }
}