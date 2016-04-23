using Xunit;

namespace Generator
{
    public sealed class ModelGenerator : ITransformer
    {
        [Fact]
        public void Test()
        {
            var genHierarchy = Generate(new Model());
        }

        public static GenHierarchy Generate(Model model)
        {
            return new HierarchyBuilder("../../Output/sample.proj", "Generated") {
                new NodeBuilder<ModelGenerator>(model) {
                    new NodeBuilder<CompanyGenerator> {
                        new NodeBuilder<EmployeeGenerator>()
                    } } }
                .With((Model m) => m.Companies)
                .With((Company c) => c.Employees)
                .Build();
        }

        public string Name => "model.cs";
        public string TransformText() => "blah";
    }
}
