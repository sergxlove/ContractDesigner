using ContractDesigner.Core.Abstractions;
using ContractDesigner.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text;

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
                            GenerateItem2(column, options);
                            GenerateItem3(column, options);
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
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("2.2 Арендодатель предоставляет в пользование Жилое помещение без права его передачи" +
                    " в субаренду/поднаем и свободным от претензий третьих лиц, которые могут воспрепятствовать" +
                    " пользованию Жилым помещением. Если на момент сдачи Жилого помещения имеются другие" +
                    " собственники квартиры, то Арендодатель получил от них согласие на сдачу этого Жилого" +
                    " помещения по настоящему Договору.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("2.3 Лица, которые могут проживать в Жилом помещении с арендатором (ФИО," +
                    " серия и номер паспорта каждого человека):    ");
                if (options.PeoplesLives.Count == 0)
                    text.Span("отсутствуют").Underline();
                else
                {
                    StringBuilder sb = new();
                    foreach (People p in options.PeoplesLives)
                    {
                        sb.Append($"{p.FullName}  {p.PassportData}  ;  ");
                    }
                    text.Span($"{sb}").Underline();
                }
                text.Span("а также ");
                text.Span($"   {options.QuantityChildren}   ").Underline();
                text.Span("детей.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("2.4 Проживание в Жилом помещении иных лиц, осуществляется только на основании " +
                    "письменного соглашения Арендодателя.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("2.5 Нахождение в жилом помещении животных ");
                if(options.IsCanAnimals)
                {
                    text.Span("запрещено/");
                    text.Span("разрешено").Underline();
                    text.Span(" для следующих животных: ");
                    text.Span($"{options.TypeAnimal}").Underline();
                    text.EmptyLine();
                }
                else
                {
                    text.Span("запрещено/").Underline();
                    text.Span("разрешено.");
                }
                text.EmptyLine();
            });
        }

        private void GenerateItem3(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("3. Срок действия договора и досрочное расторжение")
                .FontSize(12).Bold();

            column.Item().Text(text =>
            {
                text.Span("3.1 Срок действия договора: ");
                text.Span($"  {options.QuantityMonthAgreement}  ").Underline(); 
                text.Span(" месяцев начиная с «");
                text.Span($"{options.DateStartAgreement.DayNumber}").Underline(); 
                text.Span("» ");
                text.Span($"{options.DateStartAgreement.Month}").Underline(); 
                text.Span($" {options.DateStartAgreement.Year}").Underline(); 
                text.Span(" г.");
                text.EmptyLine();
            });

            column.Item().Text("(дата должна совпадать с датой в Акте передачи Жилого помещения" +
                " (Приложение №1))");

            column.Item().Text(text =>
            {
                text.EmptyLine();
                text.Span("3.2 Жилое помещение считается переданным Арендодателем Арендатору в день " +
                    "подписания Акта передачи Жилого помещения (Приложение №1).");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("3.3 Вместе с Жилым помещением Арендатору передается Имущество в соответствии с" +
                    " описью в Приложении №1.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("3.4 Не позднее чем за ");
                text.Span($"  {options.QuantityMonthBeforeOffer}  ").Underline(); 
                text.Span(" месяц(а) до истечения срока Договора Арендодатель должен предложить" +
                    " Арендатору заключить договор на тех же или иных условиях либо предупредить" +
                    " Арендатора об отказе от продления договора. Если Арендодатель не выполнил этой" +
                    " обязанности, а Арендатор не отказался от продления договора, договор считается" +
                    " продленным на тех же условиях и на тот же срок.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("3.5. Стороны вправе расторгнуть договор по взаимному согласию, " +
                    "а также по инициативе любой из Сторон с предупреждением другой Стороны за ");
                text.Span($"  {options.QuantityDayWarningCancel}  ").Underline();
                text.Span(" дней до момента расторжения Договора.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("3.6 В случае не продления или досрочного прекращения Договора Арендатор " +
                    "обязуется освободить Жилое помещение и передать его Арендодателю по Акту возврату" +
                    " Жилого помещения (Приложение № 2 к Договору) не позднее дня не продления или " +
                    "досрочного прекращения Договора.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("3.7 В случае нарушения сроков освобождения и возврата Арендодателю в" +
                    " пользование Жилого помещения, установленных этим Договором, Арендатор уплачивает" +
                    " Арендодателю неустойку в размере ");
                text.Span($" {options.Compensation} ").Underline(); 
                text.Span(" рублей за каждый дополнительный день проживания в Жилом помещении, то есть" +
                    " за каждый день нарушения сроков освобождения и возврата Арендодателю в пользование " +
                    "Жилого помещения.");
                text.EmptyLine();
            });
        }

        private void GenerateItem4(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("4. Платежи по договору (Плата за жилое" +
                " помещение и комунальные услуги)")
               .FontSize(12).Bold();
            column.Item().Text(text =>
            {
                text.Span("4.1 Размер платы за жилое помещение устанавливается в размере ");
                text.Span($"{options.PaymentMonth}").Underline();
                text.Span(" рублей в месяц.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Span("4.2 Плата за жилое помещение выплачивается авансом ежемесячно до числа месяца, " +
                    "соответствующего дате вселения. Если в месяц платежа такое число отсутствует, то " +
                    "оплата производится до последнего числа этого месяца");
                text.EmptyLine();
            });

            int currrentItem = 4;
            column.Item().Text(text =>
            {
                text.Span("4.3 Стоимость комунальных услуг в стоимость платы за жилое помещение  ");
                if (options.IsHaveCom)
                {
                    text.Span("входит").Underline();
                    text.Span("/не входит");
                }
                else
                {
                    text.Span("входит");
                    text.Span("/не входит").Underline();
                }
                text.EmptyLine();
            });

            if(!options.IsHaveCom)
            {
                column.Item().Text(text => 
                {
                    text.Span($"4.{currrentItem} В стоимость комунальных услуг, которые Арендатор оплачивает" +
                        $" ежемесячно за свой счет, входят: ");
                    StringBuilder sb = new();
                    foreach(string s in options.ServiceCom)
                    {
                        sb.Append(s);
                        sb.Append(' ');
                    }
                    text.Span($"{sb}");
                    text.EmptyLine();
                });
                currrentItem++;
            }

            column.Item().Text(text =>
            {
                text.Span($"4.{currrentItem} Арендатор обязан самостоятельно принять меры для уведомления " +
                    $"Арендодателя о показанях счетчиков, сумме коммунальных платежей к оплате.");
                text.EmptyLine();
            });
            currrentItem++;

            column.Item().Text(text =>
            {
                text.Span($"4.{currrentItem} Стороны самостоятельно договариваются о способе оплаты за " +
                    $"Жилое помещение и комунальных услуг");
                text.EmptyLine();
            });
        }
    }
}
