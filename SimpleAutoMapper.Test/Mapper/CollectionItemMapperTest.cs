using SimpleAutoMapper.Test.Model;

namespace SimpleAutoMapper.Test;

[TestClass]
public class CollectionItemMapperTest
{
    [TestMethod(DisplayName = "Should be able map item of collection")]
    public void MapItem()
    {
        var source = new SourceModel
        {
            IntList = [4, 5, 6],
            GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
            EnumList = [StatusEnum.Inactive, StatusEnum.Active],
            StringArray = ["Three", "Four"],
            NestedClassList =
            [
                new SourceModel
                {
                    Id = 1,
                    Name = "NestedListSource1",
                    IntList = [4, 5, 6],
                    GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                    EnumList = [StatusEnum.Inactive, StatusEnum.Active],
                    StringArray = ["Three", "Four"],
                    NestedClassList =
                    [
                        new SourceModel { Id = 2, Name = "NestedListSource1" },
                        new SourceModel { Id = 3, Name = "NestedListSource2" }
                    ],
                    NestedClassArray =
                    [
                        new SourceModel { Id = 4, Name = "NestedArraySource1" },
                        new SourceModel { Id = 5, Name = "NestedArraySource2" }
                    ]
                }
            ],
            NestedClassArray =
            [
                new SourceModel
                {
                    Id = 1,
                    Name = "NestedListSource1",
                    IntList = [4, 5, 6],
                    GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                    EnumList = [StatusEnum.Inactive, StatusEnum.Active],
                    StringArray = ["Three", "Four"],
                    NestedClassList =
                    [
                        new SourceModel { Id = 2, Name = "NestedListSource1" },
                        new SourceModel { Id = 3, Name = "NestedListSource2" }
                    ],
                    NestedClassArray =
                    [
                        new SourceModel { Id = 4, Name = "NestedArraySource1" },
                        new SourceModel { Id = 5, Name = "NestedArraySource2" }
                    ]
                }
            ]
        };

        // Act
        var destination = AutoMapper.Map<DestinationModel>(source);

        // Assert
        Assert.IsNotNull(destination);

        // Check lists and arrays for both levels
        CollectionAssert.AreEqual(source.IntList, destination.IntList);
        CollectionAssert.AreEqual(source.GuidArray, destination.GuidArray);
        CollectionAssert.AreEqual(source.EnumList, destination.EnumList);
        CollectionAssert.AreEqual(source.StringArray, destination.StringArray);

        // Check in NestedClassList
        Assert.HasCount(source.NestedClassList.Count, destination.NestedClassList);
        Assert.AreEqual(source.NestedClassList[0].Id, destination.NestedClassList[0].Id);
        Assert.AreEqual(source.NestedClassList[0].Name, destination.NestedClassList[0].Name);

        Assert.AreEqual(source.NestedClassList[0].NestedClassList[0].Id, destination.NestedClassList[0].NestedClassList[0].Id);
        Assert.AreEqual(source.NestedClassList[0].NestedClassList[0].Name, destination.NestedClassList[0].NestedClassList[0].Name);

        Assert.AreEqual(source.NestedClassList[0].NestedClassArray[0].Id, destination.NestedClassList[0].NestedClassArray[0].Id);
        Assert.AreEqual(source.NestedClassList[0].NestedClassArray[0].Name, destination.NestedClassList[0].NestedClassArray[0].Name);

        CollectionAssert.AreEqual(source.NestedClassList[0].IntList, destination.NestedClassList[0].IntList);
        CollectionAssert.AreEqual(source.NestedClassList[0].GuidArray, destination.NestedClassList[0].GuidArray);
        CollectionAssert.AreEqual(source.NestedClassList[0].EnumList, destination.NestedClassList[0].EnumList);
        CollectionAssert.AreEqual(source.NestedClassList[0].StringArray, destination.NestedClassList[0].StringArray);

        // Check in NestedClassArray
        Assert.HasCount(source.NestedClassArray.Length, destination.NestedClassArray);
        Assert.AreEqual(source.NestedClassArray[0].Id, destination.NestedClassArray[0].Id);
        Assert.AreEqual(source.NestedClassArray[0].Name, destination.NestedClassArray[0].Name);

        Assert.AreEqual(source.NestedClassArray[0].NestedClassList[0].Id, destination.NestedClassArray[0].NestedClassList[0].Id);
        Assert.AreEqual(source.NestedClassArray[0].NestedClassList[0].Name, destination.NestedClassArray[0].NestedClassList[0].Name);

        Assert.AreEqual(source.NestedClassArray[0].NestedClassArray[0].Id, destination.NestedClassArray[0].NestedClassArray[0].Id);
        Assert.AreEqual(source.NestedClassArray[0].NestedClassArray[0].Name, destination.NestedClassArray[0].NestedClassArray[0].Name);

        CollectionAssert.AreEqual(source.NestedClassArray[0].IntList, destination.NestedClassArray[0].IntList);
        CollectionAssert.AreEqual(source.NestedClassArray[0].GuidArray, destination.NestedClassArray[0].GuidArray);
        CollectionAssert.AreEqual(source.NestedClassArray[0].EnumList, destination.NestedClassArray[0].EnumList);
        CollectionAssert.AreEqual(source.NestedClassArray[0].StringArray, destination.NestedClassArray[0].StringArray);

    }
}
