﻿// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Linq;
using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;
using ServiceToggle.Windsor.UnitTest.Helpers.Factories;
using NUnit.Framework;

namespace ServiceToggle.Windsor.UnitTest.UsingTypes
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
                    .Unless(type => type == typeof(DependencyImpl1Extension))
                    .Configure(config => config.Named(config.Implementation.Name))
                    .LifestylePerThread(),
                Component
                    .For<IDependencyFactory>()
                    .AsFactory(config => config.SelectedWith(new IDependencySelector()))
                    .LifestyleTransient(),
                Component
                    .For<ServiceWithTypedFactory>()
                    .LifestyleTransient());
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement
                    .For(typeof(DependencyImpl1), "DependencyImpl1")
                    .ReplacedBy(
                        typeof(DependencyImpl1Extension),
                        Component
                            .For(typeof(DependencyImpl1))
                            .ImplementedBy(typeof(DependencyImpl1Extension))
                            .LifestyleTransient()));
        }

        [Then]
        public void ShouldReplaceSpecifiedService()
        {
            var service = Container.Resolve<ServiceWithTypedFactory>();

            Assert.AreEqual(typeof(DependencyImpl1Extension), service.Dependency1.GetType());

            Container.Release(service);
        }

        [Then]
        public void ShouldGetLifestyleFromCustomRegistration()
        {
            var handler = Container.Kernel.GetAssignableHandlers(typeof(IDependency))
                .Single(h => h.ComponentModel.Implementation == typeof(DependencyImpl1Extension));

            Assert.AreEqual(LifestyleType.Transient, handler.ComponentModel.LifestyleType);
        }

        [Then]
        public void ShouldNotReplaceOtherImplementations()
        {
            var service = Container.Resolve<ServiceWithTypedFactory>();

            Assert.AreEqual(typeof(DependencyImpl2), service.Dependency2.GetType());
            Assert.AreEqual(typeof(DependencyImpl3), service.Dependency3.GetType());

            Container.Release(service);
        }
    }
}