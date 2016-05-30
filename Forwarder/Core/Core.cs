using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Forwarder.Entity;
using Excel=Microsoft.Office.Interop.Excel;

namespace Forwarder
{
    class Core
    {
        public static string generatePasswordHash(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static void DisplayDrivers(IEnumerable<Driver> drivers)
        {
            var excelApp = new Excel.Application();
            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collection returned 
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template. 
            // Because no argument is sent in this example, Add creates a new workbook. 
            excelApp.Workbooks.Add();

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheet.Name = "Водители";
            workSheet.Cells[1, "A"] = "№";
            workSheet.Cells[1, "B"] = "ФИО";
            workSheet.Cells[1, "C"] = "Телефон";

            var row = 1;
            foreach (var driver in drivers)
            {
                row++;
                workSheet.Cells[row, "A"] = driver.Id;
                workSheet.Cells[row, "B"] = driver.Name;
                workSheet.Cells[row, "C"] = driver.Phone;
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
        }

        public static void DisplayCars(IEnumerable<Car> cars)
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheet.Name = "Машины";
            workSheet.Cells[1, "A"] = "№";
            workSheet.Cells[1, "B"] = "Номер машины";
            workSheet.Cells[1, "C"] = "Фирма";
            workSheet.Cells[1, "D"] = "Водитель";
            workSheet.Cells[1, "E"] = "Цена за 1 км";

            var row = 1;
            foreach (var car in cars)
            {
                row++;
                workSheet.Cells[row, "A"] = car.Id;
                workSheet.Cells[row, "B"] = car.Number;
                workSheet.Cells[row, "C"] = car.Firm.Name;
                workSheet.Cells[row, "D"] = car.Drivers.Name;
                workSheet.Cells[row, "E"] = car.Price;
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
            workSheet.Columns[4].AutoFit();
            workSheet.Columns[5].AutoFit();
        }
        public static void DisplayPribil(IEnumerable<Order> orders, decimal kom)
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheet.Name = "Прибыль";
            workSheet.Cells[1, "A"] = "№";
            workSheet.Cells[1, "B"] = "Выручка";
            workSheet.Cells[1, "C"] = "Приыбль";
            kom = kom / 100;
            var row = 2;
            Decimal itogPribil = 0;
            Decimal itogPrice = 0;
            foreach (var order in orders)
            {                
                workSheet.Cells[row, "A"] = order.Id;
                workSheet.Cells[row, "B"] = order.Price;
                itogPribil += order.Price * kom;
                itogPrice += order.Price;
                workSheet.Cells[row, "C"] = order.Price*kom;
                row++;
            }
            workSheet.Cells[row, "B"] = "Итог: " + itogPrice.ToString();
            workSheet.Cells[row, "C"] = "Итог: " + itogPribil.ToString();
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();        
        }
    }
}
