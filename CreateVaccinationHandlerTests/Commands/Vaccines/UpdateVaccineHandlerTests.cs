using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCardHandlerTests.Commands.Vaccinations;

namespace VaccinationCardHandlerTests.Commands.Vaccines;

public class UpdateVaccineHandlerTests
{
    private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
    private readonly Mock<ILogger<UpdateVaccineHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public UpdateVaccineHandlerTests()
    {
        _vaccineRepositoryMock = new Mock<IVaccineRepository>();

        _loggerMock = new Mock<ILogger<UpdateVaccineHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new VaccineProfile()), loggerFactory);

        _mapper = configuration.CreateMapper();
    }

    private UpdateVaccineHandler SUT() => new(
     _vaccineRepositoryMock.Object,
     _loggerMock.Object,
     _mapper
     );

    [Fact]
    public async Task Should_throw_NotFound_when_vaccine_not_exists()
    {
        // arrange
        var id = Guid.NewGuid();
        var cmd = new UpdateVaccineCommand { 
         Id = id,
         Name = "New Name",
        };

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(id, It.IsAny<CancellationToken>()))
             .ReturnsAsync((Vaccine?)null);

        // act + assert
        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _vaccineRepositoryMock.Verify(r => r.UpdateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()), Times.Never);
    }


    [Fact]
    public async Task Should_return_false_and_log_warning_when_update_returns_null()
    {
        // arrange
        var id = Guid.NewGuid();
        var cmd = new UpdateVaccineCommand
        {
            Id = id,
            Name = "Hep B",
        };

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(id, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new Vaccine { Id = id, Name = "Old" });

        _vaccineRepositoryMock.Setup(r => r.UpdateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync((Vaccine?)null);

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.UpdateSuccess.Should().BeFalse();
        _loggerMock.VerifyLog(LogLevel.Warning, "could not be updated");
        _vaccineRepositoryMock.Verify(r => r.UpdateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_update_and_return_true_on_success()
    {
        // arrange
        var id = Guid.NewGuid();
        var cmd = new UpdateVaccineCommand
            { 
            Id = id, 
            Name = "Influenza" 
        };

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(id, It.IsAny<CancellationToken>()))
             .ReturnsAsync(new Vaccine { Id = id, Name = "Old" });

        Vaccine? captured = null;
        _vaccineRepositoryMock.Setup(r => r.UpdateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()))
             .Callback<Vaccine, CancellationToken>((v, _) => captured = v)
             .ReturnsAsync((Vaccine v, CancellationToken _) => v); 

        var before = DateTime.UtcNow;

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.UpdateSuccess.Should().BeTrue();

        captured.Should().NotBeNull();
        captured!.Id.Should().Be(id);
        captured.Name.Should().Be("Influenza");
        captured.UpdateAt.Should().BeOnOrAfter(before); 

        _vaccineRepositoryMock.Verify(r => r.UpdateVaccineAsync(It.IsAny<Vaccine>(), It.IsAny<CancellationToken>()), Times.Once);
        _loggerMock.VerifyNoLog(LogLevel.Warning);
    }
}
