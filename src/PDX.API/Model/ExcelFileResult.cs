using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;

namespace PDX.API.Model {
    public class ExcelFileResult : FileResult {
        private IEnumerable<dynamic> _data;
        private IList<JObject> _columns;
        public ExcelFileResult (IEnumerable<dynamic> data, IList<JObject> columns) : base ("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
            _data = data;
            _columns = columns;
            FileDownloadName = "Excel.xlsx";
        }

        public async override Task ExecuteResultAsync (ActionContext context) {
            var response = context.HttpContext.Request;
            var enumerable = _data as System.Collections.IEnumerable;
            if (enumerable == null) {
                throw new ArgumentException ("IEnumerable type required");
            }

            byte[] FileContents = null;
            using (MemoryStream mem = new MemoryStream ()) {
                using (var workbook = SpreadsheetDocument.Create (mem, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook)) {
                    var workbookPart = workbook.AddWorkbookPart ();
                    workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook ();
                    workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets ();
                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart> ();
                    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData ();
                    sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet (sheetData);

                    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets> ();
                    string relationshipId = workbook.WorkbookPart.GetIdOfPart (sheetPart);
                    uint sheetId = 1;
                    if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet> ().Count () > 0) {
                        sheetId = sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet> ().Select (s => s.SheetId.Value).Max () + 1;
                    }

                    DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet () { Id = relationshipId, SheetId = sheetId, Name = "Sheet1" };
                    sheets.Append (sheet);

                    //add logo
                    string imgPath = "assets/logo.png";
                    var drawingsPart = sheetPart.AddNewPart<DrawingsPart> ();

                    if (!sheetPart.Worksheet.ChildElements.OfType<Drawing> ().Any ()) {
                        sheetPart.Worksheet.Append (new Drawing { Id = sheetPart.GetIdOfPart (drawingsPart) });
                    }

                    if (drawingsPart.WorksheetDrawing == null) {
                        drawingsPart.WorksheetDrawing = new WorksheetDrawing ();
                    }

                    var worksheetDrawing = drawingsPart.WorksheetDrawing;

                    var imagePart = drawingsPart.AddImagePart (ImagePartType.Png);

                    using (var stream = new FileStream (imgPath, FileMode.Open)) {
                        imagePart.FeedData (stream);
                    }

                    Bitmap bm = new Bitmap (imgPath);
                    DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents ();
                    var extentsCx = (long) bm.Width * (long) ((float) 31440 / bm.HorizontalResolution);
                    var extentsCy = (long) bm.Height * (long) ((float) 31440 / bm.VerticalResolution);
                    bm.Dispose ();

                    var colOffset = 2;
                    var rowOffset = 0;
                    int colNumber = 4;
                    int rowNumber = 1;

                    var nvps = worksheetDrawing.Descendants<DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties> ();
                    var nvpId = nvps.Count () > 0 ?
                        (UInt32Value) worksheetDrawing.Descendants<DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties> ().Max (p => p.Id.Value) + 1 :
                        1U;

                    var oneCellAnchor = new DocumentFormat.OpenXml.Drawing.Spreadsheet.OneCellAnchor (
                        new DocumentFormat.OpenXml.Drawing.Spreadsheet.FromMarker {
                            ColumnId = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnId ((colNumber - 1).ToString ()),
                                RowId = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowId ((rowNumber - 1).ToString ()),
                                ColumnOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnOffset (colOffset.ToString ()),
                                RowOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowOffset (rowOffset.ToString ())
                        },
                        new DocumentFormat.OpenXml.Drawing.Spreadsheet.Extent { Cx = extentsCx, Cy = extentsCy },
                        new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture (
                            new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties (
                                new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties { Id = nvpId, Name = "Picture " + nvpId, Description = imgPath },
                                new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties (new DocumentFormat.OpenXml.Drawing.PictureLocks { NoChangeAspect = true })
                            ),
                            new DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill (
                                new DocumentFormat.OpenXml.Drawing.Blip { Embed = drawingsPart.GetIdOfPart (imagePart), CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print },
                                new DocumentFormat.OpenXml.Drawing.Stretch (new DocumentFormat.OpenXml.Drawing.FillRectangle ())
                            ),
                            new DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties (
                                new DocumentFormat.OpenXml.Drawing.Transform2D (
                                    new DocumentFormat.OpenXml.Drawing.Offset { X = 0, Y = 0 },
                                    new DocumentFormat.OpenXml.Drawing.Extents { Cx = extentsCx, Cy = extentsCy }
                                ),
                                new DocumentFormat.OpenXml.Drawing.PresetGeometry { Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle }
                            )
                        ),
                        new DocumentFormat.OpenXml.Drawing.Spreadsheet.ClientData ()
                    );

                    worksheetDrawing.Append (oneCellAnchor);

                    sheetData.AppendChild (new DocumentFormat.OpenXml.Spreadsheet.Row ());
                    sheetData.AppendChild (new DocumentFormat.OpenXml.Spreadsheet.Row ());
                    sheetData.AppendChild (new DocumentFormat.OpenXml.Spreadsheet.Row ());
                    sheetData.AppendChild (new DocumentFormat.OpenXml.Spreadsheet.Row ());

                    //company name
                    DocumentFormat.OpenXml.Spreadsheet.Row company = new DocumentFormat.OpenXml.Spreadsheet.Row ();
                    DocumentFormat.OpenXml.Spreadsheet.Cell cellCompany = new DocumentFormat.OpenXml.Spreadsheet.Cell () { StyleIndex = (UInt32Value) 1U };
                    cellCompany.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cellCompany.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue ("FOOD, MEDICINE AND HEALTH CARE ADMINISTRATION AND CONTROL AUTHORITY OF ETHIOPIA");
                    company.AppendChild (cellCompany);
                    sheetData.AppendChild (company);

                    sheetData.AppendChild (new DocumentFormat.OpenXml.Spreadsheet.Row ());

                    //header
                    DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row ();

                    foreach (var column in _columns) {
                        if (!(bool) column["IsVisible"]) continue;
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell ();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue (column["Title"].ToString ());
                        headerRow.AppendChild (cell);
                    }

                    sheetData.AppendChild (headerRow);

                    foreach (var item in enumerable) {
                        IDictionary<string, object> row = (IDictionary<string, object>) item;
                        DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row ();

                        foreach (var header in _columns) {
                        if (!(bool) header["IsVisible"]) continue;
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell ();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                        var value = row[header["FieldName"].ToString ()];
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue (value?.ToString ()); //
                        newRow.AppendChild (cell);
                        }
                        sheetData.AppendChild (newRow);
                    }

                    //geenrated by
                    sheetData.AppendChild (new DocumentFormat.OpenXml.Spreadsheet.Row ());

                    DocumentFormat.OpenXml.Spreadsheet.Row footer = new DocumentFormat.OpenXml.Spreadsheet.Row ();
                    DocumentFormat.OpenXml.Spreadsheet.Cell footerCell = new DocumentFormat.OpenXml.Spreadsheet.Cell (new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowOffset ("4"));
                    footerCell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    footerCell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue ("Generated By eRIS");

                    footer.AppendChild (footerCell);
                    sheetData.AppendChild (footer);

                    sheetPart.Worksheet.Save ();
                    workbook.WorkbookPart.Workbook.Save ();
                    workbook.Close ();
                    FileContents = mem.ToArray ();

                    var contentResult = new FileContentResult (FileContents, this.ContentType);
                    await contentResult.ExecuteResultAsync (context);

                    //await response.Body.WriteAsync(FileContents, 0, FileContents.Length);
                }
            }

        }

