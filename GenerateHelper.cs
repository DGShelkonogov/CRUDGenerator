using System.Reflection;
using System.Text;

namespace ScripGenerateCRUDSForAvalonia
{
    public class GenerateHelper
    {
        public static string getName(string typeName)
        {
            string name = "";

            for (int i = 0; i < typeName.Length; i++)
            {
                if (typeName[i] == '>')
                    return name;

                if (typeName[i] != '<')
                    name += typeName[i];

            }
            return name;
        }

        public static void writeInFile(List<string> list, string path, FileMode mode)
        {
            string str = "";
            foreach (string item in list)
                str += item + "\n";


            using (FileStream fstream = new FileStream(path, mode))
            {
                byte[] array = Encoding.Default.GetBytes(str);
                fstream.Write(array, 0, array.Length);
            }
        }

        public static string writeRow(FieldInfo info)
        {

            string str = "";
            var type = info.FieldType.Name;
            var name = getName(info.Name);

            if (type.Equals("Int32"))
            {
                str = name + " =  Convert.ToInt32(txt" + name + ".Text),";
            }
            else if (type.Equals("String"))
            {
                str = name + " = txt" + name + ".Text,";
            }
            else if (type.Equals("DateOnly"))
            {
                str = name + " =  DateOnly.Parse(dt" + name + ".Text),";
            }
            else
            {
                str = name + " = db." + name + "s" + ".FirstOrDefault(x => x.Id == " + name.ToLower() + ".Id),";
            }

            return str;
        }
    }
}
