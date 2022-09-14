using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScripGenerateCRUDSForAvalonia
{
    public class GenerateView
    {
        public static void create(Object o)
        {

            var OtherObject = new List<FieldInfo>();
            var method = new List<string>();

            var myType = o.GetType();
            string TypeName = myType.Name;
            string typeName = myType.Name.ToLower();
            var myFieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var field in myFieldInfo)
            {
                var type = field.FieldType.Name;
                if (!type.Equals("Int32") && !type.Equals("DateOnly") && !type.Equals("DateTime") && !type.Equals("TimeOnly") && !type.Equals("String"))
                    OtherObject.Add(field);
            }

            method.Add($"----------------------{TypeName}View---------------------");

            method.Add("<Grid> <Grid> <Grid.RowDefinitions> <RowDefinition Height=\"30\"/>" +
                " <RowDefinition/> </Grid.RowDefinitions>");
            method.Add("<Grid> <Grid.ColumnDefinitions> <ColumnDefinition/> " +
                "<ColumnDefinitionWidth = \"60\"/> </Grid.ColumnDefinitions> " +
                "<TextBox Text = \"{Binding SearchText}\"/> <Button Grid.Column = \"1\" " +
                "Content = \"Поиск\" Command = \"{Binding Search}\"/> </Grid>");
            method.Add("<Grid Grid.Row=\"1\"> " +
                "<Grid.ColumnDefinitions> <ColumnDefinition Width = \"200\"/>" +
                " <ColumnDefinition/> </Grid.ColumnDefinitions> " +
                "<Grid Grid.Column = \"0\"> <Grid.RowDefinitions>");
            
            foreach(var field in myFieldInfo)
            {
                method.Add("<RowDefinition Height=\"30\"/>");
            }
            method.Add("<RowDefinition/>");
            method.Add("<RowDefinition Height=\"30\"/>");
            method.Add("</Grid.RowDefinitions>");

            foreach (var field in myFieldInfo)
            {
                var type = field.FieldType.Name;
                switch (type)
                {
                   
                }
            }


        }
    }
}
