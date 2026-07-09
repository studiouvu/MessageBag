namespace Studiouvu.Core.EventBag
{
    public interface IEventBag
    {
        void Release(EventBagToken token);
        void Clear();
    }
}
