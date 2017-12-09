// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;
using NUnit.Framework;
using System;

namespace ServiceToggle.Windsor.UnitTest.UsingTypes
{
    [TestFixture]
    internal class WhenCurrentImplementationIsNotImplementationOfServiceType : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel));

            Container.Register(
                Component.For<IDependency>().ImplementedBy<DependencyImpl1>(),
                Component.For<IDependency>().ImplementedBy<DependencyImpl2>(),
                Component.For<ServiceWithCollectionDependency>());
        }

        protected override void When()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Container.Register(
                    ComponentReplacement
                        .For(typeof(IDependency))
                        .CurrentlyImplementedBy(typeof(DateTime))
                        .ReplacedBy(typeof(DependencyImpl3)));
            });

            Assert.Throws<ArgumentException>(() =>
            {
                Container.Register(
                    ComponentReplacement
                        .For(typeof(IDependency), typeof(DependencyImpl1).FullName)
                        .CurrentlyImplementedBy(typeof(DateTime))
                        .ReplacedBy(typeof(DependencyImpl3)));
            });
        }

        [Then]
        public void ShouldThrowArgumentException()
        {
            // Will fail in test setup if exception is not thrown
        }
    }
}