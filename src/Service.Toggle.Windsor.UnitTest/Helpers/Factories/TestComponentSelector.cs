// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace Castle.Windsor.Service.Replacement.UnitTest.Helpers.Factories
{
    internal class TestComponentSelector : DefaultTypedFactoryComponentSelector
    {
        private const string FactoryMethodName = "Create";

        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == FactoryMethodName && arguments.Length == 1 && arguments[0] is string)
            {
                return (string) arguments[0];
            }

            return base.GetComponentName(method, arguments);
        }
    }
}