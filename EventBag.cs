using System;
using System.Collections.Generic;
using Studiouvu.Core.EventBag.Event;
namespace Studiouvu.Core.EventBag
{
    public class EventBag<T> : IEventBag where T : IEvent
    {
        private readonly Dictionary<EventBagToken, Action<T>> _events = new();

        public EventBagToken AddEvent(EventBagToken token, Action<T> action)
        {
            if (!_events.TryAdd(token, action))
                _events[token] += action;

            token.registeredList.AddLast(this);
            return token;
        }

        public void Release(EventBagToken token)
        {
            _events.Remove(token);
        }

        public void Invoke(T message)
        {
            foreach (var action in _events.Values)
                action.Invoke(message);
        }
    }
}
