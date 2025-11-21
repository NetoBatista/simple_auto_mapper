using SimpleAutoMapper.Test.Model;

namespace SimpleAutoMapper.Test
{
    [TestClass]
    public class AutoMapperTest
    {
        [TestMethod(DisplayName = "Should be able copy all properties with three level of recursion")]
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
                        new SourceModel
                        {
                            Id = 7,
                            Name = "NestedList7",
                            Value = 100.50m,
                            Status = StatusEnum.Active,
                            UniqueId = Guid.NewGuid(),
                            NullableInt = 5,
                            Date = DateTime.Now,
                            IntList = [1, 2, 3],
                            GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                            EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                            StringArray = ["One", "Two"]
                        },
                        new SourceModel
                        {
                            Id = 8,
                            Name = "NestedList8",
                            Value = 100.50m,
                            Status = StatusEnum.Active,
                            UniqueId = Guid.NewGuid(),
                            NullableInt = 5,
                            Date = DateTime.Now,
                            IntList = [1, 2, 3],
                            GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                            EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                            StringArray = ["One", "Two"]
                        },
                    ],
                    NestedClassArray =
                    [
                        new SourceModel
                        {
                            Id = 9,
                            Name = "NestedInternalArray9",
                            Value = 100.50m,
                            Status = StatusEnum.Active,
                            UniqueId = Guid.NewGuid(),
                            NullableInt = 5,
                            Date = DateTime.Now,
                            IntList = [1, 2, 3],
                            GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                            EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                            StringArray = ["One", "Two"]
                        },
                        new SourceModel
                        {
                            Id = 10,
                            Name = "NestedInternalArray10",
                            Value = 100.50m,
                            Status = StatusEnum.Active,
                            UniqueId = Guid.NewGuid(),
                            NullableInt = 5,
                            Date = DateTime.Now,
                            IntList = [1, 2, 3],
                            GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                            EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                            StringArray = ["One", "Two"]
                        },
                    ]
                },
                IntList = [1, 2, 3],
                GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                StringArray = ["One", "Two"],
                NestedClassList =
                [
                    new SourceModel
                    {
                        Id = 11,
                        Name = "NestedArray11",
                        Value = 100.50m,
                        Status = StatusEnum.Active,
                        UniqueId = Guid.NewGuid(),
                        NullableInt = 5,
                        Date = DateTime.Now,
                        IntList = [1, 2, 3],
                        GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                        EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                        StringArray = ["One", "Two"]
                    },
                    new SourceModel
                    {
                        Id = 12,
                        Name = "NestedArray12",
                        Value = 100.50m,
                        Status = StatusEnum.Active,
                        UniqueId = Guid.NewGuid(),
                        NullableInt = 5,
                        Date = DateTime.Now,
                        IntList = [1, 2, 3],
                        GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                        EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                        StringArray = ["One", "Two"]
                    },
                ],
                NestedClassArray =
                [
                    new SourceModel
                    {
                        Id = 13,
                        Name = "NestedArray13",
                        Value = 100.50m,
                        Status = StatusEnum.Active,
                        UniqueId = Guid.NewGuid(),
                        NullableInt = 5,
                        Date = DateTime.Now,
                        IntList = [1, 2, 3],
                        GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                        EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                        StringArray = ["One", "Two"]
                    },
                    new SourceModel
                    {
                        Id = 10,
                        Name = "NestedArray10",
                        Value = 100.50m,
                        Status = StatusEnum.Active,
                        UniqueId = Guid.NewGuid(),
                        NullableInt = 5,
                        Date = DateTime.Now,
                        IntList = [1, 2, 3],
                        GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                        EnumList = [StatusEnum.Active, StatusEnum.Inactive],
                        StringArray = ["One", "Two"]
                    },
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

            // Check NestedClassList
            Assert.HasCount(source.NestedClassList.Count, destination.NestedClassList);
            Assert.AreEqual(source.NestedClassList[0].Id, destination.NestedClassList[0].Id);
            Assert.AreEqual(source.NestedClassList[0].Name, destination.NestedClassList[0].Name);
            Assert.AreEqual(source.NestedClassList[0].Value, destination.NestedClassList[0].Value);
            Assert.AreEqual(source.NestedClassList[0].Status, destination.NestedClassList[0].Status);
            Assert.AreEqual(source.NestedClassList[0].UniqueId, destination.NestedClassList[0].UniqueId);
            Assert.AreEqual(source.NestedClassList[0].NullableInt, destination.NestedClassList[0].NullableInt);
            Assert.AreEqual(source.NestedClassList[0].Date, destination.NestedClassList[0].Date);
            Assert.AreEqual("ReadOnly", destination.NestedClassList[0].ReadOnlyValue);

            Assert.AreEqual(source.NestedClassList[1].Id, destination.NestedClassList[1].Id);
            Assert.AreEqual(source.NestedClassList[1].Name, destination.NestedClassList[1].Name);
            Assert.AreEqual(source.NestedClassList[1].Value, destination.NestedClassList[1].Value);
            Assert.AreEqual(source.NestedClassList[1].Status, destination.NestedClassList[1].Status);
            Assert.AreEqual(source.NestedClassList[1].UniqueId, destination.NestedClassList[1].UniqueId);
            Assert.AreEqual(source.NestedClassList[1].NullableInt, destination.NestedClassList[1].NullableInt);
            Assert.AreEqual(source.NestedClassList[1].Date, destination.NestedClassList[1].Date);
            Assert.AreEqual("ReadOnly", destination.NestedClassList[1].ReadOnlyValue);

            // Check NestedClassArray
            Assert.HasCount(source.NestedClassArray.Length, destination.NestedClassArray);
            Assert.AreEqual(source.NestedClassArray[0].Id, destination.NestedClassArray[0].Id);
            Assert.AreEqual(source.NestedClassArray[0].Name, destination.NestedClassArray[0].Name);
            Assert.AreEqual(source.NestedClassArray[0].Value, destination.NestedClassArray[0].Value);
            Assert.AreEqual(source.NestedClassArray[0].Status, destination.NestedClassArray[0].Status);
            Assert.AreEqual(source.NestedClassArray[0].UniqueId, destination.NestedClassArray[0].UniqueId);
            Assert.AreEqual(source.NestedClassArray[0].NullableInt, destination.NestedClassArray[0].NullableInt);
            Assert.AreEqual(source.NestedClassArray[0].Date, destination.NestedClassArray[0].Date);
            Assert.AreEqual("ReadOnly", destination.NestedClassArray[0].ReadOnlyValue);

            Assert.AreEqual(source.NestedClassArray[1].Id, destination.NestedClassArray[1].Id);
            Assert.AreEqual(source.NestedClassArray[1].Name, destination.NestedClassArray[1].Name);
            Assert.AreEqual(source.NestedClassArray[1].Value, destination.NestedClassArray[1].Value);
            Assert.AreEqual(source.NestedClassArray[1].Status, destination.NestedClassArray[1].Status);
            Assert.AreEqual(source.NestedClassArray[1].UniqueId, destination.NestedClassArray[1].UniqueId);
            Assert.AreEqual(source.NestedClassArray[1].NullableInt, destination.NestedClassArray[1].NullableInt);
            Assert.AreEqual(source.NestedClassArray[1].Date, destination.NestedClassArray[1].Date);
            Assert.AreEqual("ReadOnly", destination.NestedClassArray[1].ReadOnlyValue);

            // Nested objects within the second level
            CollectionAssert.AreEqual(source.Nested.IntList, destination.Nested.IntList);
            CollectionAssert.AreEqual(source.Nested.GuidArray, destination.Nested.GuidArray);
            CollectionAssert.AreEqual(source.Nested.EnumList, destination.Nested.EnumList);
            CollectionAssert.AreEqual(source.Nested.StringArray, destination.Nested.StringArray);

            // Check NestedClassList
            Assert.HasCount(source.Nested.NestedClassList.Count, destination.NestedClassList);
            Assert.AreEqual(source.Nested.NestedClassList[0].Id, destination.Nested.NestedClassList[0].Id);
            Assert.AreEqual(source.Nested.NestedClassList[0].Name, destination.Nested.NestedClassList[0].Name);
            Assert.AreEqual(source.Nested.NestedClassList[0].Value, destination.Nested.NestedClassList[0].Value);
            Assert.AreEqual(source.Nested.NestedClassList[0].Status, destination.Nested.NestedClassList[0].Status);
            Assert.AreEqual(source.Nested.NestedClassList[0].UniqueId, destination.Nested.NestedClassList[0].UniqueId);
            Assert.AreEqual(source.Nested.NestedClassList[0].NullableInt, destination.Nested.NestedClassList[0].NullableInt);
            Assert.AreEqual(source.Nested.NestedClassList[0].Date, destination.Nested.NestedClassList[0].Date);
            Assert.AreEqual("ReadOnly", destination.Nested.NestedClassList[0].ReadOnlyValue);

            Assert.AreEqual(source.Nested.NestedClassList[1].Id, destination.Nested.NestedClassList[1].Id);
            Assert.AreEqual(source.Nested.NestedClassList[1].Name, destination.Nested.NestedClassList[1].Name);
            Assert.AreEqual(source.Nested.NestedClassList[1].Value, destination.Nested.NestedClassList[1].Value);
            Assert.AreEqual(source.Nested.NestedClassList[1].Status, destination.Nested.NestedClassList[1].Status);
            Assert.AreEqual(source.Nested.NestedClassList[1].UniqueId, destination.Nested.NestedClassList[1].UniqueId);
            Assert.AreEqual(source.Nested.NestedClassList[1].NullableInt, destination.Nested.NestedClassList[1].NullableInt);
            Assert.AreEqual(source.Nested.NestedClassList[1].Date, destination.Nested.NestedClassList[1].Date);
            Assert.AreEqual("ReadOnly", destination.Nested.NestedClassList[1].ReadOnlyValue);

            // Check NestedClassArray
            Assert.HasCount(source.Nested.NestedClassArray.Length, destination.Nested.NestedClassArray);
            Assert.AreEqual(source.Nested.NestedClassArray[0].Id, destination.Nested.NestedClassArray[0].Id);
            Assert.AreEqual(source.Nested.NestedClassArray[0].Name, destination.Nested.NestedClassArray[0].Name);
            Assert.AreEqual(source.Nested.NestedClassArray[0].Value, destination.Nested.NestedClassArray[0].Value);
            Assert.AreEqual(source.Nested.NestedClassArray[0].Status, destination.Nested.NestedClassArray[0].Status);
            Assert.AreEqual(source.Nested.NestedClassArray[0].UniqueId, destination.Nested.NestedClassArray[0].UniqueId);
            Assert.AreEqual(source.Nested.NestedClassArray[0].NullableInt, destination.Nested.NestedClassArray[0].NullableInt);
            Assert.AreEqual(source.Nested.NestedClassArray[0].Date, destination.Nested.NestedClassArray[0].Date);
            Assert.AreEqual("ReadOnly", destination.Nested.NestedClassArray[0].ReadOnlyValue);

            Assert.AreEqual(source.Nested.NestedClassArray[1].Id, destination.Nested.NestedClassArray[1].Id);
            Assert.AreEqual(source.Nested.NestedClassArray[1].Name, destination.Nested.NestedClassArray[1].Name);
            Assert.AreEqual(source.Nested.NestedClassArray[1].Value, destination.Nested.NestedClassArray[1].Value);
            Assert.AreEqual(source.Nested.NestedClassArray[1].Status, destination.Nested.NestedClassArray[1].Status);
            Assert.AreEqual(source.Nested.NestedClassArray[1].UniqueId, destination.Nested.NestedClassArray[1].UniqueId);
            Assert.AreEqual(source.Nested.NestedClassArray[1].NullableInt, destination.Nested.NestedClassArray[1].NullableInt);
            Assert.AreEqual(source.Nested.NestedClassArray[1].Date, destination.Nested.NestedClassArray[1].Date);
            Assert.AreEqual("ReadOnly", destination.Nested.NestedClassArray[1].ReadOnlyValue);
        }
    }
}