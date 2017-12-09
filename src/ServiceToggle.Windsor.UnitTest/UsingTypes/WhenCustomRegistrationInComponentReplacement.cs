// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Registration;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace ServiceToggle.Windsor.UnitTest.UsingTypes
{
    [TestFixture]
    internal class WhenCustomRegistrationInComponentReplacement : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Register(
                Component
                    .For<IDependency>()
                    .ImplementedBy<DependencyImpl1>()
                    .LifestylePerThread(),
                Component
                    .For<ServiceWithInterfaceDependency>());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement
                    .For(typeof(IDependency))
                    .ReplacedBy(
                        typeof(DependencyImpl2),
                        Component
                        .For(typeof(IDependency))
                        .ImplementedBy(typeof(DependencyImpl2))
                        .LifestyleTransient()));
        }

        [Then]
        public void ShouldReplaceInjectedService()
        {
            var service = Container.Resolve<ServiceWithInterfaceDependency>();

            Assert.AreEqual(typeof(DependencyImpl2), service.Dependency.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldGetLifestyleFromCustomRegistration()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(IDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(DependencyImpl2));

            Assert.AreEqual(LifestyleType.Transient, handler.ComponentModel.LifestyleType);
        }
    }
}