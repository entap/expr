using System;
namespace Entap.Expr
{
	/// <summary>
	/// 変数を持つ式
	/// </summary>
	internal class VarExpr : IExpr
	{
		readonly string _name;

		/// <summary>
		/// <see cref="T:Entap.Expr.VarExpr"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="name">変数名</param>
		public VarExpr(string name)
		{
			_name = name;
		}

		/// <summary>
		/// このノードを評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="binding">変数のバインディング</param>
		public ExprValue Eval(BindingDelegate binding)
		{
			return new ExprValue(binding(_name));
		}
	}
}
