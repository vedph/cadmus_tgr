using System;
using Xunit;
using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using System.Linq;
using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Test.Codicology
{
    public sealed class MsHistoryPartTest
    {
        private static MsHistoryPart GetPart()
        {
            MsHistoryPartSeeder seeder = new MsHistoryPartSeeder();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (MsHistoryPart)seeder.GetPart(item, null, null);
        }

        private static MsHistoryPart GetEmptyPart()
        {
            return new MsHistoryPart
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
            MsHistoryPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsHistoryPart part2 = TestHelper.DeserializePart<MsHistoryPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Provenances.Count, part2.Provenances.Count);
        }

        [Fact]
        public void GetDataPins_Empty_NoPin()
        {
            MsHistoryPart part = GetEmptyPart();
            Assert.Empty(part.GetDataPins(null));
        }

        [Fact]
        public void GetDataPins_NotEmpty_Ok()
        {
            MsHistoryPart part = GetEmptyPart();
            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Provenances.Add(new GeoAddress
                {
                    Area = even ? "France" : "Italy",
                });
                part.Owners.Add(even ? "even" : "odd");
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "area" && p.Value == "italy");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "area" && p.Value == "france");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "owner" && p.Value == "even");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "owner" && p.Value == "odd");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
