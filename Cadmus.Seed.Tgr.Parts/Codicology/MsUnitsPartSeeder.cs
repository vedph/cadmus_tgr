using Bogus;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using Cadmus.Tgr.Parts.Codicology;
using System.Collections.Generic;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Seed.Tgr.Parts.Codicology
{
    /// <summary>
    /// Seeder for <see cref="MsUnitsPart"/>.
    /// Tag: <c>seed.it.vedph.tgr.ms-units</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tgr.ms-units")]
    public sealed class MsUnitsPartSeeder : PartSeederBase
    {
        private static List<MsGuardSheet> GetGuardSheets(int count)
        {
            List<MsGuardSheet> sheets = new List<MsGuardSheet>();

            for (int n = 1; n <= count; n++)
            {
                sheets.Add(new Faker<MsGuardSheet>()
                    .RuleFor(s => s.IsBack, f => f.Random.Bool(0.5f))
                    .RuleFor(s => s.Material,
                        f => f.PickRandom("parchment", "paper"))
                    .RuleFor(s => s.Watermarks,
                        f => GetWatermarks(f.Random.Number(0, 3)))
                    .RuleFor(s => s.Note,
                        f => f.Random.Bool(0.2f) ? f.Lorem.Sentence() : null)
                    .Generate());
            }

            return sheets;
        }

        private static List<MsWatermark> GetWatermarks(int count)
        {
            List<MsWatermark> watermarks = new List<MsWatermark>();

            for (int n = 1; n <= count; n++)
            {
                watermarks.Add(new MsWatermark
                {
                    Value = $"watermark {n}",
                    Description = $"Description of watermark {n}"
                });
            }

            return watermarks;
        }

        private static List<MsPalimpsest> GetPalimpsests(int count)
        {
            List<MsPalimpsest> palimpsests = new List<MsPalimpsest>();

            for (int n = 1; n <= count; n++)
            {
                bool even = n % 2 == 0;
                palimpsests.Add(new Faker<MsPalimpsest>()
                    .RuleFor(p => p.Date, HistoricalDate.Parse($"{1300 + n} AD"))
                    .RuleFor(p => p.Locations,
                        new List<MsLocation>(new MsLocation[]
                        {
                            new MsLocation
                            {
                                N = n, S = even? "v" : "r", L = n + 10
                            }
                        }))
                    .RuleFor(p => p.Note,
                        f => f.Random.Bool(0.2f) ? f.Lorem.Sentence() : null)
                    .Generate());
            }

            return palimpsests;
        }

        private static List<MsRuling> GetRulings(int count)
        {
            List<MsRuling> rulings = new List<MsRuling>();

            for (int n = 1; n <= count; n++)
            {
                bool even = n % 2 == 0;
                rulings.Add(new Faker<MsRuling>()
                    .RuleFor(r => r.Manner, f => f.PickRandom("ink", "lead-pt"))
                    .RuleFor(r => r.System, f => f.PickRandom("direct", "transmitted"))
                    .RuleFor(r => r.Type, f => f.Lorem.Word())
                    .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                    .Generate());
            }

            return rulings;
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            MsUnitsPart part = new MsUnitsPart();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 5 + 1);
            for (int n = 1; n <= count; n++)
            {
                bool even = n % 2 == 0;
                int guardCount = Randomizer.Seed.Next(0, 3);

                part.Units.Add(new Faker<MsUnit>()
                    .RuleFor(u => u.Start, f => new MsLocation
                        {
                            N = n, S = even ? "v" : "r", L = f.Random.Number(1, 40)
                        })
                    .RuleFor(u => u.End, f => new MsLocation
                        {
                            N = n + 3,
                            S = even ? "v" : "r",
                            L = f.Random.Number(1, 40)
                        })
                    .RuleFor(u => u.Palimpsests, f => GetPalimpsests(f.Random.Number(0, 2)))
                    .RuleFor(u => u.Material, f => f.PickRandom("paper", "parchment"))
                    .RuleFor(u => u.GuardSheetMaterial,
                        f => f.PickRandom("paper", "parchment"))
                    .RuleFor(u => u.SheetCount, f => f.Random.Number(1, 3))
                    .RuleFor(u => u.GuardSheetCount, guardCount)
                    .RuleFor(u => u.BackGuardSheetCount, guardCount)
                    .RuleFor(u => u.GuardSheets, GetGuardSheets(guardCount))
                    .RuleFor(u => u.Quires, f => f.Lorem.Sentence())
                    .RuleFor(u => u.SheetNumbering, f => f.Lorem.Sentence())
                    .RuleFor(u => u.QuireNumbering, f => f.Lorem.Sentence())
                    .RuleFor(u => u.LeafSizes, SeedHelper.GetSizes(1, 2))
                    .RuleFor(u => u.LeafSizeSamples,
                        new List<MsLocation>(new[]
                        {
                            new MsLocation
                            {
                                N = n,
                                S = even? "v" : "r"
                            }
                        }))
                    .RuleFor(u => u.WrittenAreaSizes, SeedHelper.GetSizes(1, 2))
                    .RuleFor(u => u.WrittenAreaSizeSamples,
                        new List<MsLocation>(new[]
                        {
                            new MsLocation
                            {
                                N = n,
                                S = even? "v" : "r"
                            }
                        }))
                    .RuleFor(u => u.Rulings, f => GetRulings(f.Random.Number(1, 2)))
                    .RuleFor(u => u.Watermarks, f => GetWatermarks(f.Random.Number(0, 3)))
                    .RuleFor(u => u.State, f => f.Lorem.Sentence())
                    .RuleFor(u => u.Binding, f => f.Lorem.Sentence())
                    .Generate());
            }

            return part;
        }
    }
}
