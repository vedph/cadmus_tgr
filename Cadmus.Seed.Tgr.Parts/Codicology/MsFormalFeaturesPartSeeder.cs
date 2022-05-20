using Bogus;
using Cadmus.Core;
using Cadmus.Tgr.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Tgr.Parts.Codicology
{
    /// <summary>
    /// Seeder for <see cref="MsFormalFeaturesPart"/>.
    /// Tag: <c>seed.it.vedph.tgr.ms-formal-features</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tgr.ms-formal-features")]
    public sealed class MsFormalFeaturesPartSeeder : PartSeederBase
    {
        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            MsFormalFeaturesPart part = new();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Features.Add(new Faker<MsFormalFeature>()
                    .RuleFor(f => f.Description, f => f.Lorem.Sentence())
                    .RuleFor(f => f.HandId, f => f.Lorem.Word())
                    .Generate());
            }

            return part;
        }
    }
}
