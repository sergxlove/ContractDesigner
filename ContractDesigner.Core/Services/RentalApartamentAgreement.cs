using ContractDesigner.Core.Abstractions;
using ContractDesigner.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ContractDesigner.Core.Services
{
    public class RentalApartamentAgreement : IRentalApartamentAgreement
    {
        public byte[] Generate(RentalApartamentAgreementOptions options)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(14).FontFamily("Times New Roman"));

                    page.Header()
                        .Text("ДОГОВОР НАЙМА (АРЕНДЫ) ЖИЛОГО ПОМЕЩЕНИЯ")
                        .FontSize(16)
                        .Bold()
                        .AlignCenter();

                    page.Content()
                        .PaddingVertical(10)
                        .Column(column =>
                        {
                            GenerateItem1(column, options);
                        });

                    page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.CurrentPageNumber();
                    });
                });


            }).GeneratePdf();
        }

        private void GenerateItem1(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingBottom(10).Text("1. Общие положения").FontSize(14).Bold();

            column.Item().PaddingBottom(5).Text(text =>
            {
                text.Span("Настоящий договор заключен между:");
                text.EmptyLine();
                text.Span("Наймодателем (Арендодателем):").Bold();
                text.EmptyLine();
                text.Span($"{options.FullNameLandlord}").Underline();
                text.EmptyLine();
                text.Span($"    паспорт:     ");
                text.Span($"{options.PassportDataLandlord}").Underline();
                text.EmptyLine();
                text.Span($"выдан:    ");
                text.Span($"{options.IssuedLandlord}").Underline();
                text.EmptyLine();
                text.Span($"    зарегистрирован по адресу:       ");
                text.Span($"{options.AddressRegLandlord}");
                text.EmptyLine();
                text.Span("    в дальнейшем именуемый «Арендодатель»,");
                text.EmptyLine();
                text.Span($"И");
                text.EmptyLine();
                text.Span("Нанимателем (Арендатором):").Bold();
                text.EmptyLine();
                text.Span($"{options.FullNameTenant}").Underline();
                text.EmptyLine();
                text.Span($"    паспорт:     ");
                text.Span($"{options.PassportDataTenant}").Underline();
                text.EmptyLine();
                text.Span($"выдан:    ");
                text.Span($"{options.IssuedTenant}").Underline();
                text.EmptyLine();
                text.Span($"    зарегистрирован по адресу:       ");
                text.Span($"{options.AddressRegTenant}");
                text.EmptyLine();
                text.Span("    в дальнейшем именуемый «Арендатор»,");
                text.EmptyLine();
            });
        }

        private void GenerateItem2(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("2. Предмет договора").FontSize(12).Bold();

            column.Item().Text(text =>
            {
                text.Span("2.1 В соответствии с условиями настоящего Договора Арендодатель предоставляет, ");
                text.Span("а Арендатор принимает за плату во временное пользование для проживания объект ");
                text.Span($"по адресу: {options.ApartamentAddress} общей площадью {options.ApartamentSquare} кв.м. ");
                text.Span("(в дальнейшем именуется «Жилое помещение»). Право собственности Арендодателя ");
                text.Span($"на Жилое помещение подтверждается {options.OwnershipDocument}.");
            });

            column.Item().Text(text =>
            {
                text.Span("2.2 Арендодатель предоставляет в пользование Жилое помещение без права его передачи" +
                    " в субаренду/поднаем и свободным от претензий третьих лиц, которые могут воспрепятствовать" +
                    " пользованию Жилым помещением. Если на момент сдачи Жилого помещения имеются другие" +
                    " собственники квартиры, то Арендодатель получил от них согласие на сдачу этого Жилого" +
                    " помещения по настоящему Договору.");
            });
        }
    }
}
