using System;
using System.Collections.Generic;
using LanguageExt.TypeClasses;

namespace LanguageExt
{
    public partial class SeqT
    {
        public static Seq<Arr<B>> Sequence<A, B>(this Arr<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Either<L, B>> Sequence<L, A, B>(this Either<L, A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<EitherUnsafe<L, B>> Sequence<L, A, B>(this EitherUnsafe<L, A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Identity<B>> Sequence<A, B>(this Identity<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Traverse(Prelude.identity);

        public static Seq<IEnumerable<B>> Sequence<A, B>(this IEnumerable<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Option<B>> Sequence<A, B>(this Option<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<OptionUnsafe<B>> Sequence<A, B>(this OptionUnsafe<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Seq<B>> Sequence<A, B>(this Seq<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Set<B>> Sequence<A, B>(this Set<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Try<B>> Sequence<A, B>(this Try<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<TryOption<B>> Sequence<A, B>(this TryOption<A> ta, Func<A, Seq<B>> f) =>
            ta.Map(f).Sequence();
        
        public static Seq<Validation<FAIL, B>> Sequence<FAIL, A, B>(this Validation<FAIL, A> ta, Func<A, Seq<B>> f) => 
            ta.Map(f).Sequence();
        
        public static Seq<Validation<MonoidFail, FAIL, B>> Sequence<MonoidFail, FAIL, A, B>(this Validation<MonoidFail, FAIL, A> ta, Func<A, Seq<B>> f)
            where MonoidFail : struct, Monoid<FAIL>, Eq<FAIL> =>
            ta.Map(f).Traverse(Prelude.identity);
    }
}
