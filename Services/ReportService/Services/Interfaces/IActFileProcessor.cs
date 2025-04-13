using ReportService.Models.Domain;
using Shared.DependencyInjection.Interfaces;

namespace ReportService.Services.Interfaces;

public interface IActFileProcessor : ITransient
{
    byte[] FillTemplateBase64(ControlAct act, string templatePath);
}