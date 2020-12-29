namespace Cadmus.Tgr.Parts.Grammar
{
    /// <summary>
    /// Further occurrence of a quotation in other works, used in
    /// <see cref="VarQuotation"/>.
    /// </summary>
    public class QuotationParallel
    {
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the work citation.
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// Gets or sets the location in the work.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return (string.IsNullOrEmpty(Tag)? "" : $"[{Tag}] ")
                + $"{Work} {Location}";
        }
    }
}
