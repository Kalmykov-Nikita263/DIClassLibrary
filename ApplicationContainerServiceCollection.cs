using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DIClassLibrary
{
    public class ApplicationContainerServiceCollection : IServiceCollection
    {
        private IServiceCollection _services = new ServiceCollection();

        public List<ServiceDescriptor> HostedServices { get; private set; } = new();

        public bool TrackHostedServices { get; set; }

        public ServiceDescriptor this[int index]
        {
            get => _services[index];
            set
            {
                CheckServicesAccess();
                _services[index] = value;
            }
        }

        public int Count => _services.Count;

        public bool IsReadOnly { get; set; }

        public IServiceCollection InnerCollection
        {
            get => _services;
            set
            {
                CheckServicesAccess();
                _services = value;
            }
        }

        public void Add(ServiceDescriptor item)
        {
            CheckServicesAccess();

            if (TrackHostedServices && item.ServiceType == typeof(IHostedService))
            {
                HostedServices.Add(item);
            }
            else
            {
                _services.Add(item);
            }
        }

        public void Clear()
        {
            CheckServicesAccess();

            _services.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _services.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _services.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return _services.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            CheckServicesAccess();

            _services.Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            CheckServicesAccess();

            return _services.Remove(item);
        }

        public void RemoveAt(int index)
        {
            CheckServicesAccess();

            _services.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void CheckServicesAccess()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("Cannot modify ServiceCollection after application is built.");
            }
        }
    }
}
