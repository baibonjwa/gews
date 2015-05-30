using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sys1
{
    public class FileProperties
    {
        private String _filename;
        private Dictionary<String, String> _list;

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

            if (!File.Exists(filename))
                File.Create(filename);

            var file = new StreamWriter(filename);

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

            if (File.Exists(filename))
                LoadFromFile(filename);
            else
                File.Create(filename);
        }

        private void LoadFromFile(String file)
        {
            foreach (var line in File.ReadAllLines(file))
            {
                if ((!String.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    var index = line.IndexOf('=');
                    var key = line.Substring(0, index).Trim();
                    var value = line.Substring(index + 1).Trim();

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