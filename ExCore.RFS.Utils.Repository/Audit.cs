namespace ExCore.RFS.Utils.Repository
{
    public interface IIdentifier<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IIdentifier : IIdentifier<int>
    {

    }
}
