﻿using Bogus;
using Cadmus.Core;
using Cadmus.Tgr.Parts.Codicology;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Tgr.Parts.Codicology;

/// <summary>
/// Seeder for <see cref="MsOrnamentsPart"/>
/// Tag: <c>seed.it.vedph.tgr.ms-ornaments</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.tgr.ms-ornaments")]
public sealed class MsOrnamentsPartSeeder : PartSeederBase
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
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        MsOrnamentsPart part = new();
        SetPartMetadata(part, roleId, item);

        for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
        {
            int sn = n * 2;

            part.Ornaments.Add(new Faker<MsOrnament>()
                .RuleFor(o => o.Type, f => f.PickRandom("cycle", "figure"))
                .RuleFor(o => o.Start, f => new MsLocation
                {
                    N = sn,
                    S = sn % 2 == 0 ? "v" : "r",
                    L = f.Random.Number(1, 20)
                })
                .RuleFor(o => o.End, f => new MsLocation
                {
                    N = (sn + 1),
                    S = (sn + 1) % 2 == 0 ? "v" : "r",
                    L = f.Random.Number(1, 20)
                })
                .RuleFor(o => o.Size, SeedHelper.GetSizes(1, 1)[0])
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.Note,
                    f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                .Generate());
        }

        return part;
    }
}
