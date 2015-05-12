using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvLibrary
{
    public class CsvParser
    {
        public CsvParser()
        {
        }

        public List<T> ReadFromCsv<T>(string FileName) where T : new()
        {
            List<T> Items = new List<T>();
            List<string> columns = new List<string>();
            using (var reader = new CsvFileReader(FileName))
            {
                while (reader.ReadRow(columns))
                {
                    T Item = new T();
                    for (int i = 0; i < columns.ToList().Count(); i++)
                    {
                        Type t = Item.GetType();
                        var PropertyInfo = t.GetProperties()[i];
                        dynamic DynamicValue = columns.ToList()[i];
                        try
                        {
                            DynamicValue = Convert.ChangeType(DynamicValue, PropertyInfo.PropertyType);
                        }
                        catch
                        {
                            int column = i + 1;
                            throw new Exception("No se pudo cargar los datos del fichero " + FileName + ". El campo con valor '" + DynamicValue + "' de la columna " + column + " no pudo ser convertido al tipo " + PropertyInfo.PropertyType);
                        }
                        PropertyInfo.SetValue(Item, DynamicValue, null);
                    }
                    Items.Add(Item);
                }
            }
            return Items;
        }

        public void WriteToCsv<T>(List<T> Items, string FileName)
        {
            /*
            using (var writer = new CsvFileWriter(FileName))
            {
                foreach (T item in Items)
                {
                    List<string> columns = new List<string>();
                    for (int col = 0; col < numColumns; col++)
                    {
                        columns.Add((string)row.Cells[col].Value ?? String.Empty);
                    }
                    writer.WriteRow(columns);
                }
            }
            */ 
        }
    }
}
