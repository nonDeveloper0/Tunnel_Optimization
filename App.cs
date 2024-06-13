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

            if (panel.AddItem(new PushButtonData("FirstPlugin", "�ܸ�����ȭ ����", thisAssemblyPath, "Tunnel_Section_Optimization.Command"))
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

        public RibbonPanel RibbonPanel(UIControlledApplication a) //Return Ÿ�� RibbornPanel, �޼ҵ�� RibbonPanel?? 
        {
            String tabName = "Tunnel_Automizaion"; //���� �� �̸� ���� ����
            String panelName = "�ܸ�����ȭ"; //���� �г� �̸� ���� ���� 

            RibbonPanel ribbonPanel = null; //�ν��Ͻ� ���� �� null �� ����???

            // 0. ���� �� ���� 
            // try -catch�� ����ó�� (������ ����� �ذ����ִ� ���)
            try
            {
                a.CreateRibbonTab(tabName); // �������� ���� �غ���
            }
            catch (Exception ex) //���ܰ� �߻��ϸ� ���ܸ� �޾Ƽ� ó��
            {
                Debug.WriteLine(ex.Message);
            }

            // 1. ���� �г� ���� 
            try
            {
                a.CreateRibbonPanel(tabName, panelName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // 
            List<RibbonPanel> panels = a.GetRibbonPanels(tabName); //tab�� �ִ� ��� �г��� �����ͼ� ����Ʈȭ
            foreach (RibbonPanel p in panels.Where(p => p.Name == panelName)) //�г� ����Ʈ�� ����� �̸��� panelName�� ��ġ�ϸ� ��ȯ?
            {
                ribbonPanel = p;
            }

            return ribbonPanel;
        }
    }
}
