namespace Entap.Expr
{
	/// <summary>
	/// 式のノード
	/// </summary>
	internal interface IExpr
	{
		/// <summary>
		/// 数式ノードを評価する。
		/// </summary>
		/// <returns>評価結果</returns>
		ExprValue Eval(BindingDelegate binding);
	}
}
