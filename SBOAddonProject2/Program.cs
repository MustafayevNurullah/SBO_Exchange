using System;
using System.Collections.Generic;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using System.Threading;
using SBOAddonProject2.Models;
using SBOAddonProject2.Request;
using System.Timers;
using System.Diagnostics;
using System.Windows.Forms;

namespace SBOAddonProject2
{
    class Program
    {
      public static List< ValCurs> Curs;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                SAPbouiCOM.Framework.Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new SAPbouiCOM.Framework.Application();
                }
                else
                {
                    oApp = new SAPbouiCOM.Framework.Application(args[0]);
                }
                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                SAPbouiCOM.Framework.Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                SAPbouiCOM.Framework.Application.SBO_Application.ItemEvent += SBO_Application_ItemEvent;
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public static SAPbouiCOM.Form oForm;
        public static SAPbouiCOM.Item oItem;
        public static SAPbouiCOM.Item oOldItem;
        public static string Month = DateTime.Now.Month.ToString();
        public static string Year = DateTime.Now.Year.ToString();

        private static void SBO_Application_ItemEvent(string FormUID, ref ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
                if (pVal.FormType==866 && pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD )
                {
                    oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
                    oOldItem = oForm.Items.Item("4");
                    Matrix matrix = (SAPbouiCOM.Matrix)oOldItem.Specific;
                ExchangeForm.ExchangeForm.GetCurs(matrix,Month,Year);
                }
            if (pVal.FormType == 866 && pVal.FormMode==1  && pVal.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
            {
                oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);

                if (pVal.ItemUID.ToString() == "12")
                {
                    oOldItem = oForm.Items.Item("4");
                    Matrix matrix = (SAPbouiCOM.Matrix)oOldItem.Specific;
                    oOldItem = oForm.Items.Item("12");
                    SAPbouiCOM.ComboBox cb = (SAPbouiCOM.ComboBox)oOldItem.Specific;
                    Year = cb.Value;
                    ExchangeForm.ExchangeForm.GetCurs(matrix, Month, Year);
                }
                if (pVal.ItemUID.ToString() == "13")
                {
                    oOldItem = oForm.Items.Item("4");
                    Matrix matrix = (SAPbouiCOM.Matrix)oOldItem.Specific;
                    oOldItem = oForm.Items.Item("13");
                    SAPbouiCOM.ComboBox cb = (SAPbouiCOM.ComboBox)oOldItem.Specific;
                    Month = cb.Selected.Value;
                  
                    ExchangeForm.ExchangeForm.GetCurs(matrix, Month, Year);
                }
            }
        }

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
    }
}
