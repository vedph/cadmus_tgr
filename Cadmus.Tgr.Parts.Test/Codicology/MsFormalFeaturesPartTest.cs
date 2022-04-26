using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Codicology
{
    public sealed class MsFormalFeaturesPartTest
    {
        private static MsFormalFeaturesPart GetPart()
        {
            MsFormalFeaturesPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (MsFormalFeaturesPart)seeder.GetPart(item, null, null);
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsFormalFeaturesPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsFormalFeaturesPart part2 =
                TestHelper.DeserializePart<MsFormalFeaturesPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Features.Count, part2.Features.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsFormalFeaturesPart part = GetPart();
            part.Features.Clear();

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
            MsFormalFeaturesPart part = new()
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };
            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Features.Add(new MsFormalFeature
                {
                    HandId = $"h{n}"
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            // hand-id
            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "hand-id"
                    && p.Value == $"h{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
