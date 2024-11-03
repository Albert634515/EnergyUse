using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports;

public class ReportBase
{
    public Cell GetBoldText(string text, int rowspan = 1, int colspan = 1, iText.Layout.Properties.TextAlignment cellAlignment = iText.Layout.Properties.TextAlignment.RIGHT)
    {
        PdfFont bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
        Text first = new Text(text).SetFont(bold);
        Paragraph paragraph = new Paragraph().Add(first);
        Cell cell = new(rowspan, colspan);
        cell.SetFontSize(9);
        cell.Add(paragraph);
        cell.SetTextAlignment(cellAlignment);

        return cell;
    }

    public Cell GetBoldTextGrey(string text, int rowspan = 1, int colspan = 1, iText.Layout.Properties.TextAlignment cellAlignment = iText.Layout.Properties.TextAlignment.RIGHT)
    {
        PdfFont bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
        Text first = new Text(text).SetFont(bold);
        Paragraph paragraph = new Paragraph().Add(first);
        Cell cell = new(rowspan, colspan);
        cell.SetFontSize(9);
        cell.Add(paragraph);
        cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
        cell.SetTextAlignment(cellAlignment);

        return cell;
    }

    public Cell GetNormalText(string text, int rowspan = 1, int colspan = 1, iText.Layout.Properties.TextAlignment cellAlignment = iText.Layout.Properties.TextAlignment.RIGHT, string footerCounter = "")
    {
        PdfFont normal = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
        Text first = new Text(text).SetFont(normal);
        Paragraph paragraph = new Paragraph().Add(first);
        if (!string.IsNullOrWhiteSpace(footerCounter))
            paragraph.Add(new Text(footerCounter).SetFont(normal).SetTextRise(7).SetFontSize(6));

        Cell cell = new(rowspan, colspan);
        cell.SetFontSize(9);
        cell.Add(paragraph);
        cell.SetTextAlignment(cellAlignment);

        return cell;
    }

    public void GetSectionHeader(Table table, string headerText)
    {
        table.SetKeepTogether(true);

        table.AddHeaderCell(GetBoldTextGrey(headerText, 1, 8, iText.Layout.Properties.TextAlignment.LEFT));
    }
}