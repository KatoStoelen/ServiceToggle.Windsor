// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Extensions;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace Castle.Windsor.Service.Replacement.UnitTest.UsingGenerics
{
    [TestFixture]
    internal class WhenReplacingClassDependencyAlsoRegisteredAsInterface : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Register(
                Component.For<ClassDependencyWithInterfaceService>(),
                Component.For<InterfaceDependencyService>(),
                Component.For<IDependency, DependencyImpl1>().ImplementedBy<DependencyImpl1>());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement.For<DependencyImpl1>().ReplacedBy<InheritedInterfaceDependency>());
        }

        [Then]
        public void ShouldReplaceServiceWhereUsedAsClass()
        {
            var service = Container.Resolve<ClassDependencyWithInterfaceService>();

            Assert.AreEqual(typeof(InheritedInterfaceDependency), service.Dependency.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldNotReplaceServiceWhereUsedAsInterface()
        {
            var service = Container.Resolve<InterfaceDependencyService>();

            Assert.AreEqual(typeof(DependencyImpl1), service.Dependency.GetType());

            Container.Release(service);
        }
    }
}