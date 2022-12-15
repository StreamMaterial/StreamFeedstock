using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamFeedstock
{
    public class Canal
    {
        public delegate void MessageListener(int canalID, object? args);
        public delegate void TriggerListener(int canalID);

        private readonly Type? m_Type;
        private readonly List<MessageListener> m_MessageListeners = new();
        private readonly List<TriggerListener> m_TriggerListeners = new();
        private readonly object m_Lock = new();
        private readonly dynamic m_Id;

        public Canal(dynamic id)
        {
            m_Type = null;
            m_Id = id;
        }

        public Canal(dynamic id, Type type)
        {
            m_Type = type;
            m_Id = id;
        }

        public bool IsValid() => m_Type == null;
        public bool IsValid<T>() => m_Type?.IsAssignableFrom(typeof(T)) ?? false;

        public void Add(MessageListener listener)
        {
            Logger.Log("Canal", string.Format("Start adding on {0}", m_Id));
            lock (m_Lock) { m_MessageListeners.Add(listener); }
            Logger.Log("Canal", string.Format("Stop adding on {0}", m_Id));
        }

        public void Add(TriggerListener listener)
        {
            Logger.Log("Canal", string.Format("Start adding on {0}", m_Id));
            lock (m_Lock) { m_TriggerListeners.Add(listener); }
            Logger.Log("Canal", string.Format("Stop adding on {0}", m_Id));
        }

        public void Remove(MessageListener listener)
        {
            Logger.Log("Canal", string.Format("Start adding on {0}", m_Id));
            lock (m_Lock) { m_MessageListeners.Remove(listener); }
            Logger.Log("Canal", string.Format("Stop adding on {0}", m_Id));
        }

        public void Remove(TriggerListener listener)
        {
            Logger.Log("Canal", string.Format("Start adding on {0}", m_Id));
            lock (m_Lock) { m_TriggerListeners.Remove(listener); }
            Logger.Log("Canal", string.Format("Stop adding on {0}", m_Id));
        }

        public void Iterate(object? arg)
        {
            Task.Run(() => {
                Logger.Log("Canal", string.Format("Start iterate on {0}", m_Id));
                lock (m_Lock) { foreach (MessageListener listener in m_MessageListeners) listener(Convert.ToInt32(m_Id), arg); }
                Logger.Log("Canal", string.Format("Stop iterate on {0}", m_Id));
            });
        }

        public void Iterate()
        {
            Task.Run(() => {
                Logger.Log("Canal", string.Format("Start iterate on {0}", m_Id));
                lock (m_Lock) { foreach (TriggerListener listener in m_TriggerListeners) listener(Convert.ToInt32(m_Id)); }
                Logger.Log("Canal", string.Format("Stop iterate on {0}", m_Id));
            });
        }
    }
}