        private static Stylesheet CreateStylesheet () {
            Stylesheet stylesheet1 = new Stylesheet ();

            DocumentFormat.OpenXml.Spreadsheet.Fonts fonts1 = new DocumentFormat.OpenXml.Spreadsheet.Fonts () { Count = (UInt32Value) 1U, KnownFonts = true };

            DocumentFormat.OpenXml.Spreadsheet.Font font1 = new DocumentFormat.OpenXml.Spreadsheet.Font ();
            FontSize fontSize1 = new FontSize () { Val = 11 };
            Color color1 = new Color () { Theme = (UInt32Value) 1U };
            FontName fontName1 = new FontName () { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering () { Val = 2 };
            DocumentFormat.OpenXml.Spreadsheet.FontScheme fontScheme1 = new DocumentFormat.OpenXml.Spreadsheet.FontScheme () { Val = FontSchemeValues.Minor };
            font1.Append (fontSize1);
            font1.Append (color1);
            font1.Append (fontName1);
            font1.Append (fontFamilyNumbering1);
            font1.Append (fontScheme1);

            DocumentFormat.OpenXml.Spreadsheet.Font font2 = new DocumentFormat.OpenXml.Spreadsheet.Font ();
            FontSize fontSize2 = new FontSize () { Val = 14 };
            Color color2 = new Color () { Rgb = "FF0070C0" };
            FontName fontName2 = new FontName () { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering () { Val = 2 };
            DocumentFormat.OpenXml.Spreadsheet.FontScheme fontScheme2 = new DocumentFormat.OpenXml.Spreadsheet.FontScheme () { Val = FontSchemeValues.Minor };
            font2.Append (fontSize1);
            font1.Append (color2);
            font1.Append (fontName2);
            font1.Append (fontFamilyNumbering2);
            font1.Append (fontScheme2);

            fonts1.Append (font1);
            fonts1.Append (font2);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats () { Count = (UInt32Value) 1U };
            CellFormat cellFormat1 = new CellFormat () { NumberFormatId = (UInt32Value) 0U, FontId = (UInt32Value) 1U };

            cellStyleFormats1.Append (cellFormat1);

            CellFormats cellFormats1 = new CellFormats () { Count = (UInt32Value) 4U };
            CellFormat cellFormat2 = new CellFormat () { NumberFormatId = (UInt32Value) 0U, FontId = (UInt32Value) 1U };
            CellFormat cellFormat3 = new CellFormat () { NumberFormatId = (UInt32Value) 0U, FontId = (UInt32Value) 1U };

            cellFormats1.Append (cellFormat2);
            cellFormats1.Append (cellFormat3);

            CellStyles cellStyles1 = new CellStyles () { Count = (UInt32Value) 1U };
            CellStyle cellStyle1 = new CellStyle () { Name = "Normal", FormatId = (UInt32Value) 0U, BuiltinId = (UInt32Value) 0U };

            cellStyles1.Append (cellStyle1);

            stylesheet1.Append (fonts1);
            stylesheet1.Append (cellStyleFormats1);
            stylesheet1.Append (cellFormats1);
            stylesheet1.Append (cellStyles1);
            return stylesheet1;
        }
    }
}