using System.Collections.Generic;
using UnityEngine;
namespace Studiouvu.Core.EventBag
{
    public class EventBagToken
    {
        public readonly LinkedList<IEventBag> registeredList = new();

        ~EventBagToken()
        {
            if (registeredList.Count != 0 && Application.isPlaying)
                Debug.LogWarning($"[EventBagToken] 해제되지 않은 이벤트가 있습니다");
        }
        
        public void Release()
        {
            foreach (var eventBag in registeredList)
                eventBag.Release(this);
        }
    }
}
