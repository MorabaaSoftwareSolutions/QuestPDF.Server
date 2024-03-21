using System.Text.Json.Serialization;
using QuestPDF.Server.Core;

[JsonSerializable(typeof(CreatePDFRequest))]
internal partial class QuestPDFServerJsonSerializerContext : JsonSerializerContext
{
}
