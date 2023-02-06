using Bogus;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using Cadmus.Tgr.Parts.Codicology;
using System;

namespace Cadmus.Seed.Tgr.Parts.Codicology;

/// <summary>
/// Part seeder for <see cref="MsContentsPart"/>.
/// Tag: <c>seed.it.vedph.tgr.ms-contents</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.tgr.ms-contents")]
public sealed class MsContentsPartSeeder : PartSeederBase
{
    /// <summary>
    /// Creates and seeds a new part.
    /// </summary>
    /// <param name="item">The item this part should belong to.</param>
    /// <param name="roleId">The optional part role ID.</param>
    /// <param name="factory">The part seeder factory. This is used
    /// for layer parts, which need to seed a set of fragments.</param>
    /// <returns>A new part.</returns>
    /// <exception cref="ArgumentNullException">item</exception>
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        MsContentsPart part = new();
        SetPartMetadata(part, roleId, item);

        int count = Randomizer.Seed.Next(1, 3 + 1);
        for (int n = 1; n <= count; n++)
        {
            int sn = n * 2;

            part.Contents.Add(new Faker<MsContent>()
                .RuleFor(c => c.Start, f => new MsLocation
                {
                    N = sn,
                    S = sn % 2 == 0? "v" : "r",
                    L = f.Random.Number(1, 20)
                })
                .RuleFor(c => c.End, f => new MsLocation
                {
                    N = (sn + 1),
                    S = (sn + 1) % 2 == 0 ? "v" : "r",
                    L = f.Random.Number(1, 20)
                })
                .RuleFor(c => c.Work, f => $"{f.Lorem.Word()}.{f.Lorem.Word()}")
                .RuleFor(c => c.Location,
                    f => f.Lorem.Random.Number(1, 24) + "." +
                    f.Lorem.Random.Number(1, 100))
                .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
                .RuleFor(c => c.Incipit, f => f.Lorem.Sentence(5, 3))
                .RuleFor(c => c.Explicit, f => f.Lorem.Sentence(5, 3))
                .RuleFor(c => c.Note,
                    f => f.Random.Bool(0.2f)? f.Lorem.Sentence() : null)
                .RuleFor(c => c.Editions, SeedHelper.GetDocReferences(1, 3))
                .Generate());
        }

        return part;
    }
}
