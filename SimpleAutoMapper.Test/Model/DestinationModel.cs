namespace SimpleAutoMapper.Test.Model
{
    public class DestinationModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public StatusEnum Status { get; set; }
        public Guid UniqueId { get; set; }
        public int? NullableInt { get; set; }
        public DateTime Date { get; set; }
        public string ReadOnlyValue { get; } = "ReadOnly";
        public DestinationModel Nested { get; set; } = default!;
        public List<int> IntList { get; set; } = [];
        public Guid[] GuidArray { get; set; } = [];
        public List<StatusEnum> EnumList { get; set; } = [];
        public string[] StringArray { get; set; } = [];
        public List<DestinationModel> NestedClassList { get; set; } = [];
        public DestinationModel[] NestedClassArray { get; set; } = [];
    }
}
