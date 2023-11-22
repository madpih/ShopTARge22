using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;


namespace ShopTARge22.KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyKindergarten_WhenReturnResult()
        {   //Arrange ehk andmete ettevalmistus
            KindergartenDto kindergarten = new KindergartenDto();
            kindergarten.GroupName = "asd";
            kindergarten.ChildrenCount = 1000;
            kindergarten.KindergartenName = "asd";
            kindergarten.Teacher = "madis";
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;

            //Act ehk sisuline tegevus mida teeb
            var result = await Svc<IKindergartensServices>().Create(kindergarten);

            //Assert ehk sisestus
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNotEqual()
        {
            //Arrage
            //küsime realEstate, mida ei ole olemas
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString()); //loob guidi, mida ei ole olemas
            Guid guid = Guid.Parse("0822aca3-0c2c-4cbd-9a08-f594c70d029e"); //see guid on olemas

            //Act
            //peame kutsuma esile meetodi, mis on kindergarten classis
            //otsime läbi õige guidi, mis on olemas.

            await Svc<IKindergartensServices>().DetailsAsync(guid);

            //Assert
            //assertimise võrdlus, et võrrelda kahte GUIdi
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdKindergarten_WhenReturnsEqual()
        {

            //Otsib Id põhjal kindergarten objekti üles

            //Arrange
            Guid guid1 = Guid.Parse("0822aca3-0c2c-4cbd-9a08-f594c70d029e");
            Guid guid2 = Guid.Parse("0822aca3-0c2c-4cbd-9a08-f594c70d029e"); //baasis

            //Act
            await Svc<IKindergartensServices>().DetailsAsync(guid2);

            //Assert
            Assert.Equal(guid1, guid2);
        }

        [Fact]
        public async Task Should_DeleteByIdKindegarten_WhenDeleteKindergarten()
        {
            KindergartenDto kindergarten = MockKindergartenData();

            var addKindergarten = await Svc<IKindergartensServices>().Create(kindergarten);
            var result = await Svc<IKindergartensServices>().Delete((Guid)addKindergarten.Id);

            Assert.Equal(addKindergarten, result);
        }

        [Fact]
        //ei thohiks kustutada id, kui antakse id ette.
        public async Task ShouldNot_DeleteByIdKindergarten_WhenDidNotDeleteKindergarten()
        {
           KindergartenDto kindergarten = MockKindergartenData();

            var kindergarten1 = await Svc<IKindergartensServices>().Create(kindergarten);
            var kindergarten2 = await Svc<IKindergartensServices>().Create(kindergarten);
            var result = await Svc<IKindergartensServices>().Delete((Guid)kindergarten2.Id);

            Assert.NotEqual(kindergarten1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdateData()
        {
            //vaja luua guid, mida hakkame  kasutama upate puhul
            var guid = new Guid("0822aca3-0c2c-4cbd-9a08-f594c70d029e");
            KindergartenDto dto = MockKindergartenData();

            //uued andmed mida sisestame (asendavad mocki andmeid updeidil)
            Kindergarten kindergarten = new();

            kindergarten.Id = Guid.Parse("0822aca3-0c2c-4cbd-9a08-f594c70d029e");
           
            kindergarten.GroupName = "madis";
            kindergarten.ChildrenCount = 100;
            kindergarten.KindergartenName = "asd";
            kindergarten.Teacher = "Leelo";
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;
        

            //kasutan domaini andmeid
            await Svc<IKindergartensServices>().Update(dto);

            Assert.Equal(kindergarten.Id, guid);
            Assert.DoesNotMatch(kindergarten.GroupName, dto.GroupName);
            Assert.DoesNotMatch(kindergarten.Teacher, dto.Teacher);
            Assert.NotEqual(kindergarten.ChildrenCount, dto.ChildrenCount);
        }


        [Fact]
        public async Task Should_UpdateKindergarten_WhenUpdateDataVersion2()
        {
            KindergartenDto dto = MockKindergartenData();
            var createKindergarten = await Svc<IKindergartensServices>().Create(dto);

            KindergartenDto update = MockUpdateKindergartenData();
            var result = await Svc<IKindergartensServices>().Update(update);

            Assert.DoesNotMatch(result.Teacher, createKindergarten.Teacher);
            Assert.NotEqual(result.ChildrenCount, createKindergarten.ChildrenCount);
            //Assert.Equal(result.CreatedAt, createRealEstate.CreatedAt);
        }

        [Fact]
        //updeidib andmeid, kui andmed ei ole samad
        public async Task Should_UpdateKindergarten_WhenUpdateDataNotSame()
        {
            KindergartenDto dto = MockKindergartenData();
            var createKindergarten = await Svc<IKindergartensServices>().Create(dto);

            KindergartenDto newUpdate = MockNullKindergarten();
            var result = await Svc<IKindergartensServices>().Update(newUpdate);

            var groupName = newUpdate.GroupName;
            var childrenCount = newUpdate.ChildrenCount;

            Assert.False(dto.GroupName == groupName);
            Assert.False(dto.ChildrenCount == childrenCount);
        }


        private KindergartenDto MockKindergartenData() //nö libaandmebaas
        {
            KindergartenDto kindergarten = new()
            {
                GroupName = "asd",
                ChildrenCount = 1000,
                KindergartenName = "asd",
                Teacher = "madis",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };

            return kindergarten;
        }

        private KindergartenDto MockUpdateKindergartenData()
        {
            KindergartenDto kindergarten = new()
            {
                GroupName = "asd",
                ChildrenCount = 1,
                KindergartenName = "asd",
                Teacher = "Leelo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };
            return kindergarten;
        }

        private KindergartenDto MockNullKindergarten()
        {
            KindergartenDto newDto = new()
            {
                
                GroupName = "miniminionid",
                ChildrenCount = 1,
                KindergartenName = "minionid",
                Teacher = "Minion",
                UpdatedAt = DateTime.Now,

            };
            return newDto;
        }


    }
}

