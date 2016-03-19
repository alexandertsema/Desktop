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
    public class MyBinder : Binder
    {
        public MyBinder() : base()
        {
        }

        private class BinderState
        {
            public object[] args;
        }

        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers,
            CultureInfo culture, string[] names, out object state)
        {
            throw new NotImplementedException();
        }

        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override object ChangeType(object value, Type type, CultureInfo culture)
        {
            // Determine whether the value parameter can be converted to a value of type myType.
            if (CanConvertFrom(value.GetType(), type))
                // Return the converted object.
                return Convert.ChangeType(value, type);
            else
                // Return null.
                return null;
        }

        public override void ReorderArgumentArray(ref object[] args, object state)
        {
            throw new NotImplementedException();
        }

        // Determines whether type1 can be converted to type2. Check only for primitive types.
        private bool CanConvertFrom(Type type1, Type type2)
        {
            if (type1.IsPrimitive && type2.IsPrimitive)
            {
                TypeCode typeCode1 = Type.GetTypeCode(type1);
                TypeCode typeCode2 = Type.GetTypeCode(type2);
                // If both type1 and type2 have the same type, return true.
                if (typeCode1 == typeCode2)
                    return true;
                // Possible conversions from Char follow.
                if (typeCode1 == TypeCode.Char)
                    switch (typeCode2)
                    {
                        case TypeCode.UInt16: return true;
                        case TypeCode.UInt32: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Byte follow.
                if (typeCode1 == TypeCode.Byte)
                    switch (typeCode2)
                    {
                        case TypeCode.Char: return true;
                        case TypeCode.UInt16: return true;
                        case TypeCode.Int16: return true;
                        case TypeCode.UInt32: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from SByte follow.
                if (typeCode1 == TypeCode.SByte)
                    switch (typeCode2)
                    {
                        case TypeCode.Int16: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from UInt16 follow.
                if (typeCode1 == TypeCode.UInt16)
                    switch (typeCode2)
                    {
                        case TypeCode.UInt32: return true;
                        case TypeCode.Int32: return true;
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Int16 follow.
                if (typeCode1 == TypeCode.Int16)
                    switch (typeCode2)
                    {
                        case TypeCode.Int32: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from UInt32 follow.
                if (typeCode1 == TypeCode.UInt32)
                    switch (typeCode2)
                    {
                        case TypeCode.UInt64: return true;
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Int32 follow.
                if (typeCode1 == TypeCode.Int32)
                    switch (typeCode2)
                    {
                        case TypeCode.Int64: return true;
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from UInt64 follow.
                if (typeCode1 == TypeCode.UInt64)
                    switch (typeCode2)
                    {
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Int64 follow.
                if (typeCode1 == TypeCode.Int64)
                    switch (typeCode2)
                    {
                        case TypeCode.Single: return true;
                        case TypeCode.Double: return true;
                        default: return false;
                    }
                // Possible conversions from Single follow.
                if (typeCode1 == TypeCode.Single)
                    switch (typeCode2)
                    {
                        case TypeCode.Double: return true;
                        default: return false;
                    }
            }
            return false;
        }
    }

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
