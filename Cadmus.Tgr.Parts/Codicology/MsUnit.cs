using Cadmus.Parts.General;
using Cadmus.Tgr.Parts.Codicology;
using System.Collections.Generic;

namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// A unit in a manuscript's codicological description.
    /// </summary>
    public class MsUnit
    {
        /// <summary>
        /// Gets or sets the start location.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the end location.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Gets or sets the group identifier for this unit, when it is
        /// reconstructed as belonging to a codex disiectus.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group ordinal. This is used in conjunction
        /// with <see cref="GroupId"/> and represents the position of this unit
        /// in the reconstructed codex disiectus.
        /// </summary>
        public int GroupOrdinal { get; set; }

        /// <summary>
        /// Gets or sets the palimpsests.
        /// </summary>
        public List<MsPalimpsest> Palimpsests { get; set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Gets or sets the sheets count.
        /// </summary>
        public int SheetCount { get; set; }

        /// <summary>
        /// Gets or sets the guard sheets count.
        /// </summary>
        public int GuardSheetCount { get; set; }

        /// <summary>
        /// Gets or sets the guard sheets.
        /// </summary>
        public List<MsGuardSheet> GuardSheets { get; set; }

        /// <summary>
        /// Gets or sets the quires description.
        /// </summary>
        public string Quires { get; set; }

        /// <summary>
        /// Gets or sets the sheet numbering description.
        /// </summary>
        public string SheetNumbering { get; set; }

        /// <summary>
        /// Gets or sets the quire numbering description.
        /// </summary>
        public string QuireNumbering { get; set; }

        /// <summary>
        /// Gets or sets the leaf sizes.
        /// </summary>
        public List<PhysicalSize> LeafSizes { get; set; }

        /// <summary>
        /// Gets or sets the size of the written area.
        /// </summary>
        public PhysicalSize WrittenAreaSize { get; set; }

        /// <summary>
        /// Gets or sets the rulings.
        /// </summary>
        public List<MsRuling> Rulings { get; set; }

        /// <summary>
        /// Gets or sets the watermarks.
        /// </summary>
        public List<MsWatermark> Watermarks { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the binding.
        /// </summary>
        public string Binding { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsUnit"/> class.
        /// </summary>
        public MsUnit()
        {
            Palimpsests = new List<MsPalimpsest>();
            GuardSheets = new List<MsGuardSheet>();
            LeafSizes = new List<PhysicalSize>();
            Rulings = new List<MsRuling>();
            Watermarks = new List<MsWatermark>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Start}-{End}: {Material}";
        }
    }
}
