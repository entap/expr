using System;
using System.Collections.Generic;

namespace Entap.Expr
{
	/// <summary>
	/// 変数のバインディング
	/// </summary>
	public delegate object BindingDelegate(string name);

	/// <summary>
	/// 数式
	/// </summary>
	public class Expr
	{
		IExpr _expr;

		/// <summary>
		/// <see cref="T:Entap.Expr.Expr"/> クラスのインスタンスを初期化する。
		/// </summary>
		public Expr()
		{
			_expr = null;
		}

		/// <summary>
		/// <see cref="T:Entap.Expr.Expr"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="expr">数式.</param>
		public Expr(string expr)
		{
			Compile(expr);
		}

		/// <summary>
		/// 数式を設定する。
		/// </summary>
		/// <param name="expr">数式</param>
		public void Compile(string expr)
		{
			var lexer = Lexer.ReadAll(expr);
			_expr = Parser.Parse(lexer);
		}

		/// <summary>
		/// 数式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="expr">数式</param>
		/// <param name="vars">変数の辞書</param>
		public static ExprValue Eval(string expr, Dictionary<string, object> vars = null)
		{
			return (new Expr(expr)).Eval(vars);
		}

		/// <summary>
		/// 数式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="vars">変数の辞書</param>
		public ExprValue Eval(Dictionary<string, object> vars = null)
		{
			return Eval(name => vars != null ? vars[name] : ExprValue.Null);
		}

		/// <summary>
		/// 数式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="binding">変数のバインディング</param>
		public ExprValue Eval(BindingDelegate binding)
		{
			binding = Math.Binding(binding);
			return _expr.Eval(binding);
		}
	}
}
