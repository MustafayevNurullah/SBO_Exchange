using AutoMapper;
using SAPbouiCOM;
using SBOAddonProject2.Models;
using SBOAddonProject2.ModelsDto;
using SBOAddonProject2.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBOAddonProject2.ExchangeForm
{
    public static class ExchangeForm
    {

        public static void GetCurs(Matrix matrix, string month, string year)
        {
            List<ValCurs> Curs = new List<ValCurs>();
            Curs = CurrenciesRequest.GetValue(month, year);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Valute, ValuteDto>();});
            IMapper mapper = config.CreateMapper();
            int counter = 1;
            ValuteDto valuteDto = new ValuteDto();
                foreach (var item in Curs)
                {
                    for (int i = 0; i < item.ValType[1].Valute.Count; i++)
                    {
                    valuteDto = mapper.Map<Valute, ValuteDto>(item.ValType[1].Valute[i]);
                    if (item.ValType[1].Valute[i].Code == "DKK")
                        ((SAPbouiCOM.EditText)matrix.Columns.Item("V_0").Cells.Item(counter).Specific).Value = valuteDto.Value;
                    if (item.ValType[1].Valute[i].Code == "EUR")
                        ((SAPbouiCOM.EditText)matrix.Columns.Item("V_1").Cells.Item(counter).Specific).Value = valuteDto.Value;
                        if (item.ValType[1].Valute[i].Code == "GBP")
                        ((SAPbouiCOM.EditText)matrix.Columns.Item("V_2").Cells.Item(counter).Specific).Value = valuteDto.Value;
                    if (item.ValType[1].Valute[i].Code == "SEK")
                        ((SAPbouiCOM.EditText)matrix.Columns.Item("V_3").Cells.Item(counter).Specific).Value = valuteDto.Value;
                    if (item.ValType[1].Valute[i].Code == "USD")
                        ((SAPbouiCOM.EditText)matrix.Columns.Item("V_4").Cells.Item(counter).Specific).Value = valuteDto.Value;

                    }
                    counter++;
                }
            }
        }
    }
