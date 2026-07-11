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

                    page.Content()
                        .PaddingVertical(10)
                        .Column(column =>
                        {
                            column.Item().Text("ДОГОВОР НАЙМА (АРЕНДЫ) ЖИЛОГО ПОМЕЩЕНИЯ").FontSize(16)
                                .Bold().AlignCenter();
                            GenerateItem1(column, options);
                            GenerateItem2(column, options);
                            GenerateItem3(column, options);
                            GenerateItem4(column, options);
                            GenerateItem5(column, options);
                            GenerateItem6(column, options);
                            GenerateItem7(column, options);
                            GenerateItem8(column, options);
                            GenerateItem9(column, options);
                            GenerateItem10(column, options);
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
                text.Justify();
                text.Span("Настоящий договор заключен между:");
                text.EmptyLine();
                text.Span("Наймодателем (Арендодателем):").Bold();
                text.EmptyLine();
                text.Span($"{options.FullNameLandlord}").Underline();
                text.EmptyLine();
                text.Span($"паспорт:     ");
                text.Span($"{options.PassportDataLandlord}").Underline();
                text.EmptyLine();
                text.Span($"выдан:    ");
                text.Span($"{options.IssuedLandlord}").Underline();
                text.EmptyLine();
                text.Span($"зарегистрирован по адресу:       ");
                text.Span($"{options.AddressRegLandlord}");
                text.EmptyLine();
                text.Span("в дальнейшем именуемый «Арендодатель»,");
                text.EmptyLine();
                text.Span($"И");
                text.EmptyLine();
                text.Span("Нанимателем (Арендатором):").Bold();
                text.EmptyLine();
                text.Span($"{options.FullNameTenant}").Underline();
                text.EmptyLine();
                text.Span($"паспорт:     ");
                text.Span($"{options.PassportDataTenant}").Underline();
                text.EmptyLine();
                text.Span($"выдан:    ");
                text.Span($"{options.IssuedTenant}").Underline();
                text.EmptyLine();
                text.Span($"зарегистрирован по адресу:       ");
                text.Span($"{options.AddressRegTenant}");
                text.EmptyLine();
                text.Span("в дальнейшем именуемый «Арендатор»,");
                text.EmptyLine();
            });
        }

        private void GenerateItem2(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item()
                .PaddingVertical(10)
                .Text("2. Предмет договора")
                .FontSize(14)
                .Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("2.1 В соответствии с условиями настоящего Договора Арендодатель предоставляет, " +
                    "а Арендатор принимает за плату во временное пользование для проживания объект " +
                    $"по адресу: {options.ApartamentAddress} общей площадью {options.ApartamentSquare} кв.м. " +
                    "(в дальнейшем именуется «Жилое помещение»). Право собственности Арендодателя " +
                    $"на Жилое помещение подтверждается {options.OwnershipDocument}.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("2.2 Арендодатель предоставляет в пользование Жилое помещение без права его передачи" +
                    " в субаренду/поднаем и свободным от претензий третьих лиц, которые могут воспрепятствовать" +
                    " пользованию Жилым помещением. Если на момент сдачи Жилого помещения имеются другие" +
                    " собственники квартиры, то Арендодатель получил от них согласие на сдачу этого Жилого" +
                    " помещения по настоящему Договору.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
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
                text.Span(", а также ");
                text.Span($"   {options.QuantityChildren}   ").Underline();
                text.Span("детей.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("2.4 Проживание в Жилом помещении иных лиц, осуществляется только на основании " +
                    "письменного соглашения Арендодателя.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("2.5 Нахождение в жилом помещении животных ");
                if(options.IsCanAnimals)
                {
                    text.Span("запрещено /");
                    text.Span(" разрешено").Underline();
                    text.Span(" для следующих животных: ");
                    text.Span($"{options.TypeAnimal}").Underline();
                    text.EmptyLine();
                }
                else
                {
                    text.Span("запрещено ").Underline();
                    text.Span("/ разрешено.");
                }
                text.EmptyLine();
            });
        }

        private void GenerateItem3(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("3. Срок действия договора и досрочное расторжение")
                .FontSize(14).Bold();

            column.Item().Text(text =>
            {
                text.Justify();
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
                text.Justify();
                text.EmptyLine();
                text.Span("3.2 Жилое помещение считается переданным Арендодателем Арендатору в день " +
                    "подписания Акта передачи Жилого помещения (Приложение №1).");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("3.3 Вместе с Жилым помещением Арендатору передается Имущество в соответствии с" +
                    " описью в Приложении №1.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
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
                text.Justify();
                text.Span("3.5. Стороны вправе расторгнуть договор по взаимному согласию, " +
                    "а также по инициативе любой из Сторон с предупреждением другой Стороны за ");
                text.Span($"  {options.QuantityDayWarningCancel}  ").Underline();
                text.Span(" дней до момента расторжения Договора.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("3.6 В случае не продления или досрочного прекращения Договора Арендатор " +
                    "обязуется освободить Жилое помещение и передать его Арендодателю по Акту возврату" +
                    " Жилого помещения (Приложение № 2 к Договору) не позднее дня не продления или " +
                    "досрочного прекращения Договора.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
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
               .FontSize(14).Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("4.1 Размер платы за жилое помещение устанавливается в размере ");
                text.Span($"{options.PaymentMonth}").Underline();
                text.Span(" рублей в месяц.");
                text.EmptyLine();
            });

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("4.2 Плата за жилое помещение выплачивается авансом ежемесячно до числа месяца, " +
                    "соответствующего дате вселения. Если в месяц платежа такое число отсутствует, то " +
                    "оплата производится до последнего числа этого месяца");
                text.EmptyLine();
            });

            int currrentItem = 4;
            column.Item().Text(text =>
            {
                text.Justify();
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
                text.Justify();
                text.Span($"4.{currrentItem} Арендатор обязан самостоятельно принять меры для уведомления " +
                    $"Арендодателя о показанях счетчиков, сумме коммунальных платежей к оплате.");
                text.EmptyLine();
            });
            currrentItem++;

            column.Item().Text(text =>
            {
                text.Justify();
                text.Span($"4.{currrentItem} Стороны самостоятельно договариваются о способе оплаты за " +
                    $"Жилое помещение и комунальных услуг");
                text.EmptyLine();
            });
        }

        private void GenerateItem5(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("5. Обеспечительный платеж")
               .FontSize(14).Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("5.1 На момент подписания Договора в качестве обеспечительного платежа внессена сумма" +
                    " в размере ");
                text.Span($"{options.PaymentZl}").Underline();
                text.Span(" рублей");
                text.EmptyLine();
            });
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("5.2 В случае причинения вреда имуществу Арендодателя по вине Арендатора Арендодатель" +
                    " вправе удержать сумму ущерба из обеспечительного платежа, о чем должен уведомить Арендодатора.");
                text.EmptyLine();
            });
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("5.3 Арендатор обязан незамедлительно сообщеить Арендодателю об ущербе имуществу, " +
                    "а также компенсировать ему ущерб, который отдельно или в совокупности с другими фактами" +
                    " нанесения ущерба имуществу составляет больше, чем обеспечительный платеж (п. 5.1 " +
                    "настоящего договора).");
                text.EmptyLine();
            });
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("5.4 Арендодатель обязуется вернуть сумму обеспечительного платежа по истечении " +
                    "срока найма при исполнении Арендатором своих обязательств по Договору, в том числе " +
                    "сохранности имущества Арендодателя. Обеспечительный платеж не засчитывается в счет " +
                    "арендной платы за последний месяци возвращается после полной оплаты всех месяцев.");
                text.EmptyLine();
            });
        }

        private void GenerateItem6(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("6. Права и обязанности сторон").FontSize(14).Bold();
            column.Item().PaddingVertical(2).Text("6.1 Арендодатель обязуется:");

            column.Item().PaddingLeft(15).PaddingVertical(1).Text(text =>
            {
                text.Span("• Предоставить Арендатору Жилое помещение в состоянии, пригодном для проживания." +
                    " Обеспечить беспрепятственное пользование Жилым помещением на условиях договора.");
                text.Span("• Письменно уведомить Арендатора о правах третьих лиц на передаваемое в наем" +
                    " (аренду) Жилое помещение в случае возникновения таких прав.");

            });
            column.Item().PaddingVertical(2).Text("6.2 Арендатор обязуется:");

            column.Item().PaddingLeft(15).PaddingVertical(1).Text(text =>
            {
                text.Span("• Использовать Жилое помещение исключительно в целях проживания и не сдавать " +
                    "помещение в субаренду/поднаем без согласия Арендодателя.");
                text.Span("• Использовать Жилое помещение только для проживания лиц, указанных в Договоре.");
                text.Span("• Обеспечить сохранность Жилого помещения и поддерживать его в надлежащем " +
                    "состоянии.");
                text.Span("• Вносить плату за пользование Жилым помещением и коммунальные услуги в размере," +
                    " порядке и сроках, установленных Договором.");
                text.Span("• При пользовании Жилого помещения соблюдать требования законодательства о " +
                    "пожарной безопасности и об охране окружающей среды и не нарушать покой соседей.");
                text.Span("• Обеспечивать текущий ремонт квартиры, если ущерб был по вине Арендатора. " +
                    "Ремонт и замена сантехники, бытовой техники, электропроводки, вышедших из строя по " +
                    "причине естественного износа, производится за счет Арендодателя в течении ");
                text.Span($" {options.RepairDay} ").Underline();
                text.Span(" дней с момента заявки Арендатора.");
                text.Span("• Обеспечивать доступ в жилое Помещение Арендодателю для проверки состояния " +
                    "Жилого Помещения в случае уведомления Арендодателем за ");
                text.Span($" {options.CheckDay} ").Underline();
                text.Span(" календарных дня(ей) до даты посещения Жилого помещения.");
                text.Span("• Перед передачей помещения Арендодателю по Акту приема-передачи Арендатор" +
                    " обязан провести уборку (клининг) Жилого помещения.");

            });

            column.Item().PaddingVertical(2).Text("6.3 Арендодатель вправе:");

            column.Item().PaddingLeft(15).PaddingVertical(1).Text(text =>
            {
                text.Span("• В присутствии Арендатора осуществлять проверку сохранности, состояния Жилого " +
                    "помещения. Наймодатель должен заранее (за срок указанный в п. 6.2) предупредить " +
                    "Арендатора о намерении осуществить проверку Жилого помещения.");
                text.Span("• В любое время получать доступ к Жилому помещению в случае аварии, пожара, " +
                    "затопления, в иных случаях, угрожающих ущербом Жилому помещению.");
                text.Span("• Прекратить право Арендатора владеть и пользоваться Жилым помещением на " +
                    "основании и в соответствии с пунктов 3.6 Настоящего Договора.");
            });

            column.Item().PaddingVertical(2).Text("6.4 Арендатор вправе:");

            column.Item().PaddingLeft(15).PaddingVertical(1).Text(text =>
            {
                text.Span("• Производить переустройство, перепланировку, реконструкцию и переоборудование " +
                    "или улучшение Жилого помещения только с письменного согласия Арендодателя.");
                text.Span("• Требовать устранения неисправностей, если они возникли не по вине Арендатора;");
                text.Span("• В случае необходимости и с предварительным уведомлением Арендодателя, " +
                    "инициировать пересмотр условий договора.");
                text.Span("• Прекратить проживание и пользование Жилым помещением на основании и в " +
                    "соответствии с пунктов 3.6 Настоящего Договора.");
            });
        }

        private void GenerateItem7(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("7. Ответственность сторон")
               .FontSize(14).Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("7.1 За неисполнение условий настоящего Договора стороны несут ответственность " +
                    "в соответствии с действующим законодательством РФ и условиями настоящего Договора.");
                text.EmptyLine();
                text.Span("7.2 В случае неисполнения или ненадлежащего исполнения своих обязательств " +
                    "по настоящему Договору каждая сторона обязана возместить другой стороне причиненные убытки");
                text.EmptyLine();
                text.Span("7.3 В случае если в связи с действиями Арендатора органы власти или иные третьи " +
                    "лица предъявят Арендодателю претензии/исковыые требования, либо в отношении Арендодателя " +
                    "будут наложены штрафные санкции Арендатор обязан: исправить допущенные нарушения, " +
                    "содействовать в урегулировании отношений с соответствующими органами/третьими лицами и " +
                    "уплатить указанные штрафы");
                text.EmptyLine();
            });
        }

        private void GenerateItem8(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("8. Порядок разрешения споров")
               .FontSize(14).Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("8.1 Все споры и разногласия, возникающие в связи с настоящим договором, стороны " +
                    "обязуются урегулировать путем переговоров. В случае невозможности достижения согласия " +
                    "путем переговоров, спор передается на рассмотрение в судебном порядке в соответствии " +
                    "с законодательством РФ");
                text.EmptyLine();
            });
        }

        private void GenerateItem9(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("9. Прочие положения договора")
              .FontSize(14).Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("9.1 Настоящий договор гражданским законодательством Российской федерации и " +
                    "толкуется в соответствии с ним. Во всем, что не урегулировано настоящим Договором, " +
                    "стороны руководствуются законодательством Российской Федерации.");
                text.EmptyLine();
                text.Span("9.2 Стороны также оговаривают, что принимают Жилое помещение в соответствии с актом " +
                    "(Приложение 1), являющимся неотъемлимой частью настоящего договора. Стороны принимают " +
                    "к сведению показания счетчиков и размер задолженности за коммунальные услуги или иные " +
                    "задолженности, связанные с использованием Жилого помещения. ");
                text.EmptyLine();
            });
        }

        private void GenerateItem10(ColumnDescriptor column, RentalApartamentAgreementOptions options)
        {
            column.Item().PaddingVertical(10).Text("9. Прочие положения договора")
              .FontSize(14).Bold();
            column.Item().Text(text =>
            {
                text.Justify();
                text.Span("10.1 Документ составлен в ");
                text.Span($"   {options.CountExample}   ").Underline();
                text.Span("экземплярах. Все экземпляры имеют равную юридическую силу.");
                text.EmptyLine();
                text.Span("10.2 Договор вступает в силу с момента подписания сторонами");
                text.EmptyLine();
            });
        }
    }
}
