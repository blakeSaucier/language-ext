using System;
using System.Runtime.CompilerServices;

namespace LanguageExt
{
    /// <summary>
    /// System information helper
    /// </summary>
    public static class Sys
    {
        static int processorCount;
        static int defaultAsyncSequenceConcurrency;

        /// <summary>
        /// Cached number of processors in the machine
        /// </summary>
        public static int ProcessorCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => processorCount == 0
                ? (processorCount = Environment.ProcessorCount)
                : processorCount;
        }

        /// <summary>
        /// When working with an IEnumerable of Tasks or Seq of Tasks, this setting is used as
        /// the default number of task items streamed at any one time.  This reduces pressure
        /// on the system when working with large lazy streams of tasks.
        ///
        /// Each method that uses it has an override that allows for per-usage settings.
        ///
        /// The default value is max(1, Environment.ProcessorCount / 2)
        /// </summary>
        public static int DefaultAsyncSequenceConcurrency
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => defaultAsyncSequenceConcurrency == 0
                ? (defaultAsyncSequenceConcurrency = Math.Max(1, ProcessorCount / 2))
                : defaultAsyncSequenceConcurrency;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => defaultAsyncSequenceConcurrency = Math.Max(1, value);
        }
    }
}
