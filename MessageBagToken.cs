using System.Collections.Generic;
using UnityEngine;
namespace Studiouvu.Core.MessageBag
{
    public class MessageBagToken
    {
        public readonly LinkedList<IMessageBag> registeredList = new();

        ~MessageBagToken()
        {
            if (registeredList.Count != 0 && Application.isPlaying)
                Debug.LogError($"[EventBagToken] 해제되지 않은 이벤트가 있습니다");
        }
        
        public void Release()
        {
            foreach (var eventBag in registeredList)
                eventBag.Release(this);
        }
    }
}
