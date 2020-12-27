using Bogus;
using Cadmus.Core;
using Cadmus.Core.Layers;
using Cadmus.Philology.Parts;
using Cadmus.Philology.Parts.Layers;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Tgr.Parts.Grammar
{
    /// <summary>
    /// Seeder for <see cref="VarQuotationsLayerFragment"/>'s.
    /// Tag: <c>seed.fr.it.vedph.tgr.var-quotations</c>.
    /// </summary>
    /// <seealso cref="FragmentSeederBase" />
    [Tag("seed.fr.it.vedph.tgr.var-quotations")]
    public sealed class VarQuotationsLayerFragmentSeeder : FragmentSeederBase
    {
        /// <summary>
        /// Gets the type of the fragment.
        /// </summary>
        /// <returns>Type.</returns>
        public override Type GetFragmentType() => typeof(VarQuotationsLayerFragment);

        private static List<AnnotatedValue> GetAnnotatedValues(int count)
        {
            List<AnnotatedValue> values = new List<AnnotatedValue>();

            for (int n = 1; n <= count; n++)
            {
                values.Add(new Faker<AnnotatedValue>()
                    .RuleFor(v => v.Value, f => f.Lorem.Word())
                    .RuleFor(v => v.Note, f => f.Random.Bool(0.25f) ?
                        f.Lorem.Sentence() : null)
                    .Generate());
            }

            return values;
        }

        private static List<LocAnnotatedValue> GetLocAnnotatedValues(int count)
        {
            List<LocAnnotatedValue> values = new List<LocAnnotatedValue>();

            for (int n = 1; n <= count; n++)
            {
                values.Add(new Faker<LocAnnotatedValue>()
                    .RuleFor(v => v.Tag, f => f.PickRandom("ancient", "modern"))
                    .RuleFor(v => v.Value, f => f.Lorem.Word())
                    .RuleFor(v => v.Location,
                        f => $"{f.Random.Number(1,24)}.{f.Random.Number(1, 100)}")
                    .RuleFor(v => v.Note, f => f.Random.Bool(0.25f)?
                        f.Lorem.Sentence() : null)
                    .Generate());
            }

            return values;
        }

        private static List<QuotationVariant> GetQuotationVariants(int count)
        {
            List<QuotationVariant> variants = new List<QuotationVariant>();

            for (int n = 1; n <= count; n++)
            {
                var type = (ApparatusEntryType)Randomizer.Seed.Next(0, 4);

                variants.Add(new Faker<QuotationVariant>()
                    .RuleFor(v => v.Lemma, f => f.Lorem.Word())
                    .RuleFor(v => v.Type, type)
                    .RuleFor(v => v.Value, f =>  type == ApparatusEntryType.Note
                        ? f.Lorem.Sentence()
                        : f.Lorem.Word())
                    .RuleFor(v => v.Witnesses,
                        f => GetAnnotatedValues(f.Random.Number(1, 3)))
                    .RuleFor(v => v.Authors,
                        f => GetLocAnnotatedValues(f.Random.Number(1, 3)))
                    .Generate());
            }

            return variants;
        }

        private static List<QuotationParallel> GetQuotationParallels(int count)
        {
            List<QuotationParallel> parallels = new List<QuotationParallel>();

            for (int n = 1; n <= count; n++)
            {
                parallels.Add(new Faker<QuotationParallel>()
                    .RuleFor(p => p.Tag, f => f.Lorem.Word())
                    .RuleFor(p => p.Work, f => f.Lorem.Word())
                    .RuleFor(p => p.Location,
                        f => $"{f.Random.Number(1, 24)}.{f.Random.Number(1, 100)}")
                    .Generate());
            }

            return parallels;
        }

        internal static List<VarQuotationEntry> GetQuotationEntries(int count)
        {
            List<VarQuotationEntry> entries = new List<VarQuotationEntry>();

            for (int n = 1; n <= count; n++)
            {
                entries.Add(new Faker<VarQuotationEntry>()
                    .RuleFor(e => e.Tag, f => f.Lorem.Word())
                    .RuleFor(e => e.Authority, f => f.PickRandom("gram", "ling"))
                    .RuleFor(e => e.Work, f => f.Lorem.Word())
                    .RuleFor(e => e.Location,
                        f => $"{f.Random.Number(1, 24)}.{f.Random.Number(1, 100)}")
                    .RuleFor(e => e.Parallels,
                        f => GetQuotationParallels(f.Random.Number(1, 3)))
                    .RuleFor(e => e.Variants,
                        f => GetQuotationVariants(f.Random.Number(1, 3)))
                    .Generate());
            }

            return entries;
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="location">The location.</param>
        /// <param name="baseText">The base text.</param>
        /// <returns>A new fragment.</returns>
        /// <exception cref="ArgumentNullException">location or
        /// baseText</exception>
        public override ITextLayerFragment GetFragment(
            IItem item, string location, string baseText)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));
            if (baseText == null)
                throw new ArgumentNullException(nameof(baseText));

            return new Faker<VarQuotationsLayerFragment>()
                .RuleFor(fr => fr.Location, location)
                .RuleFor(fr => fr.Entries,
                    f => GetQuotationEntries(f.Random.Number(1, 3)))
                .Generate();
        }
    }
}
