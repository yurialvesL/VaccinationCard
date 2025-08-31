namespace VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;

/// <summary>
/// Result of the DeleteVaccine command  
/// </summary>
public class DeleteVaccineResult
{
    /// <summary>
    /// if true, the vaccine was deleted successfully
    /// </summary>
    public bool? IsDeleted { get; set; }
}
