// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Factories;

namespace Castle.Windsor.Service.Replacement.UnitTest.Helpers
{
    internal class ServiceWithTypedFactory : IDisposable
    {
        private readonly IDependencyFactory _factory;

        public IDependency Dependency1 { get; }
        public IDependency Dependency2 { get; }
        public IDependency Dependency3 { get; }

        public ServiceWithTypedFactory(IDependencyFactory factory)
        {
            _factory = factory;

            Dependency1 = factory.Create("DependencyImpl1");
            Dependency2 = factory.Create("DependencyImpl2");
            Dependency3 = factory.Create("DependencyImpl3");
        }

        public void Dispose()
        {
            _factory.Release(Dependency1);
            _factory.Release(Dependency2);
            _factory.Release(Dependency3);
        }
    }
}