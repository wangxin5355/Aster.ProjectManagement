﻿using System;

namespace Aster.Framework.Common.Data.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MapToAttribute : Attribute
    {
        public string DatabaseColumn { get; private set; }
        public MapToAttribute(string databaseColumn) { DatabaseColumn = databaseColumn; }
    }
}
