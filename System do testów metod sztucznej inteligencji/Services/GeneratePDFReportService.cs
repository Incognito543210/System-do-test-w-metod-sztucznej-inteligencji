using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using PdfSharp.Fonts;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class GeneratePDFReportService : IGeneratePDFReport
    {
        private readonly IParamInfoService _paramInfoService;

        public GeneratePDFReportService(IParamInfoService paramInfoService)
        {
            _paramInfoService = paramInfoService;
        }

        public bool GenerateReport(ICollection<SolveInput> solveInputs, ICollection<SolveOutput> outputs)
        {
            string pathToFilePDF = @"C:\Users\Lenovo\Desktop\raport3.pdf";
            GlobalFontSettings.FontResolver = new CustomFontResolver();
            try
            {
                using (PdfDocument document = new PdfDocument())
                {
                   

                    for (int i = 0; i < solveInputs.Count; i++)
                    {
                        PdfPage page = document.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        XFont font = new XFont("Arial", 12);


                        float startY = 20;


                        gfx.DrawString("Raport", font, XBrushes.Black, new XRect(10, startY, page.Width, 0), XStringFormats.TopLeft); startY += 15;
                        
                        gfx.DrawString("----------------------------------------------------------", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        
                        SolveInput solveInput = solveInputs.ElementAt(i);
                        SolveOutput solveOutput = outputs.ElementAt(i);

                        ICollection<ParamInfo> paramsInfo = _paramInfoService.GetParamsInfo(solveInput.Algorithm);

                        gfx.DrawString($"Algorytm: {solveInput.Algorithm}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        gfx.DrawString($"Funkcja: {solveInput.Function}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        gfx.DrawString($"Wartość funkcji celu: {solveOutput.solveBest[0]}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        gfx.DrawString("Najlepszy osobnik: ", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;

                        for (int k = 1; k < solveOutput.solveBest.Length; k++)
                        {
                            gfx.DrawString($"{solveOutput.solveBest[k]}, ", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        }

                        gfx.DrawString("", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        gfx.DrawString($"{paramsInfo.ElementAt(0).Name}: {solveInput.Parameters[0]}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        gfx.DrawString($"{paramsInfo.ElementAt(1).Name}: {solveInput.Parameters[1]}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                        gfx.DrawString($"{paramsInfo.ElementAt(2).Name}: {solveInput.Min.Length}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;

                        int p = 2;
                        for (int k = 3; k < paramsInfo.Count; k++)
                        {
                            gfx.DrawString($"{paramsInfo.ElementAt(k).Name}: {solveInput.Parameters[p]}", font, XBrushes.Black, new XRect(10, startY, page.Width, 0)); startY += 15;
                            p++;
                        }
                    }

                   
                    document.Save(pathToFilePDF);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisywania raportu PDF: {ex.Message}");
                return false;
            }
        }
    }
}
