using Bogus;
using Cadmus.Core;
using Cadmus.Tgr.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Tgr.Parts.Codicology
{
    /// <summary>
    /// Part seeder for <see cref="MsScriptsPart"/>.
    /// Tag: <c>seed.it.vedph.tgr.ms-scripts</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tgr.ms-scripts")]
    public sealed class MsScriptsPartSeeder : PartSeederBase
    {
        private List<MsHandLetter> GetLetters(int count)
        {
            List<MsHandLetter> letters = new List<MsHandLetter>();
            for (int n = 1; n <= count; n++)
            {
                char c = (char)('A' + Randomizer.Seed.Next(0, 26));

                letters.Add(new Faker<MsHandLetter>()
                    .RuleFor(l => l.Letter, new string(c, 1))
                    .RuleFor(l => l.Description, f => f.Lorem.Sentence())
                    .RuleFor(l => l.ImageId, $"img-{c}-")
                    .Generate());
            }
            return letters;
        }

        private List<MsHand> GetHands(int count)
        {
            List<MsHand> hands = new List<MsHand>();
            for (int n = 1; n <= count; n++)
            {
                bool even = n % 2 == 0;

                hands.Add(new Faker<MsHand>()
                    .RuleFor(h => h.Id, f => f.Lorem.Word())
                    .RuleFor(h => h.Date, HistoricalDate.Parse($"{1300 + n}"))
                    .RuleFor(h => h.Start, f => new MsLocation
                    {
                        N = n,
                        V = even,
                        L = f.Random.Number(1, 40)
                    })
                    .RuleFor(h => h.End, f => new MsLocation
                    {
                        N = n + 3,
                        V = even,
                        L = f.Random.Number(1, 40)
                    })
                    .RuleFor(h => h.Description, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Abbreviations, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Letters, f => GetLetters(f.Random.Number(0, 3)))
                    .Generate());
            }
            return hands;
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
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            MsScriptsPart part = new MsScriptsPart();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 5 + 1);
            for (int n = 1; n <= count; n++)
            {
                bool even = n % 2 == 0;
                part.Scripts.Add(new Faker<MsScript>()
                    .RuleFor(s => s.Role, f => f.PickRandom("superior", "inferior"))
                    .RuleFor(s => s.Language, f => f.PickRandom("lat", "grc"))
                    .RuleFor(s => s.Type, f => f.PickRandom("cap", "unc"))
                    .RuleFor(s => s.Hands, f => GetHands(f.Random.Number(0, 2)))
                    .Generate());
            }

            return part;
        }
    }
}
