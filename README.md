# Blazr.Sitebuilder

I've made this Repo public so anyone can look at my Blazor SiteBuilder code.  I'm plkanning to write an article on the subject at some point.

The site this builds is my personal Github site - https://shauncurtis.github.io/.

There's two projects:

*Blazr.Sitebuilder* is the base code.
*Blazr.Sitebuilder.Builder* is a web application builder project.  On stsrtup it builds the site from the source content files into *wwwroot* and then starts up the Server.

Once you've built the the site you can copy the contents of *wwwroot* to your static site.

It's still a bit querky as it's first pass code.

As an entry point to the code start with the `SiteBuilderFactory` service class.

Good luck.