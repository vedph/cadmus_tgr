using Bogus;
using Cadmus.Core;
using Cadmus.Core.Layers;
using Cadmus.Philology.Parts.Layers;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Tgr.Parts.Grammar
{
    /// <summary>
    /// Seeder for <see cref="InterpolationsLayerFragment"/>'s.
    /// Tag: <c>seed.fr.it.vedph.tgr.interpolations</c>.
    /// </summary>
    /// <seealso cref="FragmentSeederBase" />
    [Tag("seed.fr.it.vedph.tgr.interpolations")]
    public sealed class InterpolationsLayerFragmentSeeder : FragmentSeederBase
    {
        /// <summary>
        /// Gets the type of the fragment.
        /// </summary>
        /// <returns>Type.</returns>
        public override Type GetFragmentType() => typeof(InterpolationsLayerFragment);

        internal static List<ReadingSource> GetReadingSources(int count)
        {
            List<ReadingSource> sources = new List<ReadingSource>();

            for (int n = 1; n <= count; n++)
            {
                sources.Add(new Faker<ReadingSource>()
                    .RuleFor(s => s.Witness,
                        f => new string((char)('A' + f.Random.Number(0, 25)), 1))
                    .RuleFor(s => s.HandId, f => f.Lorem.Word())
                    .Generate());
            }

            return sources;
        }

        private static List<Interpolation> GetEntries(int count)
        {
            List<Interpolation> entries = new List<Interpolation>();

            for (int n = 1; n <= count; n++)
            {
                var type = (ApparatusEntryType)Randomizer.Seed.Next(0, 4);

                entries.Add(new Faker<Interpolation>()
                    .RuleFor(e => e.Type, type)
                    .RuleFor(e => e.Languages,
                        f => new string[] { f.PickRandom("lat", "grc") })
                    .RuleFor(e => e.Value, f => type == ApparatusEntryType.Note
                        ? f.Lorem.Sentence()
                        : f.Lorem.Word())
                    .RuleFor(e => e.Tag, f => f.PickRandom(null, "margin"))
                    .RuleFor(e => e.Role,
                        f => f.PickRandom("-", "paleo", "gloss", "paratext"))
                    .RuleFor(e => e.GroupId, f => f.PickRandom(null, "group"))
                    .RuleFor(e => e.Note, f => f.Random.Bool(0.25f)
                        ? f.Lorem.Sentence() : null)
                    .RuleFor(e => e.Sources, f => GetReadingSources(f.Random.Number(1, 3)))
                    .RuleFor(e => e.Quotations,
                        f => VarQuotationsLayerFragmentSeeder.GetQuotationEntries(
                            f.Random.Number(1, 3)))
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

            return new Faker<InterpolationsLayerFragment>()
                .RuleFor(fr => fr.Location, location)
                .RuleFor(fr => fr.Interpolations, f => GetEntries(f.Random.Number(1, 3)))
                .Generate();
        }
    }
}
