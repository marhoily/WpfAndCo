namespace Sample
{
    public partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
            DataContext = AboutViewModel.Instance;
        }
    }
}