using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace DIClassLibrary
{
    public sealed class ApplicationContainer : IAsyncDisposable, IDisposable
    {
        private readonly IHost _host;
        
        internal ApplicationContainer(IHost host)
        {
            _host = host;
            ApplicationBuilder = new ApplicationBuilder(host.Services);
        }

        /// <summary>
        /// The application's configured services.
        /// </summary>
        public IServiceProvider Services => _host.Services;

        internal ApplicationBuilder ApplicationBuilder { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContainerBuilder"/> class with preconfigured defaults.
        /// </summary>
        /// <returns>The <see cref="ApplicationContainerBuilder"/>.</returns>
        public static ApplicationContainerBuilder CreateBuilder() =>
            new(new());

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContainerBuilder"/> class with preconfigured defaults.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>The <see cref="ApplicationContainerBuilder"/>.</returns>
        public static ApplicationContainerBuilder CreateBuilder(string[] args) =>
            new(new() { Args = args });

        /// <summary>
        /// Disposes the application.
        /// </summary>
        void IDisposable.Dispose() => _host.Dispose();

        /// <summary>
        /// Disposes the application.
        /// </summary>
        public ValueTask DisposeAsync() => ((IAsyncDisposable)_host).DisposeAsync();
    }
}