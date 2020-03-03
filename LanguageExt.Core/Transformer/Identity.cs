﻿using System;
using LanguageExt;
using System.Linq;
using System.Collections.Generic;
using LanguageExt.TypeClasses;
using static LanguageExt.Prelude;

namespace LanguageExt
{
    public static class IdentityTExtensions
    {
        public static Identity<Arr<B>> Traverse<A, B>(this Arr<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<Arr<B>>(new Arr<B>(res));            
        }

        public static Identity<Either<L, B>> Traverse<L, A, B>(this Either<L, Identity<A>> ma, Func<A, B> f) =>
            ma.Match(
                Right: x => new Identity<Either<L, B>>(f(x.Value)),
                Left: e => new Identity<Either<L, B>>(Either<L, B>.Left(e)));

        public static Identity<EitherUnsafe<L, B>> Traverse<L, A, B>(this EitherUnsafe<L, Identity<A>> ma, Func<A, B> f) =>
            ma.MatchUnsafe(
                Right: x => new Identity<EitherUnsafe<L, B>>(f(x.Value)),
                Left: e => new Identity<EitherUnsafe<L, B>>(EitherUnsafe<L, B>.Left(e)));

        public static Identity<HashSet<B>> Traverse<L, A, B>(this HashSet<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<HashSet<B>>(new HashSet<B>(res));            
        }

        public static Identity<Identity<B>> Traverse<L, A, B>(this Identity<Identity<A>> ma, Func<A, B> f) =>
            new Identity<Identity<B>>(new Identity<B>(f(ma.Value.Value)));
        
        public static Identity<Lst<B>> Traverse<L, A, B>(this Lst<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<Lst<B>>(new Lst<B>(res));            
        }
        
        public static Identity<Option<B>> Traverse<A, B>(this Option<Identity<A>> ma, Func<A, B> f) =>
            ma.Match(
                Some: x => new Identity<Option<B>>(f(x.Value)),
                None: () => new Identity<Option<B>>(Option<B>.None));
        
        public static Identity<OptionUnsafe<B>> Traverse<A, B>(this OptionUnsafe<Identity<A>> ma, Func<A, B> f) =>
            ma.MatchUnsafe(
                Some: x => new Identity<OptionUnsafe<B>>(f(x.Value)),
                None: () => new Identity<OptionUnsafe<B>>(OptionUnsafe<B>.None));
        
        public static Identity<Que<B>> Traverse<L, A, B>(this Que<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<Que<B>>(new Que<B>(res));            
        }
        
        public static Identity<Seq<B>> Traverse<L, A, B>(this Seq<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<Seq<B>>(Seq.FromArray(res));            
        }
        
        public static Identity<IEnumerable<B>> Traverse<L, A, B>(this IEnumerable<Identity<A>> ma, Func<A, B> f)
        {
            var res = new List<B>();
            foreach (var xs in ma)
            {
                res.Add(f(xs.Value));
            }
            return new Identity<IEnumerable<B>>(Seq.FromArray(res.ToArray()));            
        }
        
        public static Identity<Set<B>> Traverse<L, A, B>(this Set<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<Set<B>>(new Set<B>(res));            
        }
        
        public static Identity<Stck<B>> Traverse<L, A, B>(this Stck<Identity<A>> ma, Func<A, B> f)
        {
            var res = new B[ma.Count];
            var ix = 0;
            foreach (var xs in ma)
            {
                res[ix] = f(xs.Value);
                ix++;
            }
            return new Identity<Stck<B>>(new Stck<B>(res));            
        }
        
        public static Identity<Try<B>> Traverse<L, A, B>(this Try<Identity<A>> ma, Func<A, B> f) =>
            ma.Match(
                Succ: x => new Identity<Try<B>>(Try(f(x.Value))),
                Fail: e => new Identity<Try<B>>(Try<B>(e)));
        
        public static Identity<TryOption<B>> Traverse<L, A, B>(this TryOption<Identity<A>> ma, Func<A, B> f) =>
            ma.Match(
                Some: x  => new Identity<TryOption<B>>(TryOption(f(x.Value))),
                None: () => new Identity<TryOption<B>>(TryOption<B>(Option<B>.None)),
                Fail: e  => new Identity<TryOption<B>>(TryOption<B>(e)));
        
        public static Identity<Validation<L, B>> Traverse<L, A, B>(this Validation<L, Identity<A>> ma, Func<A, B> f) =>
            ma.Match(
                Succ: x => new Identity<Validation<L, B>>(f(x.Value)),
                Fail: e => new Identity<Validation<L, B>>(Validation<L, B>.Fail(e)));
        
        public static Identity<Validation<MonoidL, L, B>> Traverse<MonoidL, L, A, B>(this Validation<MonoidL, L, Identity<A>> ma, Func<A, B> f) 
            where MonoidL : struct, Monoid<L>, Eq<L> =>
            ma.Match(
                Succ: x => new Identity<Validation<MonoidL, L, B>>(f(x.Value)),
                Fail: e => new Identity<Validation<MonoidL, L, B>>(Validation<MonoidL, L, B>.Fail(e)));
    }
}