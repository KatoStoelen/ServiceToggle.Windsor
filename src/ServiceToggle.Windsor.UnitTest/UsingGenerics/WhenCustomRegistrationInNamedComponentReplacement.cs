// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Linq;
using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Extensions;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Factories;
using NUnit.Framework;

namespace Castle.Windsor.Service.Replacement.UnitTest.UsingGenerics
{
    [TestFixture]
    internal class WhenCustomRegistrationInNamedComponentReplacement : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.AddFacility<TypedFactoryFacility>();

            Container.Register(
                Classes
                    .FromThisAssembly()
                    .IncludeNonPublicTypes()
                    .BasedOn<IDependency>()
                    .Unless(type => type == typeof(InheritedInterfaceDependency))
                    .Configure(config => config.Named(config.Implementation.Name))
                    .LifestylePerThread(),
                Component
                    .For<IDependencyFactory>()
                    .AsFactory(config => config.SelectedWith(new TestComponentSelector()))
                    .LifestyleTransient(),
                Component
                    .For<TypedFactoryService>()
                    .LifestyleTransient());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement
                    .For<DependencyImpl1>("DependencyImpl1")
                    .ReplacedBy<InheritedInterfaceDependency>(
                        Component
                            .For<DependencyImpl1>()
                            .ImplementedBy<InheritedInterfaceDependency>()
                            .LifestyleTransient()));
        }

        [Then]
        public void ShouldReplaceSpecifiedService()
        {
            var service = Container.Resolve<TypedFactoryService>();

            Assert.AreEqual(typeof(InheritedInterfaceDependency), service.Dependency1.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldGetLifestyleFromCustomRegistration()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(IDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(InheritedInterfaceDependency));

            Assert.AreEqual(LifestyleType.Transient, handler.ComponentModel.LifestyleType);
        }

        [Then]
        public void ShouldNotReplaceOtherImplementations()
        {
            var service = Container.Resolve<TypedFactoryService>();

            Assert.AreEqual(typeof(DependencyImpl2), service.Dependency2.GetType());
            Assert.AreEqual(typeof(DependencyImpl3), service.Dependency3.GetType());

            Container.Release(service);
        }
    }
}