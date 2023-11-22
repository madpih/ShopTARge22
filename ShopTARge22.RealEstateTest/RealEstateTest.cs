using Microsoft.EntityFrameworkCore;
using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Data.Migrations;


namespace ShopTARge22.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {   //Arrange ehk andmete ettevalmistus
            RealEstateDto realEstate = new RealEstateDto();
            //RealEstateDto realEstate = new(); sama mis eelmine, lihtsalt lühem
            realEstate.Address = "asd";
            realEstate.SizeSqrM = 1000;
            realEstate.RoomCount = 10;
            realEstate.Floor = 3;
            realEstate.BuildingType = "asd";
            realEstate.BuiltInYear = DateTime.Now;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.UpdatedAt = DateTime.Now;

            //Act ehk sisuline tegevus mida teeb
            var result = await Svc<IRealEstatesServices>().Create(realEstate);

            //Assert ehk sisestus
            Assert.NotNull(result); //iga kord kui tehakse unit test, kirjutatakse assert...
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //Arrage
            //küsime realEstate, mida ei ole olemas
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString()); //loob guidi, mida ei ole olemas
            Guid guid = Guid.Parse("6c87369d-ced0-40c3-be4d-b71362bf0c27"); //see guid on olemas

            //Act
            //peame kutsuma esile meetodi, mis on realEstateService classis
            //otsime läbi õige guidi, mis on olemas.

            await Svc<IRealEstatesServices>().DetailsAsync(guid);

            //Assert
            //assertimise võrdlus, et võrrelda kahte GUIdi
            Assert.NotEqual(wrongGuid, guid);
        }
        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {

            //Otsib Id põhjal realestate objekti üles

            //Arrange
            Guid guid1 = Guid.Parse("6c87369d-ced0-40c3-be4d-b71362bf0c27");
            Guid guid2 = Guid.Parse("6c87369d-ced0-40c3-be4d-b71362bf0c27"); //baasis

            //Act
            await Svc<IRealEstatesServices>().DetailsAsync(guid2);

            //Assert
            Assert.Equal(guid1, guid2);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteREalEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstatesServices>().Create(realEstate);
            var result = await Svc<IRealEstatesServices>().Delete((Guid)addRealEstate.Id);

            Assert.Equal(addRealEstate, result);
        }

        [Fact]
        //ei thohiks kustutada id, kui antakse id ette.
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var realEstate1 = await Svc<IRealEstatesServices>().Create(realEstate);
            var realEstate2 = await Svc<IRealEstatesServices>().Create(realEstate);
            var result = await Svc<IRealEstatesServices>().Delete((Guid)realEstate2.Id);

            Assert.NotEqual(realEstate1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //vaja luua guid, mida hakkame  kasutama upate puhul
            var guid = new Guid("6c87369d-ced0-40c3-be4d-b71362bf0c27");
            RealEstateDto dto = MockRealEstateData();

            //vaja saada domenist andmed kätte
            RealEstate realEstate = new();
            realEstate.Id = Guid.Parse("6c87369d-ced0-40c3-be4d-b71362bf0c27");
            realEstate.Address = "asd1243";
            realEstate.SizeSqrM = 1500;
            realEstate.RoomCount = 100;
            realEstate.Floor = 3;
            realEstate.BuildingType = "skyscraper";
            realEstate.BuiltInYear = DateTime.Now;
            realEstate.CreatedAt = DateTime.Now.AddYears(1);

            //kasutan domaini andmeid
            await Svc<IRealEstatesServices>().Update(dto);

            Assert.Equal(realEstate.Id, guid);
            Assert.DoesNotMatch(realEstate.Address, dto.Address);
            Assert.DoesNotMatch(realEstate.Floor.ToString(), dto.Floor.ToString());
            Assert.NotEqual(realEstate.RoomCount, dto.RoomCount);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstatesServices>().Create(dto);

            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstatesServices>().Update(update);

            Assert.DoesNotMatch(result.Address, createRealEstate.Address);
            Assert.NotEqual(result.UpdatedAt, createRealEstate.UpdatedAt);
            //Assert.Equal(result.CreatedAt, createRealEstate.CreatedAt);
        }

        [Fact]
        //ei updeidi datat, kui id on null
        public async Task ShouldNot_UpdateRealEstate_WhenNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealestate = await Svc<IRealEstatesServices>().Create(dto);

            RealEstateDto nullUpdate = MockNullRealEstate();
            var result = await Svc<IRealEstatesServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }


        private RealEstateDto MockRealEstateData() //nö libaandmebaas
        {
            RealEstateDto realEstate = new()
            {
                Address = "asd",
                SizeSqrM = 123,
                RoomCount = 1,
                Floor = 2,
                BuildingType = "asd",
                BuiltInYear = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };

            return realEstate;
        }
        
      
        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Address = "asdasd",
                SizeSqrM = 123123,
                RoomCount = 55,
                Floor = 33,
                BuildingType = "asd",
                BuiltInYear = DateTime.Now.AddYears(1),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddYears(1),

            };
            return realEstate;
        }

        private RealEstateDto MockNullRealEstate()
        {
            RealEstateDto nullDto = new()
            {
                Id = null,
                Address = "Address123",
                SizeSqrM = 12.5f,
                RoomCount = 123,
                Floor = 123,
                BuildingType = "BuildingType123",
                BuiltInYear = DateTime.Now.AddYears(-1),
                CreatedAt= DateTime.Now.AddYears(-1),
                UpdatedAt= DateTime.Now.AddYears(-1),
            };
            return nullDto;
        }

    }
}
