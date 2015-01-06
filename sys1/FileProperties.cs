using System;
using System.Collections.Generic;
using System.Linq;

namespace sys1
{
    public class FileProperties
    {
        private Dictionary<String, String> _list;
        private String _filename;

        public FileProperties(String file)
        {
            Reload(file);
        }

        public String Get(String field, String defValue)
        {
            return (Get(field) == null) ? (defValue) : (Get(field));
        }
        public String Get(String field)
        {
            return (_list.ContainsKey(field)) ? (_list[field]) : (null);
        }

        public void Set(String field, Object value)
        {
            if (!_list.ContainsKey(field))
                _list.Add(field, value.ToString());
            else
                _list[field] = value.ToString();
        }

        public void Save()
        {
            Save(_filename);
        }

        public void Save(String filename)
        {
            _filename = filename;

            if (!System.IO.File.Exists(filename))
                System.IO.File.Create(filename);

            var file = new System.IO.StreamWriter(filename);

            foreach (var prop in _list.Keys.ToArray().Where(prop => !String.IsNullOrWhiteSpace(_list[prop])))
                file.WriteLine(prop + "=" + _list[prop]);

            file.Close();
        }

        public void Reload()
        {
            Reload(_filename);
        }

        public void Reload(String filename)
        {
            _filename = filename;
            _list = new Dictionary<String, String>();

            if (System.IO.File.Exists(filename))
                LoadFromFile(filename);
            else
                System.IO.File.Create(filename);
        }

        private void LoadFromFile(String file)
        {
            foreach (String line in System.IO.File.ReadAllLines(file))
            {
                if ((!String.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    String key = line.Substring(0, index).Trim();
                    String value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    try
                    {
                        //ignore dublicates
                        _list.Add(key, value);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
