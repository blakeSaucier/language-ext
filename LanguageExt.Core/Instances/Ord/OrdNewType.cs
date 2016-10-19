﻿using LanguageExt.TypeClasses;
using static LanguageExt.TypeClass;

namespace LanguageExt.Instances
{
    /// <summary>
    /// Compare the equality and ordering of any type in the NewType
    /// type-class
    /// </summary>
    public struct OrdNewType<NEWTYPE, ORD, A> : Ord<NewType<NEWTYPE, A>>
        where ORD     : struct, Ord<A>
        where NEWTYPE : NewType<NEWTYPE, A>
    {
        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        public bool Equals(NewType<NEWTYPE, A> x, NewType<NEWTYPE, A> y) =>
            default(EqNewType<NEWTYPE, ORD, A>).Equals(x, y);

        /// <summary>
        /// Compare two values
        /// </summary>
        /// <param name="x">Left hand side of the compare operation</param>
        /// <param name="y">Right hand side of the compare operation</param>
        /// <returns>
        /// if x greater than y : 1
        /// if x less than y    : -1
        /// if x equals y       : 0
        /// </returns>
        public int Compare(NewType<NEWTYPE, A> mx, NewType<NEWTYPE, A> my)
        {
            if (ReferenceEquals(mx, my)) return 0;
            if (ReferenceEquals(mx, null)) return -1;
            if (ReferenceEquals(my, null)) return 1;
            return default(ORD).Compare(mx.Value, my.Value);
        }
    }
}