﻿// -----------------------------------------------------------------------
// <copyright file="InvalidUrlComponentValueException.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting
{
	using System;

	public class InvalidUrlArgumentException : ArgumentException
	{
		public InvalidUrlArgumentException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
