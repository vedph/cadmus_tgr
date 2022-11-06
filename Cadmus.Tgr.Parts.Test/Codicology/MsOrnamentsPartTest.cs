using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Codicology
{
    public sealed class MsOrnamentsPartTest
    {
        private static MsOrnamentsPart GetPart()
        {
            MsOrnamentsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (MsOrnamentsPart)seeder.GetPart(item, null, null)!;
        }

        private static MsOrnamentsPart GetEmptyPart()
        {
            return new MsOrnamentsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsOrnamentsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsOrnamentsPart part2 =
                TestHelper.DeserializePart<MsOrnamentsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Ornaments.Count, part2.Ornaments.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsOrnamentsPart part = GetPart();
            part.Ornaments.Clear();

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
            MsOrnamentsPart part = GetEmptyPart();

            for (int n = 1; n <= 3; n++)
            {
                part.Ornaments.Add(new MsOrnament
                {
                    Type = n % 2 == 0 ? "even" : "odd"
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(3, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "type-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "type-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);
        }
    }
}
