namespace Sample
{
    public interface IHandler<in T>
    {
        void Handle(T commit);
    }
}