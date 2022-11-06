using Bogus;
using Cadmus.Core;
using Cadmus.Tgr.Parts;
using Cadmus.Tgr.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Tgr.Parts.Codicology
{
    /// <summary>
    /// Seeder for <see cref="MsHistoryPart"/>.
    /// Tag: <c>seed.it.vedph.tgr.ms-history</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tgr.ms-history")]
    public sealed class MsHistoryPartSeeder : PartSeederBase
    {
        private static List<GeoAddress> GetProvenances(int count)
        {
            List<GeoAddress> provenances = new();

            for (int n = 1; n <= count; n++)
            {
                provenances.Add(new Faker<GeoAddress>()
                    .RuleFor(a => a.Area, f => f.Address.Country())
                    .RuleFor(a => a.Address, f => f.Address.City())
                    .Generate());
            }

            return provenances;
        }

        private static MsSubscription GetSubscription()
        {
            return new Faker<MsSubscription>()
                .RuleFor(s => s.Locations, f => new List<MsLocation>(
                    new[]
                    {
                        new MsLocation
                        {
                            N = f.Random.Number(1, 30),
                            S = f.Random.Bool()? "v" : "r",
                            L = f.Random.Number(1, 40)
                        }
                    }))
                .RuleFor(s => s.Language, f => f.PickRandom("lat", "grc"))
                .RuleFor(s => s.Text, f => f.Lorem.Sentence())
                .RuleFor(s => s.Note,
                    f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                .RuleFor(s => s.HandId, f => f.Lorem.Word())
                .Generate();
        }

        private static List<MsAnnotation> GetAnnotations(int count)
        {
            List<MsAnnotation> annotations = new();

            for (int n = 1; n <= count; n++)
            {
                annotations.Add(new Faker<MsAnnotation>()
                    .RuleFor(s => s.HandId, f => f.Lorem.Word())
                    .RuleFor(s => s.Language, f => f.PickRandom("lat", "grc"))
                    .RuleFor(s => s.Note,
                        f => f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null)
                    .Generate());
            }

            return annotations;
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart? GetPart(IItem item, string? roleId,
            PartSeederFactory? factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            MsHistoryPart part = new Faker<MsHistoryPart>()
                .RuleFor(p => p.Provenances, f => GetProvenances(f.Random.Number(1, 3)))
                .RuleFor(p => p.History, f => f.Lorem.Sentence())
                .RuleFor(p => p.Owners, f => new List<string>(new[] { f.Name.FirstName() }))
                .RuleFor(p => p.Subscription, GetSubscription())
                .RuleFor(p => p.Annotations, f => GetAnnotations(f.Random.Number(1, 3)))
                .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
