namespace TvMaze.UnitTests
{
    public class CustomServiceScope : IServiceScope
    {
        public IServiceProvider ServiceProvider { get; set; }

        private IServiceCollection serviceCollection { get; set; }
        public CustomServiceScope()
        {
            serviceCollection = new ServiceCollection();
            ServiceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(serviceCollection);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
