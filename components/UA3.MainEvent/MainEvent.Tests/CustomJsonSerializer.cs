//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using NEventStore.Serialization;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace MainEvent.Tests
{
    public class CustomJsonSerializer : ISerialize
    {
        private readonly Newtonsoft.Json.JsonSerializer _untypedSerializer = new Newtonsoft.Json.JsonSerializer
        {
            TypeNameHandling = TypeNameHandling.Auto,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        public CustomJsonSerializer()
        {
            this._untypedSerializer.SerializationBinder = new CustomSerializationBinder();
        }

        public virtual void Serialize<T>(Stream output, T graph)
        {
            using (var streamWriter = new StreamWriter(output, Encoding.UTF8))
            {
                Serialize(new JsonTextWriter(streamWriter), graph);
            }
        }

        public virtual T Deserialize<T>(Stream input)
        {
            using (var streamReader = new StreamReader(input, Encoding.UTF8))
            {
                return Deserialize<T>(new JsonTextReader(streamReader));
            }
        }

        protected virtual void Serialize(JsonWriter writer, object graph)
        {
            using (writer)
            {
                GetSerializer(graph.GetType()).Serialize(writer, graph);
            }
        }

        protected virtual T Deserialize<T>(JsonReader reader)
        {
            Type type = typeof(T);

            using (reader)
            {
                return (T)GetSerializer(type).Deserialize(reader, type);
            }
        }

        protected virtual Newtonsoft.Json.JsonSerializer GetSerializer(Type typeToSerialize)
        {
            return _untypedSerializer;
        }
    }
}
