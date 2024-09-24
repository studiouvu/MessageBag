using System;
using System.Collections.Generic;
using Studiouvu.Core.MessageBag.Message;
namespace Studiouvu.Core.MessageBag
{
    public class MessageBag<T> : IMessageBag where T : IMessage
    {
        private readonly Dictionary<MessageBagToken, Action<T>> _events = new();

        public MessageBagToken AddEvent(MessageBagToken token, Action<T> action)
        {
            if (!_events.TryAdd(token, action))
                _events[token] += action;

            token.registeredList.AddLast(this);
            return token;
        }

        public void Release(MessageBagToken token)
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
