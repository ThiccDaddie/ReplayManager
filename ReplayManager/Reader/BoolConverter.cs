// <copyright file="BoolConverter.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.Buffers;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ReplayManager.Models;
using ReplayManager.Models.Post;
using ReplayManager.Models.Pre;

namespace ReplayManager.Reader
{
	public class BoolConverter : JsonConverter<bool>
	{
		public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Number)
			{
				ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
				if (Utf8Parser.TryParse(span, out int value, out int _))
				{
					return Convert.ToBoolean(value);
				}
			}

			return reader.GetBoolean();
		}

		public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
