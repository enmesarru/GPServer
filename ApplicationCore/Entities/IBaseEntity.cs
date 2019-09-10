namespace ApplicationCore.Entities
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}