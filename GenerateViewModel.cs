using System.Reflection;

namespace ScripGenerateCRUDSForAvalonia
{
    public class GenerateViewModel : GenerateHelper
    {
        public static void create(object o)
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

            method.Add($"----------------------{TypeName}ViewModel---------------------");

            method.Add("public ICommand Add { get; private set; }");
            method.Add("public ICommand Edit { get; private set; }");
            method.Add("public ICommand Remove { get; private set; }");
            method.Add("public ICommand Search { get; private set; }");

            method.Add($"private ObservableCollection<{TypeName}> _{typeName}s = new ObservableCollection<{TypeName}>();");
            method.Add($"public ObservableCollection<{TypeName}> {TypeName}s");
            method.Add("{");
            method.Add($"get => _{typeName}s;");
            method.Add($"set => this.RaiseAndSetIfChanged(ref _{typeName}s, value);");
            method.Add("}");

            method.Add($"private string _searchText;");
            method.Add($"public string SearchText");
            method.Add("{");
            method.Add($"get => _searchText;");
            method.Add($"set => this.RaiseAndSetIfChanged(ref _searchText, value);");
            method.Add("}");

            method.Add($"private {TypeName} _{typeName}Selected;");
            method.Add($"public {TypeName} {TypeName}Selected");
            method.Add("{");
            method.Add($"get => _{typeName}Selected;");
            method.Add($"set => this.RaiseAndSetIfChanged(ref _{typeName}Selected, value);");
            method.Add("}");


            //Fields Private Selected
            foreach (var info in OtherObject)
            {
                var type = info.FieldType.Name;
                var name = getName(info.Name + "");

                if (type.StartsWith("ICollection"))
                {
                    string genericTypeArguments = info.FieldType.GenericTypeArguments[0].Name;

                    //Selected field
                    method.Add($"private {genericTypeArguments} _{genericTypeArguments.ToLower()}Selected;");
                    //Inotify Selected field
                    method.Add($"public {genericTypeArguments} {genericTypeArguments}Selected");
                    method.Add("{");
                    method.Add($"get => _{genericTypeArguments.ToLower()}Selected;");
                    method.Add($"set => this.RaiseAndSetIfChanged(ref _{genericTypeArguments.ToLower()}Selected, value);");
                    method.Add("}");


                    //хранилище для всех "этих" полей
                    method.Add($"private ObservableCollection<{genericTypeArguments}> " +
                        $"_{genericTypeArguments.ToLower()}sAll = new ObservableCollection<{genericTypeArguments}>();");
                    method.Add($"public ObservableCollection<{genericTypeArguments}> {genericTypeArguments}sAll");
                    method.Add("{");
                    method.Add($"get => _{genericTypeArguments.ToLower()}sAll;");
                    method.Add($"set => this.RaiseAndSetIfChanged(ref _{genericTypeArguments.ToLower()}sAll, value);");
                    method.Add("}");



                }
                else
                {
                    method.Add($"private {name} _{name.ToLower()}Selected;");

                    //Inotify field
                    method.Add($"public {name} {name}Selected");
                    method.Add("{");
                    method.Add($"get => _{name.ToLower()}Selected;");
                    method.Add($"set => this.RaiseAndSetIfChanged(ref _{name.ToLower()}Selected, value);");
                    method.Add("}");

                    //для всех "этих" полей
                    method.Add($"private ObservableCollection<{name}> " +
                        $"_{name.ToLower()}sAll = new ObservableCollection<{name}>();");

                    //Inotify field
                    method.Add($"public ObservableCollection<{name}> {name}sAll");
                    method.Add("{");
                    method.Add($"get => _{name.ToLower()}sAll;");
                    method.Add($"set => this.RaiseAndSetIfChanged(ref _{name.ToLower()}sAll, value);");
                    method.Add("}");

                }
            }


            //Constructor Region
            method.Add("ApplicationContext db;");

