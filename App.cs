#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

#endregion

namespace Tunnel_Section_Optimization
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            RibbonPanel panel = RibbonPanel(a);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if (panel.AddItem(new PushButtonData("FirstPlugin", "단면최적화 실행", thisAssemblyPath, "Tunnel_Section_Optimization.Command"))
                is PushButton button)
            {
                button.ToolTip = "Button Tooltip";

                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "tunnel.ico"));
                BitmapImage bitmap = new BitmapImage(uri);
                button.LargeImage = bitmap;
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        public RibbonPanel RibbonPanel(UIControlledApplication a) //Return 타입 RibbornPanel, 메소드명 RibbonPanel?? 
        {
            String tabName = "Tunnel_Automizaion"; //리본 탭 이름 변수 지정
            String panelName = "단면최적화"; //리본 패널 이름 변수 지정 

            RibbonPanel ribbonPanel = null; //인스턴스 생성 후 null 값 지정???

            // 0. 리본 탭 생성 
            // try -catch로 예외처리 (오류가 생기면 해결해주는 기능)
            try
            {
                a.CreateRibbonTab(tabName); // 리본탭을 생성 해보고
            }
            catch (Exception ex) //예외가 발생하면 예외를 받아서 처리
            {
                Debug.WriteLine(ex.Message);
            }

            // 1. 리본 패널 생성 
            try
            {
                a.CreateRibbonPanel(tabName, panelName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // 
            List<RibbonPanel> panels = a.GetRibbonPanels(tabName); //tab에 있는 모든 패널을 가져와서 리스트화
            foreach (RibbonPanel p in panels.Where(p => p.Name == panelName)) //패널 리스트의 요소의 이름이 panelName과 일치하면 순환?
            {
                ribbonPanel = p;
            }

            return ribbonPanel;
        }
    }
}
