// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor.Service.Replacement.Extensions;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace Castle.Windsor.Service.Replacement.UnitTest.UsingTypes
{
    [TestFixture]
    internal class WhenReplacingOneOfMultipleImplementationsOfService : GivenWhenThenTest
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
            Container.Register(
                ComponentReplacement
                    .For(typeof(IDependency))
                    .CurrentlyImplementedBy(typeof(DependencyImpl2))
                    .ReplacedBy(typeof(DependencyImpl3)));
        }

        [Then]
        public void NumberOfImplementationsShouldStayTheSame()
        {
            var service = Container.Resolve<ServiceWithCollectionDependency>();

            Assert.AreEqual(2, service.Dependencies.Count());

            Container.Release(service);
        }

        [Then]
        public void ShouldInjectServiceThatIsNotReplaced()
        {
            var service = Container.Resolve<ServiceWithCollectionDependency>();

            Assert.IsTrue(service.Dependencies.Any(dependency => dependency.GetType() == typeof(DependencyImpl1)));

            Container.Release(service);
        }

        [Then]
        public void ShouldInjectNewImplementationOfReplacedService()
        {
            var service = Container.Resolve<ServiceWithCollectionDependency>();

            Assert.IsTrue(service.Dependencies.Any(dependency => dependency.GetType() == typeof(DependencyImpl3)));

            Container.Release(service);
        }
    }
}