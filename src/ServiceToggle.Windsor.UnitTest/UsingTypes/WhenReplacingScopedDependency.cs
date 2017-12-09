// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Linq;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;
using Castle.MicroKernel.Registration;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace ServiceToggle.Windsor.UnitTest.UsingTypes
{
    internal class WhenReplacingScopedDependency : GivenWhenThenTest
    {
        private class CustomScopeAccessor : IScopeAccessor
        {
            private readonly ILifetimeScope _scope = new DefaultLifetimeScope();

            public void Dispose()
            {
                _scope.Dispose();
            }

            public ILifetimeScope GetScope(CreationContext context)
            {
                return _scope;
            }
        }

        protected override void Given()
        {
            Container.Register(
                Component
                    .For<IDependency>()
                    .ImplementedBy<DependencyImpl1>()
                    .LifestyleScoped(typeof(CustomScopeAccessor)),
                Component
                    .For<ServiceWithInterfaceDependency>());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement.For(typeof(IDependency)).ReplacedBy(typeof(DependencyImpl3)));
        }

        [Then]
        public void ShouldReplaceInjectedService()
        {
            var service = Container.Resolve<ServiceWithInterfaceDependency>();

            Assert.AreEqual(typeof(DependencyImpl3), service.Dependency.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldGetLifestyleFromServiceToReplace()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(IDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(DependencyImpl3));

            Assert.AreEqual(LifestyleType.Scoped, handler.ComponentModel.LifestyleType);
        }

        [Then]
        public void ShouldGetScopeAccessorFromServiceToReplace()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(IDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(DependencyImpl3));

            var scopeAccessorType = (Type) handler.ComponentModel.ExtendedProperties[Constants.ScopeAccessorType];

            Assert.AreEqual(typeof(CustomScopeAccessor), scopeAccessorType);
        }
    }
}