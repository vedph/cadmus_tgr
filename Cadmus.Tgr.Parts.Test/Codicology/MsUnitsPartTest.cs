using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Codicology
{
    public sealed class MsUnitsPartTest
    {
        private static MsUnitsPart GetPart()
        {
            MsUnitsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (MsUnitsPart)seeder.GetPart(item, null, null);
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsUnitsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsUnitsPart part2 =
                TestHelper.DeserializePart<MsUnitsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Units.Count, part2.Units.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsUnitsPart part = GetPart();
            part.Units.Clear();

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
            MsUnitsPart part = new()
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };
            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Units.Add(new MsUnit
                {
                    Material = even ? "paper" : "parchment",
                    Palimpsests = even
                        ? new List<MsPalimpsest>(new[] { new MsPalimpsest() })
                        : null,
                    SheetCount = n * 2,
                    GuardSheetCount = n
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(10, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            // material
            pin = pins.Find(p => p.Name == "material" && p.Value == "parchment");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "material" && p.Value == "paper");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // palimpsest
            pin = pins.Find(p => p.Name == "palimpsest" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // sheets
            for (int n = 1; n <= 3; n++)
            {
                // sheet-count
                pin = pins.Find(p => p.Name == "sheet-count"
                    && p.Value == $"{n * 2}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
                // guard-sheet-count
                pin = pins.Find(p => p.Name == "guard-sheet-count"
                    && p.Value == $"{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
