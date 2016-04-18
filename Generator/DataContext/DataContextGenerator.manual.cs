using Microsoft.Data.Entity.Metadata;

namespace Generator.DataContext
{
    partial class DataContextGenerator
    {
        private readonly IModel _model;

        public DataContextGenerator(IModel model)
        {
            _model = model;
        }
    }
}