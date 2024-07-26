using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BusAllocatorApp
{
    public class TupleKeyConverter : JsonConverter<(string, string)>
    {
        public override (string, string) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var tupleString = reader.GetString();
            var parts = tupleString.Split('-');
            return (parts[0], parts[1]);
        }

        public override void Write(Utf8JsonWriter writer, (string, string) value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.Item1}-{value.Item2}");
        }
    }
    internal class IO
    {
        private Vars vars;
        private MainForm mainform;

        public IO(Vars v, MainForm f)
        {
            vars = v;
            mainform = f;
        }

        public void GenerateJSONFiles()
        {
            ConvertBusCapacitiesToJson();
            ConvertBuffersToJson();
            ConvertRatesToJson();
            ConvertDeptsToJson();
            ConvertTimeSetsToJson();
            ConvertRoutesToJson();
        }

        void ConvertRoutesToJson()
        {
            var routesData = new
            {
                solo_routes = vars.solo_routes,
                hybrid_routes = vars.hybrid_routes
            };

            string json = JsonSerializer.Serialize(routesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("data/routes.json", json);

            mainform.WriteLine("Converted Routes to JSON.");
        }

        // Convert List of Dictionaries of Time Sets to JSON
        void ConvertTimeSetsToJson()
        {
            string json = JsonSerializer.Serialize(vars.timeSets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("data/time_sets.json", json);

            mainform.WriteLine("Converted Time Sets to JSON.");
        }

        // Convert List of Departments to JSON
        void ConvertDeptsToJson()
        {
            string json = JsonSerializer.Serialize(vars.departments, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("data/depts.json", json);

            mainform.WriteLine("Converted Departments to JSON.");
        }

        // Convert Dictionary of Rates to JSON
        void ConvertRatesToJson()
        {
            var ratesData = new RatesData
            {
                costSmallBus = vars.costSmallBus,
                costLargeBus = vars.costLargeBus,
                costSmallHybridRoute = vars.costSmallHybridRoute,
                costLargeHybridRoute = vars.costLargeHybridRoute
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new TupleKeyConverter() }
            };

            string json = JsonSerializer.Serialize(ratesData, options);
            File.WriteAllText("data/rates.json", json);

            mainform.WriteLine("Converted Bus Rates to JSON.");
        }

        private class RatesData
        {
            public required Dictionary<string, double> costSmallBus { get; set; }
            public required Dictionary<string, double> costLargeBus { get; set; }

            [JsonConverter(typeof(DictionaryTupleDoubleConverter))]
            public required Dictionary<(string, string), double> costSmallHybridRoute { get; set; }

            [JsonConverter(typeof(DictionaryTupleDoubleConverter))]
            public required Dictionary<(string, string), double> costLargeHybridRoute { get; set; }
        }

        private class DictionaryTupleDoubleConverter : JsonConverter<Dictionary<(string, string), double>>
        {
            public override Dictionary<(string, string), double> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<(string, string), double> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                foreach (var kvp in value)
                {
                    writer.WritePropertyName($"{kvp.Key.Item1}-{kvp.Key.Item2}");
                    writer.WriteNumberValue(kvp.Value);
                }
                writer.WriteEndObject();
            }
        }

        // Convert Dictionary of Buffer Capacities to JSON
        void ConvertBuffersToJson()
        {
            var buffersData = new BuffersData
            {
                bufferCurrentSmall = vars.bufferCurrentSmall,
                bufferCurrentLarge = vars.bufferCurrentLarge
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new TupleKeyConverter() }
            };

            string json = JsonSerializer.Serialize(buffersData, options);
            File.WriteAllText("data/buffers.json", json);

            mainform.WriteLine("Converted Buffer Capacities to JSON.");
        }

        private class BuffersData
        {
            [JsonConverter(typeof(DictionaryObjectIntConverter))]
            public required Dictionary<object, int> bufferCurrentSmall { get; set; }

            [JsonConverter(typeof(DictionaryObjectIntConverter))]
            public required Dictionary<object, int> bufferCurrentLarge { get; set; }
        }

        private class DictionaryObjectIntConverter : JsonConverter<Dictionary<object, int>>
        {
            public override Dictionary<object, int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<object, int> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                foreach (var kvp in value)
                {
                    if (kvp.Key is string)
                    {
                        writer.WritePropertyName((string)kvp.Key);
                        writer.WriteNumberValue(kvp.Value);
                    }
                    else if (kvp.Key is ValueTuple<string, string> tuple)
                    {
                        writer.WritePropertyName($"{tuple.Item1}-{tuple.Item2}");
                        writer.WriteNumberValue(kvp.Value);
                    }
                }
                writer.WriteEndObject();
            }
        }

        // Convert Bus Max Capacities to JSON
        void ConvertBusCapacitiesToJson()
        {
            var capacitiesData = new
            {
                capacitySmallBus = vars.capacitySmallBus,
                capacityLargeBus = vars.capacityLargeBus
            };

            string json = JsonSerializer.Serialize(capacitiesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("data/bus_capacities.json", json);

            mainform.WriteLine("Converted Bus Capacities to JSON.");
        }
    }
}
