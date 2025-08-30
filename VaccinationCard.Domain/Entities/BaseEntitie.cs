namespace VaccinationCard.Domain.Entities;

public class BaseEntitie
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdateAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; } = null;

    public BaseEntitie()
    {
        Id = Guid.NewGuid();
        CreatedAt = CreatedAt;
        UpdateAt = UpdateAt;
    }
}
