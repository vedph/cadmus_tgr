using Bogus;
using Cadmus.Core;
using Cadmus.Parts.General;
using Cadmus.Tgr.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Tgr.Parts.Codicology
{
    /// <summary>
    /// Part seeder for <see cref="MsPlacesPart"/>.
    /// Tag: <c>seed.it.vedph.tgr.ms-places</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tgr.ms-places")]
    public sealed class MsPlacesPartSeeder : PartSeederBase
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
            if (item == null) throw new ArgumentNullException(nameof(item));

            MsPlacesPart part = new MsPlacesPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Places.Add(new Faker<MsPlace>()
                    .RuleFor(p => p.Area,
                        f => f.PickRandom("France", "Germany", "Italy"))
                    .RuleFor(p => p.Address, f => $"{f.Lorem.Word()}, {f.Lorem.Word()}")
                    .RuleFor(p => p.City, f => f.Address.City())
                    .RuleFor(p => p.Site, f => f.PickRandom("A library", "A monastery"))
                    .RuleFor(p => p.Rank, (short)n)
                    .RuleFor(p => p.Sources, SeedHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            return part;
        }
    }
}