            method.Add($"public {TypeName}PageViewModel(ApplicationContext applicationContext)");
            method.Add("{");

            method.Add("db = applicationContext;");
            method.Add($"{TypeName}s = new(db.{TypeName}s.ToList());");
            method.Add($"{TypeName}Buffer = new {TypeName}();");
            foreach (var info in OtherObject)
            {
                var type = info.FieldType.Name;

                if (type.StartsWith("ICollection"))
                    type = info.FieldType.GenericTypeArguments[0].Name;

                method.Add($"{type}sAll = new(db.{type}s.ToList());");
            }

            //Add Method
            method.Add("Add = ReactiveCommand.Create(() => \n { \n");

            method.Add("try");
            method.Add("{");

            method.Add($"if (ApplicationContext.validData({TypeName}Buffer))");
            method.Add("{");
            foreach (var item in OtherObject)
            {
                var name = getName(item.Name + "");
                method.Add($"if (ApplicationContext.validData({TypeName}Buffer.{name}))");
                method.Add("{");
            }
            method.Add($"{TypeName}s.Add({TypeName}Buffer);");
            method.Add($"db.{TypeName}s.Add({TypeName}Buffer);");
            method.Add("db.SaveChanges();");
            method.Add($"{TypeName}Selected = new {TypeName}();");
            for (int i = 0; i < OtherObject.Count + 2; i++)
                method.Add("}");
            method.Add("catch (Exception ex) { }");

            method.Add("});");



            //Edit Method 
            method.Add("Edit = ReactiveCommand.Create(() => \n { \n");

            method.Add("try");
            method.Add("{");

            method.Add($"if (ApplicationContext.validData({TypeName}Selected))");
            method.Add("{");
            foreach (var item in OtherObject)
            {
                var name = getName(item.Name + "");
                method.Add($"if (ApplicationContext.validData({TypeName}Selected.{name}))");
                method.Add("{");
            }

            method.Add($"db.{TypeName}s.Update({TypeName}Selected);");
            method.Add("db.SaveChanges();");
            method.Add($"{TypeName}Selected = new {TypeName}();");
            for (int i = 0; i < OtherObject.Count + 2; i++)
                method.Add("}");
            method.Add("catch (Exception ex) { }");

            method.Add("});");



            //Remove Method
            method.Add("Remove = ReactiveCommand.Create(() => \n { \n");

            method.Add($"if ({TypeName}Selected != null)");
            method.Add("{");
            method.Add($"db.{TypeName}s.Remove({TypeName}Selected);");
            method.Add($"{TypeName}s.Remove({TypeName}Selected);");
            method.Add("db.SaveChanges();");
            method.Add($"{TypeName}Selected = new {TypeName}();");
            method.Add("}");
            method.Add("});");


            //Search Method
            method.Add("Search = ReactiveCommand.Create(() => \n { \n");

            method.Add("if (SearchText == null)");
            method.Add("{");
            method.Add($"{TypeName}s = new(db.{TypeName}s");

            foreach (var field in myFieldInfo)
            {
                var type = field.FieldType.Name;
                if (!type.Equals("Int32") && !type.Equals("DateOnly") && !type.Equals("String"))
                    method.Add($".Include(x => x.{getName(field.Name)})");

            }
            method.Add(".ToList());");
            method.Add("return;");
            method.Add("}");

            //если есть строчка поиска
            method.Add($"{TypeName}s = new(db.{TypeName}s");

            foreach (var field in myFieldInfo)
            {
                var type = field.FieldType.Name;
                if (!type.Equals("Int32") && !type.Equals("DateOnly") && !type.Equals("String"))
                    method.Add($".Include(x => x.{getName(field.Name)})");

            }
            method.Add(".Where(x => x.___.Contains(SearchText))");
            method.Add(".ToList());");

            method.Add("});");

            //END
            method.Add("}");

            writeInFile(method, "test.txt", FileMode.Append);
        }
    }
}
