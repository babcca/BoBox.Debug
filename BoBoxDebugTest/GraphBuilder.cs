﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BoBox.Interfaces;
using BoBox.Entities;

using System.IO;

namespace BoBoxDebugTest
{
    class GraphBuilder
    {
        public static IGraph FromFile(string path)
        {
            return FromStream(new FileStream(path, FileMode.Open));
        }

        public static IGraph FromStream(Stream stream)
        {



            return null;
        }

        public static void Serialize<T>(T g, string path)
        {
            var file = new StreamWriter(path);
            var a = Newtonsoft.Json.JsonConvert.SerializeObject(g, Newtonsoft.Json.Formatting.Indented);
            file.Write(a);
            file.Close();                                                
        }
        
        public static T Deserialize<T>(string path)
        {
            var file = new StreamReader(path);
            var a = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(file.ReadToEnd(), new GraphDeserializationConverter());            
            file.Close();
            return a;
        }
    }

    class GraphDeserializationConverter : JsonCreationConverter<INode>
    {
        protected override INode Create(Type objectType, Newtonsoft.Json.Linq.JObject jObject)
        {
            Newtonsoft.Json.Linq.JToken type = null;
            if (jObject.TryGetValue("type", StringComparison.CurrentCultureIgnoreCase, out type))
            {
                return NodeFactory.JsonNodeFactory(type.ToString());
            }
            else
            {
                throw new Exception("Type not set");
            }
            
        }
    }

    abstract class JsonCreationConverter<T> : Newtonsoft.Json.JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">contents of JSON object that will be deserialized</param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, Newtonsoft.Json.Linq.JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            // Load JObject from stream
           Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
