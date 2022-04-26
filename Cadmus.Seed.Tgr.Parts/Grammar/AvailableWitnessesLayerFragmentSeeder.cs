using Bogus;
using Cadmus.Core;
using Cadmus.Core.Layers;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Tgr.Parts.Grammar
{
    /// <summary>
    /// Seeder for <see cref="AvailableWitnessesLayerFragment"/>.
    /// <para>Tag: <c>seed.fr.it.vedph.tgr.available-witnesses</c>.</para>
    /// </summary>
    /// <seealso cref="FragmentSeederBase" />
    [Tag("seed.fr.it.vedph.tgr.available-witnesses")]
    public sealed class AvailableWitnessesLayerFragmentSeeder : FragmentSeederBase
    {
        /// <summary>
        /// Gets the type of the fragment.
        /// </summary>
        /// <returns>Type.</returns>
        public override Type GetFragmentType() => typeof(AvailableWitnessesLayerFragment);

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="location">The location.</param>
        /// <param name="baseText">The base text.</param>
        /// <returns>
        /// A new fragment.
        /// </returns>
        /// <exception cref="ArgumentNullException">location or baseText</exception>
        public override ITextLayerFragment GetFragment(IItem item,
            string location, string baseText)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));
            if (baseText == null)
                throw new ArgumentNullException(nameof(baseText));

            return new Faker<AvailableWitnessesLayerFragment>()
                .RuleFor(fr => fr.Location, location)
                .RuleFor(fr => fr.Witnesses,
                    AvailableWitnessesPartSeeder.GenerateWitnesses(
                        Randomizer.Seed.Next(1, 5)))
                .Generate();
        }
    }
}
