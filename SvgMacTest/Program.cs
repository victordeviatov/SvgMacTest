using Aspose.Svg;
using Aspose.Svg.Rendering;
using Aspose.Svg.Rendering.Pdf;
using Aspose.Svg.Services;

class Program
{
    static void Main()
    {
        // 1️⃣  build a configuration object
        using var cfg = new Configuration();

        // 2️⃣  grab the User-Agent service
        var ua = cfg.GetService<IUserAgentService>();

        // 3️⃣  tell it where to look for fonts on macOS runners
        ua.FontsSettings.SetFontsLookupFolders(
            new[] { "/System/Library/Fonts", "/Library/Fonts" }, true);

        // ① create an in-memory SVG
        using (var svg1 = new SVGDocument(
                   "<svg xmlns='http://www.w3.org/2000/svg' width='120' height='120'>" +
                   "<rect x='10' y='10' width='100' height='100' fill='cornflowerblue'/></svg>",
                   "", cfg))
        using (var svg2 = new SVGDocument(
                   "<svg xmlns='http://www.w3.org/2000/svg' width='120' height='120'>" +
                   "<circle cx='60' cy='60' r='50' fill='gold'/></svg>",
                   "", cfg))
        using (var svg3 = new SVGDocument(
                   "<svg xmlns='http://www.w3.org/2000/svg' width='120' height='120'>" +
                   "<text x='10' y='70' font-size='48'>Hi</text></svg>",
                   "", cfg))

        // ② render it to PNG via Skia
        using (var renderer = new SvgRenderer())
        {
            // Create an instance of PdfDevice
            using (var device = new PdfDevice("./SvgMacTest/merged-svg.pdf"))
            {
                // Merge all SVG documents to PDF
                renderer.Render(device, svg1, svg2, svg3);
            }
        }
        System.Console.WriteLine("✅ merged-svg.pdf created");
    }
}
