using PdfSharp.Fonts;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class CustomFontResolver: IFontResolver
    {

        public byte[] GetFont(string faceName)
        {
          
            string fontPath = @"C:\Windows\Fonts\arial.ttf";

            return File.ReadAllBytes(fontPath);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
           
            return new FontResolverInfo("Arial");
        }
    }
}
