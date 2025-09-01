using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.Application.Common.Mappings.Vaccinations;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enum;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCardHandlerTests.Commands.Vaccinations;

public class CreateVaccinationHandlerTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
    private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
    private readonly Mock<ILogger<CreateVaccinationHandler>> _loggerMock;
    private readonly IMapper _mapper;


    public CreateVaccinationHandlerTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _vaccineRepositoryMock = new Mock<IVaccineRepository>();
        _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
        _loggerMock = new Mock<ILogger<CreateVaccinationHandler>>();


        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var cfgg = new MapperConfiguration(cfi =>
        {
            cfi.AddProfile(new VaccinationProfile());
            cfi.AddProfile(new VaccineProfile());
        }, loggerFactory);

        _mapper = cfgg.CreateMapper();

    }

    private CreateVaccinationHandler SUT() => new(
    _vaccinationRepositoryMock.Object,
    _personRepositoryMock.Object,
    _vaccineRepositoryMock.Object,
    _loggerMock.Object,
   _mapper
    );


    [Fact]
    public async Task Should_throw_NotFound_when_person_missing()
    {

        var cmd = new CreateVaccinationCommand
        {
            PersonId = Guid.NewGuid(),
            VaccineId = Guid.NewGuid(),
            Dose = Dose.firstDose
        };

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(cmd.PersonId, It.IsAny<CancellationToken>()))
               .ReturnsAsync((Person?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default)); 

    }


    [Fact]
    public async Task Should_throw_NotFound_when_vaccine_missing()
    {
        var cmd = new CreateVaccinationCommand
        {
            PersonId = Guid.NewGuid(),
            VaccineId = Guid.NewGuid(),
            Dose = Dose.firstDose
        };

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(cmd.PersonId, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Person { Id = cmd.PersonId });

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(cmd.VaccineId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Vaccine?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));
    }


    [Fact]
    public async Task Should_throw_Conflict_when_duplicate_dose()
    {
        var cmd = new CreateVaccinationCommand
        {
            PersonId = Guid.NewGuid(),
            VaccineId = Guid.NewGuid(),
            Dose = Dose.firstDose
        };

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(cmd.PersonId, default)).ReturnsAsync(new Person { Id = cmd.PersonId });

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(cmd.VaccineId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Vaccine { Id = cmd.VaccineId, Name = "HPV" });

        _vaccinationRepositoryMock.Setup(r => r.VaccinationExistsAsync(cmd.PersonId, cmd.VaccineId, cmd.Dose, default)).ReturnsAsync(true);

        await Assert.ThrowsAsync<ConflictException>(() => SUT().Handle(cmd, default));
    }


    [Fact]
    public async Task Should_throw_422_when_missing_previous_doses()
    {
        var cmd = new CreateVaccinationCommand
        {
            PersonId = Guid.NewGuid(),
            VaccineId = Guid.NewGuid(),
            Dose = Dose.secondDose
        };

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(cmd.PersonId, default)).ReturnsAsync(new Person { Id = cmd.PersonId });

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(cmd.VaccineId, default)).ReturnsAsync(new Vaccine { Id = cmd.VaccineId, Name = "Hep B" });

        _vaccinationRepositoryMock.Setup(r => r.VaccinationExistsAsync(cmd.PersonId, cmd.VaccineId, cmd.Dose, default)).ReturnsAsync(false);

        _vaccinationRepositoryMock.Setup(r => r.HasAllPreviousDosesAsync(cmd.PersonId, cmd.VaccineId, cmd.Dose, default)).ReturnsAsync(false);

        await Assert.ThrowsAsync<UnprocessableContentException>(() => SUT().Handle(cmd, default));
    }

    [Fact]
    public async Task Should_create_and_return_result_on_success()
    {
        var personId = Guid.NewGuid();
        var vaccineId = Guid.NewGuid();
        var cmd = new CreateVaccinationCommand
        {
            PersonId = personId,
            VaccineId = vaccineId,
            Dose = Dose.firstDose
        };

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(personId, default)).ReturnsAsync(new Person { Id = personId });

        _vaccineRepositoryMock.Setup(r => r.GetVaccineByIdAsync(vaccineId, default))
                 .ReturnsAsync(new Vaccine { Id = vaccineId, Name = "Hep B" });

        _vaccinationRepositoryMock.Setup(r => r.VaccinationExistsAsync(personId, vaccineId, Dose.firstDose, default)).ReturnsAsync(false);

        _vaccinationRepositoryMock.Setup(r => r.HasAllPreviousDosesAsync(personId, vaccineId, Dose.firstDose, default)).ReturnsAsync(true);

        _vaccinationRepositoryMock.Setup(r => r.CreateVaccinationAsync(It.IsAny<Vaccination>()))
            .ReturnsAsync((Vaccination v) => { v.Id = Guid.NewGuid(); return v; });

        var res = await SUT().Handle(cmd, default);

        res.PersonId.Should().Be(personId);
        res.Vaccine.VaccineId.Should().Be(vaccineId);
        res.DoseAplicated.Should().Be(Dose.firstDose);
        res.Vaccine!.Name.Should().Be("Hep B");
        _vaccinationRepositoryMock.Verify(r => r.CreateVaccinationAsync(It.IsAny<Vaccination>()), Times.Once);
    }
}
