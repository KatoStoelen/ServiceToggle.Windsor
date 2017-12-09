// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;

namespace ServiceToggle.Windsor.Extensions
{
    internal static class HandlerExtensions
    {
        public static LifestyleType GetLifestyleType(this IHandler @this)
        {
            var lifestyleType = @this.ComponentModel.LifestyleType;

            return lifestyleType == LifestyleType.Undefined
                ? LifestyleType.Singleton
                : lifestyleType;
        }

        public static bool HasService(this IHandler @this, Type serviceType)
        {
            return @this.ComponentModel.Services.Contains(serviceType);
        }
    }
}