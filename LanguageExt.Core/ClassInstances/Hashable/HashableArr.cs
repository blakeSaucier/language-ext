﻿using LanguageExt.TypeClasses;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using static LanguageExt.TypeClass;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Array hash
    /// </summary>
    public struct HashableArr<HashA, A> : Hashable<Arr<A>> where HashA : struct, Hashable<A>
    {
        /// <summary>
        /// Get hash code of the value
        /// </summary>
        /// <param name="x">Value to get the hash code of</param>
        /// <returns>The hash code of x</returns>
        [Pure]
        public int GetHashCode(Arr<A> x) =>
            hash<HashA, A>(x);

        [Pure]
        public Task<int> GetHashCodeAsync(Arr<A> x) =>
            GetHashCode(x).AsTask();
    }

    /// <summary>
    /// Array hash
    /// </summary>
    public struct HashableArr<A> : Hashable<Arr<A>>
    {
        /// <summary>
        /// Get hash code of the value
        /// </summary>
        /// <param name="x">Value to get the hash code of</param>
        /// <returns>The hash code of x</returns>
        [Pure]
        public int GetHashCode(Arr<A> x) =>
            default(HashableArr<HashableDefault<A>, A>).GetHashCode(x);

        [Pure]
        public Task<int> GetHashCodeAsync(Arr<A> x) =>
            GetHashCode(x).AsTask();
    }
}
