namespace Entap.Expr
{
	/// <summary>
	/// 定数値を持つ式
	/// </summary>
	internal class ConstantExpr : IExpr
	{
		readonly ExprValue _value;

		/// <summary>
		/// <see cref="T:Entap.Expr.ConstantExpr"/> クラスのインスタンスを初期化する。
		/// </summary>
		/// <param name="value">定数値</param>
		public ConstantExpr(ExprValue value)
		{
			_value = value;
		}

		/// <summary>
		/// この式を評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		/// <param name="binding">変数のバインディング</param>
		public ExprValue Eval(BindingDelegate binding)
		{
			return _value;
		}
	}
}
