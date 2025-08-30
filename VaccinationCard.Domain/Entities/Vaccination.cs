using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Domain.Entities
{
    /// <summary>
    /// Represents a vaccination record linking a person to a vaccine and dose information.
    /// </summary>
    public class Vaccination : BaseEntitie
    {
        /// <summary>
        /// Gets or sets the unique identifier for a person. FK
        /// </summary>
        public Guid PersonId { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for a vaccine. FK
        /// </summary>
        public Guid VaccineId { get; set; }
        /// <summary>
        /// Gets or sets the dose information for the vaccination.
        /// </summary>
        public Dose Dose { get; set; }
        /// <summary>
        /// Gets or sets the person associated with this instance.
        /// </summary>
        public Person Person { get; set; } = null!;
        /// <summary>
        /// Gets or sets the vaccine associated with the entity.
        /// </summary>
        public Vaccine Vaccine { get; set; } = null!;
    }
}
