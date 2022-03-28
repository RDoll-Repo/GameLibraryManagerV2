namespace GameLibraryManagerV2.Repository
{
    public interface IEntity<TIndex>
    {
        TIndex Id { get; set; }
    }
}