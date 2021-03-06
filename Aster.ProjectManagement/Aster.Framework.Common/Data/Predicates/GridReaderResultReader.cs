﻿using System.Collections.Generic;
using Dapper;
using Aster.Framework.Common.Data.Core.Predicates;

namespace Aster.Framework.Common.Data.Predicates
{
    public class GridReaderResultReader : IMultipleResultReader
    {
        private readonly SqlMapper.GridReader _reader;

        public GridReaderResultReader(SqlMapper.GridReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<T> Read<T>()
        {
            return _reader.Read<T>();
        }
    }
}