using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using InstaSync.Models.Models;

namespace InstaSync.UOW.Helpers
{
    public class XmlHelper
    {
        #region public methods

        public static void SaveXml<T>(T model)
        {
            CreateXml(model).Save($"{model.GetType().Name}.xml");
        }

        public static object GetModel<T>(string path, T model)
        {
            var xml = GetXml(path);
            if (xml == null) return null;
            var properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                var singleOrDefault = xml.Elements(property.Name).SingleOrDefault();
                if (singleOrDefault == null) continue;
                var prop = singleOrDefault.Value;
                //var h = property.PropertyType;
                model.GetType().InvokeMember(property.Name, BindingFlags.SetProperty, null, model, new object[] { prop });
            }
            return model;
        }

        #endregion public

        #region private methods

        private static XElement GetXml(string path)
        {
            if (!File.Exists(path))
                return null;
            
            var xmlStr = File.ReadAllText(path);
            var xml = XElement.Parse(xmlStr);
            
            return xml;
        }

        private static XElement CreateXml<T>(T model)
        {
            var type = model.GetType();
            var properties = type.GetProperties();
            List<XElement> elements = (from property in properties let value = GetPropValue(model, property.Name) select new XElement(property.Name, value)).ToList();
            return new XElement(type.Name, elements);
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        #endregion private methods
    }
}
