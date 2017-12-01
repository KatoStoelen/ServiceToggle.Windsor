// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Extensions;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace Castle.Windsor.Service.Replacement.UnitTest.UsingTypes
{
    [TestFixture]
    internal class WhenReplacingClassDependency : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Register(
                Component.For<ClassDependencyWithoutInterfaceService>(),
                Component.For<OtherDependency>().LifestyleTransient());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement.For(typeof(OtherDependency)).ReplacedBy(typeof(InheritedDependency)));
        }

        [Then]
        public void ShouldReplaceInjectedService()
        {
            var service = Container.Resolve<ClassDependencyWithoutInterfaceService>();

            Assert.AreEqual(typeof(InheritedDependency), service.Dependency.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldGetLifestyleFromServiceToReplace()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(OtherDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(InheritedDependency));

            Assert.AreEqual(LifestyleType.Transient, handler.ComponentModel.LifestyleType);
        }
    }
}