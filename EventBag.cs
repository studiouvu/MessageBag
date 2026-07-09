using System;
using System.Collections.Generic;
using System.Linq;
namespace Studiouvu.Core.EventBag
{
    public class EventBag<T> : IEventBag
    {
        //todo! AddEvent 후 해제 안되는 이벤트 있는지 추적하는 디버깅 기능 구현하기, Invoke될때 검사하거나, 가지고 있는 Event 상황 디스플레이

        private readonly Dictionary<EventBagToken, Action<T>> _events = new();

        public EventBagToken AddEvent(EventBagToken token, Action<T> action)
        {
            if (!_events.TryAdd(token, action))
                _events[token] += action;

            token.Register(this);
            return token;
        }

        public void Release(EventBagToken token)
        {
            _events.Remove(token);
        }

        public void Invoke(T message)
        {
            var list = _events.Values.ToList();

            foreach (var action in list)
                action.Invoke(message);
        }

        public void Clear()
        {
            foreach (var token in new List<EventBagToken>(_events.Keys))
                token.Release(this);

            if (_events.Count > 0)
                throw new Exception("EventBag Clear Failed: There are still registered events.");
        }
    }

    public class EventBag : IEventBag
    {
        private readonly Dictionary<EventBagToken, Action> _events = new();

        public EventBagToken AddEvent(EventBagToken token, Action action)
        {
            if (!_events.TryAdd(token, action))
                _events[token] += action;

            token.Register(this);
            return token;
        }

        public void Release(EventBagToken token)
        {
            _events.Remove(token);
        }

        public void Invoke()
        {
            var list = _events.Values.ToList();

            foreach (var action in list)
                action.Invoke();
        }

        public void Clear()
        {
            foreach (var token in new List<EventBagToken>(_events.Keys))
                token.Release(this);

            if (_events.Count > 0)
                throw new Exception("EventBag Clear Failed: There are still registered events.");
        }
    }
}
