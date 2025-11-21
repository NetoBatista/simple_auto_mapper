using SimpleAutoMapper.Test.Model;

namespace SimpleAutoMapper.Test;

[TestClass]
public class ObjectMapperTest
{
    [TestMethod(DisplayName = "Should be able map object")]
    public void MapObject()
    {
        var source = new SourceModel
        {
            Id = 1,
            UniqueId = Guid.NewGuid(),
            Name = "Source",
            Nested = new SourceModel
            {
                Id = 2,
                UniqueId = Guid.NewGuid(),
                Name = "NestedSource",
                Nested = new SourceModel
                {
                    Id = 3,
                    UniqueId = Guid.NewGuid(),
                    Name = "NestedNestedSource"
                }
            }
        };

        // Act
        var destination = AutoMapper.Map<DestinationModel>(source);

        // Assert
        Assert.IsNotNull(destination);

        // Check value in same level 0
        Assert.AreEqual(source.Id, destination.Id);
        Assert.AreEqual(source.Name, destination.Name);
        Assert.AreEqual(source.UniqueId, destination.UniqueId);

        // Check value in same level 1
        Assert.AreEqual(source.Nested.Id, destination.Nested.Id);
        Assert.AreEqual(source.Nested.Name, destination.Nested.Name);
        Assert.AreEqual(source.Nested.UniqueId, destination.Nested.UniqueId);

        // Check value in same level 2
        Assert.AreEqual(source.Nested.Nested.Id, destination.Nested.Nested.Id);
        Assert.AreEqual(source.Nested.Nested.Name, destination.Nested.Nested.Name);
        Assert.AreEqual(source.Nested.Nested.UniqueId, destination.Nested.Nested.UniqueId);
    }
}
