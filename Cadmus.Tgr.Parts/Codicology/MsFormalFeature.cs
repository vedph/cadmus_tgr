namespace Cadmus.Tgr.Parts.Codicology
{
    /// <summary>
    /// A formal feature of a manuscript's writing, included in a
    /// <see cref="MsFormalFeaturesPart"/>.
    /// </summary>
    public class MsFormalFeature
    {
        public string Description { get; set; }
        public string HandId { get; set; }

        public override string ToString()
        {
            return $"[{HandId}] " +
                (Description?.Length > 60
                ? Description.Substring(0, 60) + "..."
                : Description);
        }
    }
}
