// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Extensions;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace Castle.Windsor.Service.Replacement.UnitTest.UsingGenerics
{
    [TestFixture]
    internal class WhenReplacingInterfaceDependency : GivenWhenThenTest
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
                ComponentReplacement.For<IDependency>().ReplacedBy<DependencyImpl2>());
        }

        [Then]
        public void ShouldReplaceInjectedService()
        {
            var service = Container.Resolve<ServiceWithInterfaceDependency>();

            Assert.AreEqual(typeof(DependencyImpl2), service.Dependency.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldGetLifestyleFromServiceToReplace()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(IDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(DependencyImpl2));

            Assert.AreEqual(LifestyleType.Thread, handler.ComponentModel.LifestyleType);
        }
    }
}