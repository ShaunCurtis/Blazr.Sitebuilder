﻿/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public class TOCItem
{
public int Level { get; set; } = 0;
public string Title { get; set; } = String.Empty;
public string Link { get; set; } = String.Empty;
public bool Hidden { get; set; } = false;
public List<TOCItem> Nodes { get; } = new List<TOCItem>();
}
