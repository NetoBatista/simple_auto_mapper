using SimpleAutoMapper.Test.Model;

namespace SimpleAutoMapper.Test
{
    [TestClass]
    public class AutoMapperTest
    {
        [TestMethod("Should be able copy all properties with three level of recursion")]
        public void MapFullObject()
        {
            // Arrange
            var source = new SourceModel
            {
                Id = 1,
                Name = "SourceName",
                Value = 100.50m,
                Status = StatusEnum.Active,
                UniqueId = Guid.NewGuid(),
                NullableInt = 5,
                Date = DateTime.Now,
                Nested = new SourceModel
                {
                    Id = 2,
                    Name = "NestedSource",
                    Value = 50.75m,
                    Status = StatusEnum.Inactive,
                    UniqueId = Guid.NewGuid(),
                    NullableInt = 10,
                    Date = DateTime.Now.AddDays(-1),
                    Nested = new SourceModel
                    {
                        Id = 3,
                        Name = "NestedLevel3",
                        Value = 25.25m,
                        Status = StatusEnum.Active,
                        UniqueId = Guid.NewGuid(),
                        NullableInt = 15,
                        Date = DateTime.Now.AddDays(-2)
                    },
                    IntList = [4, 5, 6],
                    GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                    EnumList = [StatusEnum.Inactive, StatusEnum.Active],
                    StringArray = ["Three", "Four"],
                    NestedClassList =
                    [
                        new SourceModel { Id = 7, Name = "NestedListSourceLevel3-1" },
                        new SourceModel { Id = 8, Name = "NestedListSourceLevel3-2" }
                    ],
                    NestedClassArray =
                    [
                        new SourceModel { Id = 9, Name = "NestedArraySourceLevel3-1" },
                        new SourceModel { Id = 10, Name = "NestedArraySourceLevel3-2" }
                    ]
                },
                IntList = [1, 2, 3],
                GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                StringArray = ["One", "Two"],
                NestedClassList =
                [
                    new SourceModel { Id = 11, Name = "NestedListSource1" },
                    new SourceModel { Id = 12, Name = "NestedListSource2" }
                ],
                NestedClassArray =
                [
                    new SourceModel { Id = 13, Name = "NestedArraySource1" },
                    new SourceModel { Id = 14, Name = "NestedArraySource2" }
                ]
            };

            // Act
            var destination = AutoMapper.Map<DestinationModel>(source);

            // Assert
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Id, destination.Id);
            Assert.AreEqual(source.Name, destination.Name);
            Assert.AreEqual(source.Value, destination.Value);
            Assert.AreEqual(source.Status, destination.Status);
            Assert.AreEqual(source.UniqueId, destination.UniqueId);
            Assert.AreEqual(source.NullableInt, destination.NullableInt);
            Assert.AreEqual(source.Date, destination.Date);
            Assert.AreEqual("ReadOnly", destination.ReadOnlyValue);

            // First level of recursion
            Assert.IsNotNull(destination.Nested);
            Assert.AreEqual(source.Nested.Id, destination.Nested.Id);
            Assert.AreEqual(source.Nested.Name, destination.Nested.Name);
            Assert.AreEqual(source.Nested.Value, destination.Nested.Value);
            Assert.AreEqual(source.Nested.Status, destination.Nested.Status);

            // Second level of recursion
            Assert.IsNotNull(destination.Nested.Nested);
            Assert.AreEqual(source.Nested.Nested.Id, destination.Nested.Nested.Id);
            Assert.AreEqual(source.Nested.Nested.Name, destination.Nested.Nested.Name);
            Assert.AreEqual(source.Nested.Nested.Value, destination.Nested.Nested.Value);
            Assert.AreEqual(source.Nested.Nested.Status, destination.Nested.Nested.Status);

            // Check lists and arrays for both levels
            CollectionAssert.AreEqual(source.IntList, destination.IntList);
            CollectionAssert.AreEqual(source.GuidArray, destination.GuidArray);
            CollectionAssert.AreEqual(source.EnumList, destination.EnumList);
            CollectionAssert.AreEqual(source.StringArray, destination.StringArray);
            Assert.AreEqual(source.NestedClassList.Count, destination.NestedClassList.Count);
            Assert.AreEqual(source.NestedClassList[0].Id, destination.NestedClassList[0].Id);
            Assert.AreEqual(source.NestedClassList[1].Id, destination.NestedClassList[1].Id);
            Assert.AreEqual(source.NestedClassArray.Length, destination.NestedClassArray.Length);
            Assert.AreEqual(source.NestedClassArray[0].Id, destination.NestedClassArray[0].Id);
            Assert.AreEqual(source.NestedClassArray[1].Id, destination.NestedClassArray[1].Id);

            // Nested objects within the second level
            CollectionAssert.AreEqual(source.Nested.IntList, destination.Nested.IntList);
            CollectionAssert.AreEqual(source.Nested.GuidArray, destination.Nested.GuidArray);
            CollectionAssert.AreEqual(source.Nested.EnumList, destination.Nested.EnumList);
            CollectionAssert.AreEqual(source.Nested.StringArray, destination.Nested.StringArray);
            Assert.AreEqual(source.Nested.NestedClassList.Count, destination.Nested.NestedClassList.Count);
            Assert.AreEqual(source.Nested.NestedClassList[0].Id, destination.Nested.NestedClassList[0].Id);
            Assert.AreEqual(source.Nested.NestedClassList[1].Id, destination.Nested.NestedClassList[1].Id);
            Assert.AreEqual(source.Nested.NestedClassArray.Length, destination.Nested.NestedClassArray.Length);
            Assert.AreEqual(source.Nested.NestedClassArray[0].Id, destination.Nested.NestedClassArray[0].Id);
            Assert.AreEqual(source.Nested.NestedClassArray[1].Id, destination.Nested.NestedClassArray[1].Id);
        }
    }
}