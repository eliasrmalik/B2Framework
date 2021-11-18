using System;
using System.Collections.Generic;
using System.Linq;
using TeamServer.Models;

namespace TeamServer.Services
{
    public interface IListenerService
    {

        void AddListener(Listener listener);

        IEnumerable<Listener> GetListeners();

        Listener GetListener(string name);

        void RemoveListener(Listener listener);

    }

    public class ListenerService : IListenerService
    {

        //Explore managing listeners outside memory of the TS, maybe write them to a DB or a file


        private readonly List<Listener> _listeners = new();

        public void AddListener(Listener listener)
        {
            _listeners.Add(listener);       

        }

        public Listener GetListener(string name)
        {
            return GetListeners().FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Listener> GetListeners()
        {
            return _listeners;
        }

        public void RemoveListener(Listener listener)
        {
            _listeners.Remove(Listener);
        }
    }

}
