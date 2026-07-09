using System;
using System.Collections.Generic;
namespace Studiouvu.Core.EventBag
{
    public class EventBagToken
    {
        private readonly HashSet<IEventBag> _registeredList = new();

        public void Register(IEventBag eventBag)
        {
            if (eventBag == null)
                throw new ArgumentNullException(nameof(eventBag), "EventBag cannot be null");

            _registeredList.Add(eventBag);
        }

        public void Release()
        {
            foreach (var eventBag in _registeredList)
                eventBag.Release(this);

            _registeredList.Clear();
        }

        public void Release(IEventBag eventBag)
        {
            if (eventBag == null)
                throw new ArgumentNullException(nameof(eventBag), "EventBag cannot be null");
            _registeredList.Remove(eventBag);
            eventBag.Release(this);
        }
    }
}
