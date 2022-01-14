//
// GenericEventArgs.cs
//
// Trevi Awater
// 13-01-2022
//
// © Xamarin.Neo4j
//

using System;

namespace Xamarin.Neo4j.Utilities
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Data { get; }

        public GenericEventArgs(T data)
        {
            Data = data;
        }
    }
}
