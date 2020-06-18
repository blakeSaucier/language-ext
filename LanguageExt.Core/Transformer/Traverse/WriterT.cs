using LanguageExt.TypeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using static LanguageExt.Prelude;

namespace LanguageExt
{
    public static partial class WriterT
    {
        //
        // Collections
        //
        
        public static Writer<MonoidW, W, Seq<A>> Sequence<MonoidW, W, A>(this Seq<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(Prelude.Seq);

        public static Writer<MonoidW, W, Lst<A>> Sequence<MonoidW, W, A>(this Lst<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(toList);

        public static Writer<MonoidW, W, Arr<A>> Sequence<MonoidW, W, A>(this Arr<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(toArray);

        public static Writer<MonoidW, W, A[]> Sequence<MonoidW, W, A>(this Writer<MonoidW, W, A>[] ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(x => x.ToArray());

        public static Writer<MonoidW, W, Set<A>> Sequence<MonoidW, W, A>(this Set<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(toSet);

        public static Writer<MonoidW, W, HashSet<A>> Sequence<MonoidW, W, A>(this HashSet<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(toHashSet);

        public static Writer<MonoidW, W, Stck<A>> Sequence<MonoidW, W, A>(this Stck<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma.Reverse()).Map(toStack);

        public static Writer<MonoidW, W, IEnumerable<A>> Sequence<MonoidW, W, A>(this IEnumerable<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> =>
            SequenceFast(ma).Map(Enumerable.AsEnumerable);


        public static Writer<MonoidW, W, Seq<B>> Traverse<MonoidW, W, A, B>(this Seq<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(Prelude.Seq);

        public static Writer<MonoidW, W, Lst<B>> Traverse<MonoidW, W, A, B>(this Lst<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(toList);

        public static Writer<MonoidW, W, Arr<B>> Traverse<MonoidW, W, A, B>(this Arr<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(toArray);

        public static Writer<MonoidW, W, B[]> Traverse<MonoidW, W, A, B>(this Writer<MonoidW, W, A>[] ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(x => x.ToArray());

        public static Writer<MonoidW, W, Set<B>> Traverse<MonoidW, W, A, B>(this Set<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(toSet);

        public static Writer<MonoidW, W, HashSet<B>> Traverse<MonoidW, W, A, B>(this HashSet<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(toHashSet);

        public static Writer<MonoidW, W, Stck<B>> Traverse<MonoidW, W, A, B>(this Stck<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma.Reverse(), f).Map(toStack);

        public static Writer<MonoidW, W, IEnumerable<B>> Traverse<MonoidW, W, A, B>(this IEnumerable<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> =>
            TraverseFast(ma, f).Map(Enumerable.AsEnumerable);
        
        internal static Writer<MonoidW, W, List<A>> SequenceFast<MonoidW, W, A>(this IEnumerable<Writer<MonoidW, W, A>> ma) where MonoidW : struct, Monoid<W> => () =>
        {
            var values = new List<A>();
            var output = default(MonoidW).Empty();
            foreach (var item in ma)
            {
                var (a, o, bottom) = item();
                if (bottom) return (Value: new List<A>(), Output: default(MonoidW).Empty(), IsBottom: true);
                values.Add(a);
                output = default(MonoidW).Append(output, o);
            }
            return (Value: values, Output: output, IsBottom: false);
        };

        internal static Writer<MonoidW, W, List<B>> TraverseFast<MonoidW, W, A, B>(this IEnumerable<Writer<MonoidW, W, A>> ma, Func<A, B> f) where MonoidW : struct, Monoid<W> => () =>
        {
            var values = new List<B>();
            var output = default(MonoidW).Empty();
            foreach (var item in ma)
            {
                var (a, o, bottom) = item();
                if (bottom) return (Value: new List<B>(), Output: default(MonoidW).Empty(), IsBottom: true);
                values.Add(f(a));
                output = default(MonoidW).Append(output, o);
            }
            return (Value: values, Output: output, IsBottom: false);
        };
    }
}
