using System;
using System.Text.Json.Serialization;

namespace SourceGenerators
{
	//[JsonSourceGenerationOptions(WriteIndented = true)]
	[JsonSerializable(typeof(Person))]
	internal partial class PreBattleInfoContext : JsonSerializerContext
	{
	}
}
