using SimpleAutoMapper.Test.Model;

namespace SimpleAutoMapper.Test;

[TestClass]
public class CollectionMapperTest
{
    [TestMethod("Should be able map collection")]
    public void MapCollection()
    {
        var source = new List<SourceModel>
        {
            new SourceModel
            {
                IntList = [4, 5, 6],
                GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                EnumList = [StatusEnum.Inactive, StatusEnum.Active],
                StringArray = ["Three", "Four"],
                NestedClassList =
                [
                    new SourceModel
                    {
                        IntList = [4, 5, 6],
                        GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                        EnumList = [StatusEnum.Inactive, StatusEnum.Active],
                        StringArray = ["Three", "Four"],
                    }
                ],
                NestedClassArray =
                [
                    new SourceModel
                    {
                        IntList = [4, 5, 6],
                        GuidArray = [Guid.NewGuid(), Guid.NewGuid()],
                        EnumList = [StatusEnum.Inactive, StatusEnum.Active],
                        StringArray = ["Three", "Four"]
                    }
                ]
            }
        };

        // Act
        var destination = AutoMapper.Map<List<DestinationModel>>(source);

        // Assert
        Assert.IsNotNull(destination);
        Assert.IsNotNull(destination.FirstOrDefault());

        // Check lists and arrays for both levels
        CollectionAssert.AreEqual(source[0].IntList, destination[0].IntList);
        CollectionAssert.AreEqual(source[0].GuidArray, destination[0].GuidArray);
        CollectionAssert.AreEqual(source[0].EnumList, destination[0].EnumList);
        CollectionAssert.AreEqual(source[0].StringArray, destination[0].StringArray);

        // Check in NestedClassList
        CollectionAssert.AreEqual(source[0].NestedClassList[0].IntList, destination[0].NestedClassList[0].IntList);
        CollectionAssert.AreEqual(source[0].NestedClassList[0].GuidArray, destination[0].NestedClassList[0].GuidArray);
        CollectionAssert.AreEqual(source[0].NestedClassList[0].EnumList, destination[0].NestedClassList[0].EnumList);
        CollectionAssert.AreEqual(source[0].NestedClassList[0].StringArray, destination[0].NestedClassList[0].StringArray);

        // Check in NestedClassArray
        CollectionAssert.AreEqual(source[0].NestedClassArray[0].IntList, destination[0].NestedClassArray[0].IntList);
        CollectionAssert.AreEqual(source[0].NestedClassArray[0].GuidArray, destination[0].NestedClassArray[0].GuidArray);
        CollectionAssert.AreEqual(source[0].NestedClassArray[0].EnumList, destination[0].NestedClassArray[0].EnumList);
        CollectionAssert.AreEqual(source[0].NestedClassArray[0].StringArray, destination[0].NestedClassArray[0].StringArray);
    }
}
