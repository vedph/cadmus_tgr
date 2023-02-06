using Cadmus.Core;
using Cadmus.Tgr.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Codicology;

public sealed class MsContentsPartTest
{
    private static MsContentsPart GetPart(int count)
    {
        MsContentsPart part = new()
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };

        for (int n = 1; n <= count; n++)
        {
            part.Contents.Add(new MsContent
            {
                Start = new MsLocation
                {
                    N = n, S = n % 2 == 0 ? "v" : "r", L = n + 10
                },
                End = new MsLocation
                {
                    N = n + 3, S = n % 2 == 0 ? "v" : "r", L = n + 5
                },
                Work = $"work.{n}",
                Location = "12.34",
                Title = $"Title {n}",
                Incipit = "incipit",
                Explicit = "explicit",
                Note = "note",
                Editions = TestHelper.GetDocReferences(2)
            });
        }

        return part;
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        MsContentsPart part = GetPart(2);

        string json = TestHelper.SerializePart(part);
        MsContentsPart part2 =
            TestHelper.DeserializePart<MsContentsPart>(json);

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(2, part.Contents.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        MsContentsPart part = GetPart(0);

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Entries_Ok()
    {
        MsContentsPart part = GetPart(3);

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Equal(7, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("3", pin.Value);

        for (int n = 1; n <= 3; n++)
        {
            // work
            pin = pins.Find(p => p.Name == "work" && p.Value == $"work.{n}");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // title
            pin = pins.Find(p => p.Name == "title" && p.Value == $"title {n}");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
