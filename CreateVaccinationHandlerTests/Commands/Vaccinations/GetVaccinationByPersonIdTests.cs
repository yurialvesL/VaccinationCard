using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;
using VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;
using VaccinationCard.Application.Common.Mappings.Vaccinations;
using VaccinationCard.Application.Common.Mappings.Vaccine;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enum;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCardHandlerTests.Commands.Vaccinations;

public class GetVaccinationByPersonIdTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
    private readonly Mock<ILogger<GetVaccinationByPersonIdHandler>> _loggerMock;
    private readonly IMapper _mapper;


    public GetVaccinationByPersonIdTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
        _loggerMock = new Mock<ILogger<GetVaccinationByPersonIdHandler>>();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new VaccinationProfile());
            cfg.AddProfile(new VaccineProfile());
        }, loggerFactory);

        _mapper = configuration.CreateMapper();
    }

    private GetVaccinationByPersonIdHandler SUT() => new(
   _vaccinationRepositoryMock.Object,
   _personRepositoryMock.Object,
   _loggerMock.Object,
  _mapper
   );

    [Fact]
    public async Task Should_throw_NotFound_when_person_not_exists()
    {
        // arrange
        var personId = Guid.NewGuid();
        var cmd = new GetVaccinationByPersonIdCommand { PersonId = personId };

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(personId, It.IsAny<CancellationToken>()))
               .ReturnsAsync((Person?)null);

        // act + assert
        await Assert.ThrowsAsync<NotFoundException>(() => SUT().Handle(cmd, default));

        _vaccinationRepositoryMock.Verify(r => r.GetAllVaccinationsByPersonIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Should_return_vaccination_list_on_success()
    {
        // arrange
        var personId = Guid.NewGuid();
        var vaccineId = Guid.NewGuid();
        var cmd = new GetVaccinationByPersonIdCommand { PersonId = personId};

        _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(personId, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Person { Id = personId });

        var items = new List<Vaccination>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                PersonId = personId,
                VaccineId = vaccineId,
                Dose = Dose.firstDose,
                Vaccine = new Vaccine { Id = vaccineId, Name = "Hep B" },
                Person = new Person { Id = personId, Name = "John"},
                CreatedAt = DateTime.UtcNow
            }
        };

        _vaccinationRepositoryMock.Setup(r => r.GetAllVaccinationsByPersonIdAsync(personId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(items);

        // act
        var result = await SUT().Handle(cmd, default);

        // assert
        result.Should().NotBeNull();
        result.Vaccinations.Should().HaveCount(1);
        var v = result.Vaccinations![0];
        v.VaccinationId.Should().Be(items[0].Id);
        v.PersonId.Should().Be(personId);
        v.Vaccine.VaccineId.Should().Be(vaccineId);
        v.Vaccine.Name.Should().Be("Hep B");
        v.DoseApplied.Should().Be(Dose.firstDose);
    }
}
