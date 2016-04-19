using Microsoft.Data.Entity.Metadata;

namespace Generator.DataContext
{
    partial class Tracking
    {
        private readonly IModel _model;

        public Tracking(IModel model)
        {
            _model = model;
        }
    }
}