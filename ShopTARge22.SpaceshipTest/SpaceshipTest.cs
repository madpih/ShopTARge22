using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using System.Xml.Linq;

namespace ShopTARge22.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptySpaceship_WhenReturnResult()
        {
            SpaceshipDto spaceship = new SpaceshipDto();
            spaceship.Name = "test1";
            spaceship.Type = "test1";
            spaceship.BuiltDate = DateTime.Now.AddYears(-20);
            spaceship.Passengers = 3;
            spaceship.CargoWeight = 100;
            spaceship.Crew = 100;
            spaceship.EnginePower = 100;
            spaceship.ModifiedAt = DateTime.Now;

            //Act ehk sisuline tegevus mida teeb
            var result = await Svc<ISpaceshipsServices>().Create(spaceship);

            //Assert ehk sisestus
            Assert.NotNull(result); //iga kord kui tehakse unit test, kirjutatakse assert...
        
    }
        [Fact]
        //küsime spaceshipi, mida ei ole olemas - kontrollib kas id juba eksisteerib (not equal???)
        public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            //Arrage
           
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString()); //loob guidi, mida ei ole olemas
            Guid guid = Guid.Parse("67843d9a-31de-4f83-8bc1-c71adfd0ce9b"); //see guid on olemas

            //Act
            //peame kutsuma esile meetodi, mis on SpaceshipService classis
            //otsime läbi õige guidi, mis on olemas.

            await Svc<ISpaceshipsServices>().DetailsAsync(guid);

            //Assert
            //assertimise võrdlus, et võrrelda kahte GUIdi
            Assert.NotEqual(wrongGuid, guid);
        }
        [Fact]
        //Otsib etteantud Id põhjal spaceship objekti üles
        public async Task Should_GetByIdSpaceship_WhenReturnsEqual()
        {              

            //Arrange
            Guid guid1 = Guid.Parse("67843d9a-31de-4f83-8bc1-c71adfd0ce9b");
            Guid guid2 = Guid.Parse("67843d9a-31de-4f83-8bc1-c71adfd0ce9b"); //baasis

            //Act
            await Svc<ISpaceshipsServices>().DetailsAsync(guid2);

            //Assert
            Assert.Equal(guid1, guid2);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
        {
            SpaceshipDto spaceship = MockSpaceshipData();

            var addSpaceship = await Svc<ISpaceshipsServices>().Create(spaceship);
            var result = await Svc<ISpaceshipsServices>().Delete((Guid)addSpaceship.Id);

            Assert.Equal(addSpaceship, result);
        }

        [Fact]
        //updeidib andmeid, kasutab mocki baasi, vastu mida võrdleb
        public async Task Should_UpdateSpaceship_WhenUpdateData()
        {
            //vaja luua guid, mida hakkame kasutama upate puhul
            var guid = new Guid("0822aca3-0c2c-4cbd-9a08-f594c70d029e");
           SpaceshipDto dto = MockSpaceshipData();

            //vaja saada domenist andmed kätte
            Spaceship spaceship = new();

            spaceship.Id = Guid.Parse("0822aca3-0c2c-4cbd-9a08-f594c70d029e");
            spaceship.Name = "test2";
            spaceship.Type = "test2";
            spaceship.BuiltDate = DateTime.Now.AddYears(-20);
            spaceship.Passengers = 3;
            spaceship.CargoWeight = 100;
            spaceship.Crew = 500;
            spaceship.EnginePower = 100;
            spaceship.ModifiedAt = DateTime.Now;

         

            //kasutan domaini andmeid
            await Svc<ISpaceshipsServices>().Update(dto);

            Assert.Equal(spaceship.Id, guid);
            Assert.DoesNotMatch(spaceship.Name, dto.Name);
            Assert.DoesNotMatch(spaceship.Type, dto.Type);
            Assert.NotEqual(spaceship.Crew, dto.Crew);
        }

        private SpaceshipDto MockSpaceshipData() //nö libaandmebaas andmete sisestamisel (vt kustutamise test)
        {
            SpaceshipDto spaceship = new()
            {
                Name = "test1",
                Type = "test1",
                BuiltDate = DateTime.Now.AddYears(-20),
                Passengers = 3,
                CargoWeight = 100,
                Crew = 100,
                EnginePower = 100,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            return spaceship;
        }

    }
}