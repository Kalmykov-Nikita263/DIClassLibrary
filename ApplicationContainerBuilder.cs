using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DIClassLibrary
{
    /// <summary>
    /// A builder for applications and services.
    /// </summary>
    public sealed class ApplicationContainerBuilder
    {
        private readonly HostBuilder _hostBuilder = new();

        private readonly ApplicationContainerServiceCollection _services = new();

        private ApplicationContainer _builtApplication;

        internal ApplicationContainerBuilder(ApplicationContainerOptions options) => Services = _services;

        /// <summary>
        /// A collection of services for the application to compose. This is useful for adding user provided or framework provided services.
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Builds the <see cref="ApplicationContainer"/>.
        /// </summary>
        /// <returns>A configured <see cref="ApplicationContainer"/>.</returns>
        public ApplicationContainer Build()
        {
            // Copy the services that were added via WebApplicationBuilder.Services into the final IServiceCollection
            _hostBuilder.ConfigureServices((context, services) =>
            {
                foreach (var s in _services)
                    services.Add(s);

                foreach (var s in _services.HostedServices)
                    services.Add(s);

                // Clear the hosted services list out
                _services.HostedServices.Clear();

                // Add any services to the user visible service collection so that they are observable
                _services.InnerCollection = services;
            });

            _builtApplication = new ApplicationContainer(_hostBuilder.Build());

            // Mark the service collection as read-only to prevent future modifications
            _services.IsReadOnly = true;

            return _builtApplication;
        }
    }
}