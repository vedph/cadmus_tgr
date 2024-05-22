using Bogus;
using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Seed.Tgr.Parts;

internal static class SeedHelper
{
    /// <summary>
    /// Gets a random number of document references.
    /// </summary>
    /// <param name="min">The min number of references to get.</param>
    /// <param name="max">The max number of references to get.</param>
    /// <returns>References.</returns>
    public static List<DocReference> GetDocReferences(int min, int max)
    {
        List<DocReference> refs = new();

        for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
        {
            refs.Add(new Faker<DocReference>()
                .RuleFor(r => r.Tag, f => f.PickRandom(null, "book"))
                .RuleFor(r => r.Citation,
                    f => f.Name.LastName() + " " + f.Date.Past(10))
                .RuleFor(r => r.Note, f => f.Lorem.Sentence())
                .Generate());
        }

        return refs;
    }

    public static List<PhysicalSize> GetSizes(int min, int max)
    {
        List<PhysicalSize> sizes = [];

        for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
        {
            sizes.Add(new Faker<PhysicalSize>()
                .RuleFor(s => s.W, f => new PhysicalDimension
                {
                    Unit = "cm",
                    Value = f.Random.Number(10, 20)
                })
                .RuleFor(s => s.H, f => new PhysicalDimension
                {
                    Unit = "cm",
                    Value = f.Random.Number(10, 20)
                })
                .Generate());
        }

        return sizes;
    }
}
