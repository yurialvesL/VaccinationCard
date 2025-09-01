namespace VaccinationCard.CrossCutting.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when one or more validation errors occur.
/// </summary>
/// <remarks>This exception is typically used to indicate that input data does not meet the required validation
/// rules. It provides a collection of validation errors, where each key represents the name of the invalid field and
/// the associated value is an array of error messages for that field.</remarks>
public sealed class ValidationException :  Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string message) : base(message)
        => Errors = new Dictionary<string, string[]>();

    public ValidationException(IDictionary<string, string[]> errors, string? message = null)
        : base(message ?? "An or more validations errros occured.")
        => Errors = errors;
}



public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException(string message = "Not Authenticated") : base(message) { }
}

public sealed class ForbiddenException : Exception
{
    public ForbiddenException(string message = "Acess denied") : base(message) { }
}

public sealed class ConflictException : Exception
{
    public ConflictException(string message = "Conflict while processing the request") : base(message) { }
}

public sealed class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}