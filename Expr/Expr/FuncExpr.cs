using System;
using System.Collections.Generic;
using System.Reflection;

namespace Entap.Expr
{
	internal class FuncExpr : IExpr
	{
		readonly string _func;
		readonly List<IExpr> _args;

		/// <summary>
		/// <see cref="T:Entap.Expr.FuncExpr"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="func">関数名</param>
		/// <param name="args">引数のノード</param>
		public FuncExpr(string func, List<IExpr> args)
		{
			_func = func;
			_args = args;
		}

		/// <summary>
		/// この式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="binding">変数のバインディング</param>
		public ExprValue Eval(BindingDelegate binding)
		{
			var func = binding(_func) as Delegate;
			var p = func.GetMethodInfo().GetParameters();
			if (_args.Count < p.Length) {
				throw new ArgumentException("Wrong number of arguments: " + _func);
			}
			var args = new object[p.Length];
			for (var i = 0; i < _args.Count; i++) {
				args[i] = _args[i].Eval(binding).As(p[i].ParameterType);
			}
			return new ExprValue(func.DynamicInvoke(args));
		}
	}
}
