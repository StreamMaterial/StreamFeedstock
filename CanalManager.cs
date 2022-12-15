namespace StreamFeedstock
{
    public static class CanalManager
    {
        private static readonly List<Canal?> m_Canals = new();

        public static bool NewCanal<T>(dynamic canalID)
        {
            int id = Convert.ToInt32(canalID);
            if (id >= m_Canals.Count)
            {
                for (int i = m_Canals.Count; i <= id; ++i)
                    m_Canals.Add(null);
            }
            if (m_Canals[id] != null)
                return false;
            m_Canals[id] = new Canal(canalID, typeof(T));
            return true;
        }

        public static bool NewCanal(dynamic canalID)
        {
            int id = Convert.ToInt32(canalID);
            if (id >= m_Canals.Count)
            {
                for (int i = m_Canals.Count; i <= id; ++i)
                    m_Canals.Add(null);
            }
            if (m_Canals[id] != null)
                return false;
            m_Canals[id] = new Canal(canalID);
            return true;
        }

        private static Canal? GetCanal(int canalID)
        {
            if (canalID >= m_Canals.Count)
                return null;
            return m_Canals[canalID];
        }

        public static bool Register<T>(dynamic canalID, Canal.MessageListener listener)
        {
            int id = Convert.ToInt32(canalID);
            Canal? canal = GetCanal(id);
            if (canal != null && canal.IsValid<T>())
            {
                canal.Add(listener);
                return true;
            }
            return false;
        }

        public static bool Register(dynamic canalID, Canal.TriggerListener trigger)
        {
            int id = Convert.ToInt32(canalID);
            Canal? canal = GetCanal(id);
            if (canal != null && canal.IsValid())
            {
                canal.Add(trigger);
                return true;
            }
            return false;
        }

        public static bool Unregister<T>(dynamic canalID, Canal.MessageListener listener)
        {
            int id = Convert.ToInt32(canalID);
            Canal? canal = GetCanal(id);
            if (canal != null && canal.IsValid<T>())
            {
                canal.Remove(listener);
                return true;
            }
            return false;
        }

        public static bool Unregister(dynamic canalID, Canal.TriggerListener trigger)
        {
            int id = Convert.ToInt32(canalID);
            Canal? canal = GetCanal(id);
            if (canal != null && canal.IsValid())
            {
                canal.Remove(trigger);
                return true;
            }
            return false;
        }

        public static bool Emit<T>(dynamic canalID, T args)
        {
            int id = Convert.ToInt32(canalID);
            Canal? canal = GetCanal(id);
            if (canal != null && canal.IsValid<T>())
            {
                canal.Iterate(args);
                return true;
            }
            return false;
        }

        public static bool Emit(dynamic canalID)
        {
            int id = Convert.ToInt32(canalID);
            Canal? canal = GetCanal(id);
            if (canal != null && canal.IsValid())
            {
                canal.Iterate();
                return true;
            }
            return false;
        }
    }
}
