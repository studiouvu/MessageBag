namespace Studiouvu.Core.MessageBag
{
    public interface IMessageBag
    {
        void Release(MessageBagToken token);
    }
}
