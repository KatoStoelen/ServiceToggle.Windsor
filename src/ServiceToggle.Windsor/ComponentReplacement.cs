// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.Windsor.Service.Replacement.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castle.Windsor.Service.Replacement
{
    public static class ComponentReplacement
    {
        public static ComponentReplacementBuilder<TService> For<TService>() where TService : class
        {
            return new UnnamedComponentReplacementBuilder<TService>(new[] { typeof(TService) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2>() where TService1 : class
        {
            return new UnnamedComponentReplacementBuilder<TService1>(new[] { typeof(TService1), typeof(TService2) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2, TService3>() where TService1 : class
        {
            return new UnnamedComponentReplacementBuilder<TService1>(new[] { typeof(TService1), typeof(TService2), typeof(TService3) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2, TService3, TService4>() where TService1 : class
        {
            return new UnnamedComponentReplacementBuilder<TService1>(new[] { typeof(TService1), typeof(TService2), typeof(TService3), typeof(TService4) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2, TService3, TService4, TService5>() where TService1 : class
        {
            return new UnnamedComponentReplacementBuilder<TService1>(new[] { typeof(TService1), typeof(TService2), typeof(TService3), typeof(TService4), typeof(TService5) });
        }

        public static ComponentReplacementBuilder<TService> For<TService>(string name) where TService : class
        {
            return new NamedComponentReplacementBuilder<TService>(name, new[] { typeof(TService) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2>(string name) where TService1 : class
        {
            return new NamedComponentReplacementBuilder<TService1>(name, new[] { typeof(TService1), typeof(TService2) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2, TService3>(string name) where TService1 : class
        {
            return new NamedComponentReplacementBuilder<TService1>(name, new[] { typeof(TService1), typeof(TService2), typeof(TService3) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2, TService3, TService4>(string name) where TService1 : class
        {
            return new NamedComponentReplacementBuilder<TService1>(name, new[] { typeof(TService1), typeof(TService2), typeof(TService3), typeof(TService4) });
        }

        public static ComponentReplacementBuilder<TService1> For<TService1, TService2, TService3, TService4, TService5>(string name) where TService1 : class
        {
            return new NamedComponentReplacementBuilder<TService1>(name, new[] { typeof(TService1), typeof(TService2), typeof(TService3), typeof(TService4), typeof(TService5) });
        }

        public static ComponentReplacementBuilder<object> For(Type serviceType)
        {
            return new UnnamedComponentReplacementBuilder<object>(new[] { serviceType });
        }

        public static ComponentReplacementBuilder<object> For(params Type[] serviceTypes)
        {
            return new UnnamedComponentReplacementBuilder<object>(serviceTypes);
        }

        public static ComponentReplacementBuilder<object> For(IEnumerable<Type> serviceTypes)
        {
            return new UnnamedComponentReplacementBuilder<object>(serviceTypes.ToList());
        }

        public static ComponentReplacementBuilder<object> For(Type serviceType, string name)
        {
            return new NamedComponentReplacementBuilder<object>(name, new[] { serviceType });
        }

        public static ComponentReplacementBuilder<object> For(IEnumerable<Type> serviceTypes, string name)
        {
            return new NamedComponentReplacementBuilder<object>(name, serviceTypes.ToList());
        }
    }
}
