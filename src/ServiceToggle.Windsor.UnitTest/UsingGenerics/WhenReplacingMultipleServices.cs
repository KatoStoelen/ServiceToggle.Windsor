// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace ServiceToggle.Windsor.UnitTest.UsingGenerics
{
    [TestFixture]
    internal class WhenReplacingMultipleServices : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Register(
                Component.For<ServiceWithClassDependency>(),
                Component.For<ServiceWithInterfaceDependency>(),
                Component.For<IDependency, DependencyImpl1>().ImplementedBy<DependencyImpl1>());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement.For<IDependency, DependencyImpl1>().ReplacedBy<DependencyImpl1Extension>());
        }

        [Then]
        public void ShouldReplaceServiceWhereUsedAsClass()
        {
            var service = Container.Resolve<ServiceWithClassDependency>();

            Assert.AreEqual(typeof(DependencyImpl1Extension), service.Dependency.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldReplaceServiceWhereUsedAsInterface()
        {
            var service = Container.Resolve<ServiceWithInterfaceDependency>();

            Assert.AreEqual(typeof(DependencyImpl1Extension), service.Dependency.GetType());

            Container.Release(service);
        }
    }
}