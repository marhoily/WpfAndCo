using Microsoft.Data.Entity.Metadata;

namespace Generator.DataContext
{
    partial class Write
    {
        private readonly IModel _model;

        public Write(IModel model) {
            _model = model;
        }
    }
}