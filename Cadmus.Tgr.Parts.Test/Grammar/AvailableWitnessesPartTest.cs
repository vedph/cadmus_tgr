using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Grammar
{
    public sealed class AvailableWitnessesPartTest
    {
        private static AvailableWitnessesPart GetPart()
        {
            AvailableWitnessesPartSeeder seeder =
                new AvailableWitnessesPartSeeder();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (AvailableWitnessesPart)seeder.GetPart(item, null, null);
        }

        private static AvailableWitnessesPart GetEmptyPart()
        {
            return new AvailableWitnessesPart
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
            AvailableWitnessesPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            AvailableWitnessesPart part2 =
                TestHelper.DeserializePart<AvailableWitnessesPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Witnesses.Count, part2.Witnesses.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            AvailableWitnessesPart part = GetPart();
            part.Witnesses.Clear();

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
            AvailableWitnessesPart part = GetEmptyPart();

            const string ids = "ABC";
            for (int n = 1; n <= 3; n++)
            {
                part.Witnesses.Add(new AvailableWitness
                {
                    Id = new string(ids[n - 1], 1),
                    IsPartial = n == 2,
                    Note = "A note"
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "witness" && p.Value == "A");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "witness" && p.Value == "B");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "witness" && p.Value == "C");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
