// <copyright file="ReplayManagerNotInitializedException.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System;
using System.Runtime.Serialization;

namespace ReplayManager.Models
{
	public class ReplayManagerNotInitializedException : Exception
	{
		public ReplayManagerNotInitializedException()
		{
		}

		public ReplayManagerNotInitializedException(string message)
			: base(message)
		{
		}

		public ReplayManagerNotInitializedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ReplayManagerNotInitializedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
