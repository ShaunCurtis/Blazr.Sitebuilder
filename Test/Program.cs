using Markdig;

string mdpath = Environment.CurrentDirectory + @"..\..\..\..\article.md";

string markdownText = File.ReadAllText(mdpath);

var htmltext = Markdown.ToHtml(markdownText);

Console.WriteLine(htmltext);
