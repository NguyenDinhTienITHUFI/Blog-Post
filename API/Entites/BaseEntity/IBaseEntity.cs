namespace API.Entites.BaseEntity
{
    public interface IBaseEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
