using System;
using Entap.Expr;
using NUnit.Framework;

namespace Entap.Expr.Test
{
	public class ExprTest
	{
		[Test]
		public void OnlyIdentifierTest1()
		{
			var lexer = new Lexer("abc123");
			var token = lexer.Read();
			Assert.AreEqual("abc123", token.Value);
			Assert.AreEqual(TokenType.Identifier, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyIdentifierTest2()
		{
			var lexer = new Lexer("あいうえお");
			var token = lexer.Read();
			Assert.AreEqual("あいうえお", token.Value);
			Assert.AreEqual(TokenType.Identifier, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyNumericTest1()
		{
			var lexer = new Lexer("0x1234");
			var token = lexer.Read();
			Assert.AreEqual((double)0x1234, token.Value);
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyNumericTest2()
		{
			var lexer = new Lexer("01234");
			var token = lexer.Read();
			Assert.AreEqual(01234.0, token.Value);
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyNumericTest3()
		{
			var lexer = new Lexer("1.234");
			var token = lexer.Read();
			Assert.AreEqual(1.234, token.Value);
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyNumericTest4()
		{
			var lexer = new Lexer("1.234e-10");
			var token = lexer.Read();
			Assert.AreEqual(1.234e-10, token.Value);
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyNumericTest5()
		{
			var lexer = new Lexer("0");
			var token = lexer.Read();
			Assert.AreEqual(0.0, token.Value);
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyNumericTest6()
		{
			var lexer = new Lexer("1.234e10");
			var token = lexer.Read();
			Assert.AreEqual(1.234e10, token.Value);
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyStringTest1()
		{
			var lexer = new Lexer("'abcdefg'");
			var token = lexer.Read();
			Assert.AreEqual("abcdefg", token.Value);
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyStringTest2()
		{
			var lexer = new Lexer("'abc\\ndefg\\x12\\u1234'");
			var token = lexer.Read();
			Assert.AreEqual("abc\ndefg\x12\u1234", token.Value);
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyPuncTest1()
		{
			var lexer = new Lexer("+");
			var token = lexer.Read();
			Assert.AreEqual("+", token.Value);
			Assert.AreEqual(TokenType.Punctuator, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyPuncTest2()
		{
			var lexer = new Lexer("&&");
			var token = lexer.Read();
			Assert.AreEqual("&&", token.Value);
			Assert.AreEqual(TokenType.Punctuator, token.Type);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyExpTest1()
		{
			var lexer = new Lexer("123+456*(abc-0.1)+789");
			Assert.AreEqual(123.0, lexer.Read().Value);
			Assert.AreEqual("+", lexer.Read().Value);
			Assert.AreEqual(456.0, lexer.Read().Value);
			Assert.AreEqual("*", lexer.Read().Value);
			Assert.AreEqual("(", lexer.Read().Value);
			Assert.AreEqual("abc", lexer.Read().Value);
			Assert.AreEqual("-", lexer.Read().Value);
			Assert.AreEqual(0.1, lexer.Read().Value);
			Assert.AreEqual(")", lexer.Read().Value);
			Assert.AreEqual("+", lexer.Read().Value);
			Assert.AreEqual(789.0, lexer.Read().Value);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void OnlyExpTest2()
		{
			var lexer = new Lexer("'abc'.'cde'");
			Assert.AreEqual("abc", lexer.Read().Value);
			Assert.AreEqual(".", lexer.Read().Value);
			Assert.AreEqual("cde", lexer.Read().Value);
			Assert.Null(lexer.Read());
		}

		[Test]
		public void ExprTest1()
		{
			Assert.AreEqual(123.0 + 123.0, Expression.Evaluate<double>("123 + 123"));
		}

		[Test]
		public void ExprTest2()
		{
			Assert.AreEqual(123.0 * 123.0, Expression.Evaluate<double>("123 * 123"));
		}

		[Test]
		public void ExprTest3()
		{
			Assert.AreEqual(1.0 + 2.0 * 3.0 + 4.0, Expression.Evaluate<double>("1 + 2 * 3 + 4"));
		}

		[Test]
		public void ExprTest4()
		{
			Assert.AreEqual(1.0 - 2.0 + 3.0, Expression.Evaluate<double>("1 - 2 + 3"));
		}

		[Test]
		public void ExprTest5()
		{
			Assert.AreEqual(1.0 - 2.0 * (3.0 + 4.0) + 1.0, Expression.Evaluate<double>("1 - 2 * (3 + 4) + 1"));
		}

		[Test]
		public void ExprTest6()
		{
			Assert.AreEqual(8.0, Expression.Evaluate<double>("2 ** 3"));
		}

		[Test]
		public void ExprTest7()
		{
			Assert.AreEqual(true, Expression.Evaluate<bool>("true || false"));
			Assert.AreEqual(true, Expression.Evaluate<bool>("false || true"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("false || false"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("true && false"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("false && true"));
			Assert.AreEqual(true, Expression.Evaluate<bool>("true && true"));
		}

		[Test]
		public void ExprTest8()
		{
			Assert.AreEqual((double)(123 | 321), Expression.Evaluate<double>("123|321"));
			Assert.AreEqual((double)(123 & 321), Expression.Evaluate<double>("123&321"));
			Assert.AreEqual((double)(123 ^ 321), Expression.Evaluate<double>("123^321"));
		}

		[Test]
		public void ExprTest9()
		{
			Assert.AreEqual(true, Expression.Evaluate<bool>("0 == 0"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("0 == 1"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("0 != 0"));
			Assert.AreEqual(true, Expression.Evaluate<bool>("0 != 1"));
			Assert.AreEqual(true, Expression.Evaluate<bool>("123<321"));
			Assert.AreEqual(true, Expression.Evaluate<bool>("123<=321"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("123>321"));
			Assert.AreEqual(false, Expression.Evaluate<bool>("123>=321"));
		}

		[Test]
		public void ExprTest10()
		{
			Assert.AreEqual((double)(10 << 3), Expression.Evaluate<double>("10 << 3"));
			Assert.AreEqual((double)(10 >> 1), Expression.Evaluate<double>("10 >> 1"));
		}

		[Test]
		public void ExprTest11()
		{
			Assert.AreEqual((double)(System.Math.Abs(-123)), Expression.Evaluate<double>("abs(-123)"));
			Assert.AreEqual((double)(System.Math.Acos(1)), Expression.Evaluate<double>("acos(1)"));
			Assert.AreEqual((double)(System.Math.Asin(1)), Expression.Evaluate<double>("asin(1)"));
			Assert.AreEqual((double)(System.Math.Atan(1)), Expression.Evaluate<double>("atan(1)"));
			Assert.AreEqual((double)(System.Math.Atan2(1, 1)), Expression.Evaluate<double>("atan2(1, 1)"));
			Assert.AreEqual((double)(System.Math.Ceiling(1.23)), Expression.Evaluate<double>("ceil(1.23)"));
			Assert.AreEqual((double)(System.Math.Pow(123, 1.0 / 3.0)), Expression.Evaluate<double>("cbrt(123)"));
			Assert.AreEqual((double)(System.Math.Cos(1.23)), Expression.Evaluate<double>("cos(1.23)"));
			Assert.AreEqual((double)(System.Math.Cosh(1.23)), Expression.Evaluate<double>("cosh(1.23)"));
			Assert.AreEqual((double)(System.Math.Exp(1.23)), Expression.Evaluate<double>("exp(1.23)"));
			Assert.AreEqual((double)(System.Math.Floor(1.23)), Expression.Evaluate<double>("floor(1.23)"));
			Assert.AreEqual((double)(System.Math.Sqrt(1 * 1 + 2 * 2)), Expression.Evaluate<double>("hypot(1, 2)"));
			Assert.AreEqual((double)(System.Math.Log(123, 3)), Expression.Evaluate<double>("log(123, 3)"));
			Assert.AreEqual((double)(System.Math.Log10(123)), Expression.Evaluate<double>("log10(123)"));
			Assert.AreEqual((double)(System.Math.Log(123, 2)), Expression.Evaluate<double>("log2(123)"));
			Assert.AreEqual((double)(System.Math.Max(10.0, 12.3)), Expression.Evaluate<double>("max(10.0, 12.3)"));
			Assert.AreEqual((double)(System.Math.Min(10.0, 12.3)), Expression.Evaluate<double>("min(10.0, 12.3)"));
			Assert.AreEqual((double)(System.Math.Pow(10.0, 12.3)), Expression.Evaluate<double>("pow(10.0, 12.3)"));
			Assert.AreEqual((double)(System.Math.Round(12.5)), Expression.Evaluate<double>("round(12.5)"));
			Assert.AreEqual((double)(System.Math.Sign(-3)), Expression.Evaluate<double>("sign(-3)"));
			Assert.AreEqual((double)(System.Math.Sin(1)), Expression.Evaluate<double>("sin(1)"));
			Assert.AreEqual((double)(System.Math.Sinh(1)), Expression.Evaluate<double>("sinh(1)"));
			Assert.AreEqual((double)(System.Math.Sqrt(123)), Expression.Evaluate<double>("sqrt(123)"));
			Assert.AreEqual((double)(System.Math.Tan(1)), Expression.Evaluate<double>("tan(1)"), 0.01);
			Assert.AreEqual((double)(System.Math.Tanh(1)), Expression.Evaluate<double>("tanh(1)"));
			Assert.AreEqual((double)(123 * 123), Expression.Evaluate<double>("sq(123)"));
			Assert.AreEqual((double)(10), Expression.Evaluate<double>("constrain(9,10,20)"));
			Assert.AreEqual((double)(10), Expression.Evaluate<double>("constrain(10,10,20)"));
			Assert.AreEqual((double)(20), Expression.Evaluate<double>("constrain(20,10,20)"));
			Assert.AreEqual((double)(20), Expression.Evaluate<double>("constrain(21,10,20)"));
			Assert.AreEqual((double)(System.Math.Sqrt(200)), Expression.Evaluate<double>("mag(10,10)"));
			Assert.AreEqual((double)(System.Math.Sqrt(200)), Expression.Evaluate<double>("dist(1,1,11,11)"));
			Assert.AreEqual(12.0, Expression.Evaluate<double>("lerp(10,20,0.2)"));
			Assert.AreEqual(0.8, Expression.Evaluate<double>("norm(8, 0, 10)"));
			//Assert.AreEqual(120.0, Expression.Evaluate<double>("map(2,0,10,100,200)"));
		}
	}
}
