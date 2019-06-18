﻿using System;

namespace Aster.Framework.Common.Data.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SchemaAttribute : Attribute
    {
        public string Name { get; private set; }
        public SchemaAttribute(string name)
        {
            Name = name;
        }
    }
}