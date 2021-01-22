using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Codicology
{
    public sealed class MsPlacesPartTest
    {
        private static MsPlacesPart GetPart()
        {
            MsPlacesPartSeeder seeder = new MsPlacesPartSeeder();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (MsPlacesPart)seeder.GetPart(item, null, null);
        }

        private static MsPlacesPart GetEmptyPart()
        {
            return new MsPlacesPart
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
            MsPlacesPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsPlacesPart part2 =
                TestHelper.DeserializePart<MsPlacesPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Places.Count, part2.Places.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsPlacesPart part = GetPart();
            part.Places.Clear();

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
            MsPlacesPart part = GetEmptyPart();

            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Places.Add(new MsPlace
                {
                    Area = even? "even" : "odd",
                    City = even? "Paris" : "Rome",
                    Address = even? "ea, eb" : "oa, ob"
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(11, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            // area
            pin = pins.Find(p => p.Name == "area" && p.Value == "even");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "area" && p.Value == "odd");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "city" && p.Value == "rome");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "city" && p.Value == "paris");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "address" && p.Value == "ea, eb");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "address" && p.Value == "oa, ob");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "address-1" && p.Value == "ea");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "address-2" && p.Value == "eb");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "address-1" && p.Value == "oa");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "address-2" && p.Value == "ob");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
