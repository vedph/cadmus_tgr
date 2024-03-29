﻿using System.Text;

namespace Cadmus.Tgr.Parts.Codicology;

/// <summary>
/// A location in a manuscript.
/// The location includes a number (either Arabic or Roman, always uppercase)
/// plus 0, 1 or 2 lowercase letters, where <c>r</c>=recto, <c>v</c>=verso,
/// <c>rv</c>=both, while the first letters of the alphabet represent
/// columns (<c>a</c>=1st column etc.). If the location refers to pages rather
/// than to folia, it ends with <c>%</c>.
/// </summary>
public class MsLocation
{
    /// <summary>
    /// Gets or sets the sheet number.
    /// </summary>
    public int N { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MsLocation"/>
    /// is a Roman number.
    /// </summary>
    public bool R { get; set; }

    /// <summary>
    /// Gets or sets the suffix, containing 0-2 lowercase letters.
    /// </summary>
    public string? S { get; set; }

    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    public int L { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MsLocation"/>
    /// refers to folia (false) or pages (true).
    /// </summary>
    /// <value><c>true</c> if pages; otherwise, <c>false</c>.</value>
    public bool P { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(N);
        if (!string.IsNullOrEmpty(S)) sb.Append(S);
        if (L > 0) sb.Append(L);
        if (P) sb.Append('%');
        return sb.ToString();
    }
}
