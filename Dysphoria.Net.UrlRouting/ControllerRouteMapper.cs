﻿// -----------------------------------------------------------------------
// <copyright file="ControllerRouteMapper.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting.Handlers;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ControllerRouteMapper<C>
		where C : Controller
	{
		private readonly RouteCollection routes;
		private readonly string controllerName;

		public ControllerRouteMapper(RouteCollection routes)
		{
			this.routes = routes;
			this.controllerName = GetControllerName(typeof(C));
		}

		public RouteCollection Routes { get { return this.routes; } }

		// 0-argument URL patterns

		public ControllerRouteMapper<C> MapRoute(RequestPattern<UrlPattern> pattern, Expression<Func<C, Func<ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) => methodFunc.Invoke(c).Invoke());
		}

		public ControllerRouteMapper<C> MapRoute<BodyType>(RequestPattern<UrlPattern, BodyType> pattern, Expression<Func<C, Func<BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var body = pattern.DecodeBody(context);
				return methodFunc.Invoke(c).Invoke(body);
			});
		}

		// 1-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1>(RequestPattern<UrlPattern<P1>> pattern, Expression<Func<C, Func<P1, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				return methodFunc.Invoke(c).Invoke(p.Item1);
			});
		}

		public ControllerRouteMapper<C> MapRoute<P1, BodyType>(RequestPattern<UrlPattern<P1>, BodyType> pattern, Expression<Func<C, Func<P1, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				var body = pattern.DecodeBody(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, body);
			});
		}

		// 2-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1, P2>(RequestPattern<UrlPattern<P1, P2>> pattern, Expression<Func<C, Func<P1, P2, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2);
			});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, BodyType>(RequestPattern<UrlPattern<P1, P2>, BodyType> pattern, Expression<Func<C, Func<P1, P2, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				var body = pattern.DecodeBody(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, body);
			});
		}

		// 3-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3>(RequestPattern<UrlPattern<P1, P2, P3>> pattern, Expression<Func<C, Func<P1, P2, P3, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3);
			});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, BodyType>(RequestPattern<UrlPattern<P1, P2, P3>, BodyType> pattern, Expression<Func<C, Func<P1, P2, P3, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				var body = pattern.DecodeBody(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, body);
			});
		}

		// 4-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, P4>(RequestPattern<UrlPattern<P1, P2, P3, P4>> pattern, Expression<Func<C, Func<P1, P2, P3, P4, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, p.Item4);
			});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, P4, BodyType>(RequestPattern<UrlPattern<P1, P2, P3, P4>, BodyType> pattern, Expression<Func<C, Func<P1, P2, P3, P4, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) =>
			{
				var p = pattern.Url.ExtractParameters(context);
				var body = pattern.DecodeBody(context);
				return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, p.Item4, body);
			});
		}


		private ControllerRouteMapper<C> AddRouteHandler<FunctionType>(AbstractRequestPattern pattern, Expression<FunctionType> handler, Func<C, ControllerContext, ActionResult> handlerFunction)
		{
			var method = GetMethodInfo(handler);
			var actionName = method.Name;
			this.routes.AddRoute(
				pattern,
				new ControllerRouteHandler<C>(pattern, this.controllerName, actionName, handlerFunction));

			return this; // To allow for method chaining.
		}

		internal static string GetControllerName(Type controller)
		{
			const string suffix = "Controller";
			var wholeName = controller.Name;
			var endsWithSuffix = wholeName.Length > suffix.Length && wholeName.EndsWith(suffix);
			return endsWithSuffix
					? wholeName.Substring(0, wholeName.Length - suffix.Length)
					: wholeName;
		}

		private static MethodInfo GetMethodInfo(Expression method)
		{
			var lambda = method as LambdaExpression;
			if (lambda == null) throw new ArgumentException("Argument is not a lambda expression (c => c.Thing)");

		    var body = ((UnaryExpression) lambda.Body).Operand as MethodCallExpression;
			if (body == null) throw new ArgumentException("Argument not in correct form (c => c.Thing)");

		    MethodInfo methodInfoValue = null;
            if (body.Object is ConstantExpression)
                methodInfoValue = (body.Object as ConstantExpression).Value as MethodInfo;

			if (methodInfoValue == null) throw new ArgumentException("Cannot find method name in expression: {0}", method.ToString());
			return methodInfoValue;
		}
	}
}
