using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ReportService.Models.Domain;
using ReportService.Models.Enums;
using ReportService.Services.Interfaces;

namespace ReportService.Services;

public class ActFileProcessor : IActFileProcessor
{
    public byte[] FillTemplateBase64(ControlAct act, string templatePath)
    {
        var tempFile = Path.GetTempFileName().Replace(".tmp", ".docx");
        File.Copy(templatePath, tempFile, overwrite: true);

        using (var doc = WordprocessingDocument.Open(tempFile, true))
        {
            var body = doc.MainDocumentPart!.Document.Body!;

            ReplacePlaceholder(body, "{{Address}}", act.Address);

            if (act.HasCommutingDevice)
            {
                ReplacePlaceholder(body, "{{HasCommutingDevice}}", "X");
                ReplacePlaceholder(body, "{{HasNotCommutingDevice}}", "");
            }
            else
            {
                ReplacePlaceholder(body, "{{HasCommutingDevice}}", "");
                ReplacePlaceholder(body, "{{HasNotCommutingDevice}}", "X");
            }

            if (act.HasViolation)
            {
                ReplacePlaceholder(body, "{{HasViolations}}", "X");
                ReplacePlaceholder(body, "{{HasNotViolations}}", "");
            }
            else
            {
                ReplacePlaceholder(body, "{{HasViolations}}", "");
                ReplacePlaceholder(body, "{{HasNotViolations}}", "X");
            }

            switch (act.MeteringDeviceLocationType)
            {
                case MeteringDeviceLocation.OnLanding:
                    ReplacePlaceholder(body, "{{LocationOnLanding}}", "X");
                    ReplacePlaceholder(body, "{{LocationInFlat}}", "");
                    break;
                case MeteringDeviceLocation.InFlat:
                    ReplacePlaceholder(body, "{{LocationOnLanding}}", "");
                    ReplacePlaceholder(body, "{{LocationInFlat}}", "X");
                    break;
                default:
                    ReplacePlaceholder(body, "{{LocationOnLanding}}", "");
                    ReplacePlaceholder(body, "{{LocationInFlat}}", "");
                    break;
            }

            ReplacePlaceholder(body, "{{DeviceReadings}}", act.DeviceReadings);
            ReplacePlaceholder(body, "{{FirstInspector}}", act.Workers[0]);
            ReplacePlaceholder(body, "{{SecondInspector}}", act.Workers[1]);

            if (act.HasViolation)
            {
                // ReplaceDrawingWithImage(doc, "checkbox_hasNotViolation", "Templates/checked.png");
                // ReplacePlaceholder(body, "{{Check}}", "X");
            }

            doc.MainDocumentPart.Document.Save();
        }

        var fileBytes = File.ReadAllBytes(tempFile);
        File.Delete(tempFile);
        
        return fileBytes;
    }

    private void ReplacePlaceholder(Body body, string placeholder, string value)
    {
        foreach (var text in body.Descendants<Text>())
        {
            if (text.Text.Contains(placeholder))
            {
                text.Text = text.Text.Replace(placeholder, value);
            }
        }
    }
}