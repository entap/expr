using System;
using Entap.Expr;
using Xunit;

namespace Entap.Expr.Test
{
	public class ExprTest
	{
		[Fact]
		public void OnlyIdentifierTest1()
		{
			var lexer = new Lexer("abc123");
			var token = lexer.Read();
			Assert.Equal("abc123", token.Value);
			Assert.Equal(TokenType.Identifier, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyIdentifierTest2()
		{
			var lexer = new Lexer("あいうえお");
			var token = lexer.Read();
			Assert.Equal("あいうえお", token.Value);
			Assert.Equal(TokenType.Identifier, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyNumericTest1()
		{
			var lexer = new Lexer("0x1234");
			var token = lexer.Read();
			Assert.Equal((double)0x1234, token.Value);
			Assert.Equal(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyNumericTest2()
		{
			var lexer = new Lexer("01234");
			var token = lexer.Read();
			Assert.Equal(01234.0, token.Value);
			Assert.Equal(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyNumericTest3()
		{
			var lexer = new Lexer("1.234");
			var token = lexer.Read();
			Assert.Equal(1.234, token.Value);
			Assert.Equal(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyNumericTest4()
		{
			var lexer = new Lexer("1.234e-10");
			var token = lexer.Read();
			Assert.Equal(1.234e-10, token.Value);
			Assert.Equal(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyNumericTest5()
		{
			var lexer = new Lexer("0");
			var token = lexer.Read();
			Assert.Equal(0.0, token.Value);
			Assert.Equal(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyNumericTest6()
		{
			var lexer = new Lexer("1.234e10");
			var token = lexer.Read();
			Assert.Equal(1.234e10, token.Value);
			Assert.Equal(TokenType.Number, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyStringTest1()
		{
			var lexer = new Lexer("'abcdefg'");
			var token = lexer.Read();
			Assert.Equal("abcdefg", token.Value);
			Assert.Equal(TokenType.String, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyStringTest2()
		{
			var lexer = new Lexer("'abc\\ndefg\\x12\\u1234'");
			var token = lexer.Read();
			Assert.Equal("abc\ndefg\x12\u1234", token.Value);
			Assert.Equal(TokenType.String, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyPuncTest1()
		{
			var lexer = new Lexer("+");
			var token = lexer.Read();
			Assert.Equal("+", token.Value);
			Assert.Equal(TokenType.Punctuator, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyPuncTest2()
		{
			var lexer = new Lexer("&&");
			var token = lexer.Read();
			Assert.Equal("&&", token.Value);
			Assert.Equal(TokenType.Punctuator, token.Type);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyExpTest1()
		{
			var lexer = new Lexer("123+456*(abc-0.1)+789");
			Assert.Equal(123.0, lexer.Read().Value);
			Assert.Equal("+", lexer.Read().Value);
			Assert.Equal(456.0, lexer.Read().Value);
			Assert.Equal("*", lexer.Read().Value);
			Assert.Equal("(", lexer.Read().Value);
			Assert.Equal("abc", lexer.Read().Value);
			Assert.Equal("-", lexer.Read().Value);
			Assert.Equal(0.1, lexer.Read().Value);
			Assert.Equal(")", lexer.Read().Value);
			Assert.Equal("+", lexer.Read().Value);
			Assert.Equal(789.0, lexer.Read().Value);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void OnlyExpTest2()
		{
			var lexer = new Lexer("'abc'.'cde'");
			Assert.Equal("abc", lexer.Read().Value);
			Assert.Equal(".", lexer.Read().Value);
			Assert.Equal("cde", lexer.Read().Value);
			Assert.Null(lexer.Read());
		}

		[Fact]
		public void ExprTest1()
		{
			Assert.Equal(123.0 + 123.0, Expr.Eval("123 + 123"));
		}

		[Fact]
		public void ExprTest2()
		{
			Assert.Equal(123.0 * 123.0, Expr.Eval("123 * 123"));
		}

		[Fact]
		public void ExprTest3()
		{
			Assert.Equal(1.0 + 2.0 * 3.0 + 4.0, Expr.Eval("1 + 2 * 3 + 4"));
		}

		[Fact]
		public void ExprTest4()
		{
			Assert.Equal(1.0 - 2.0 + 3.0, Expr.Eval("1 - 2 + 3"));
		}

		[Fact]
		public void ExprTest5()
		{
			Assert.Equal(1.0 - 2.0 * (3.0 + 4.0) + 1.0, Expr.Eval("1 - 2 * (3 + 4) + 1"));
		}

		[Fact]
		public void ExprTest6()
		{
			Assert.Equal(8.0, Expr.Eval("2 ** 3"));
		}

		[Fact]
		public void ExprTest7()
		{
			Assert.Equal(true, Expr.Eval("true || false"));
			Assert.Equal(true, Expr.Eval("false || true"));
			Assert.Equal(false, Expr.Eval("false || false"));
			Assert.Equal(false, Expr.Eval("true && false"));
			Assert.Equal(false, Expr.Eval("false && true"));
			Assert.Equal(true, Expr.Eval("true && true"));
		}

		[Fact]
		public void ExprTest8()
		{
			Assert.Equal((double)(123 | 321), Expr.Eval("123|321"));
			Assert.Equal((double)(123 & 321), Expr.Eval("123&321"));
			Assert.Equal((double)(123 ^ 321), Expr.Eval("123^321"));
		}

		[Fact]
		public void ExprTest9()
		{
			Assert.Equal((bool)(0.0 == 0.0), Expr.Eval("0 == 0"));
			Assert.Equal((bool)(0.0 == 1.0), Expr.Eval("0 == 1"));
			Assert.Equal((bool)(0.0 != 0.0), Expr.Eval("0 != 0"));
			Assert.Equal((bool)(0.0 != 1.0), Expr.Eval("0 != 1"));
			Assert.Equal((bool)(123.0 < 321.0), Expr.Eval("123<321"));
			Assert.Equal((bool)(123.0 <= 321.0), Expr.Eval("123<=321"));
			Assert.Equal((bool)(123.0 > 321.0), Expr.Eval("123>321"));
			Assert.Equal((bool)(123.0 >= 321.0), Expr.Eval("123>=321"));
		}

		[Fact]
		public void ExprTest10()
		{
			Assert.Equal((double)(10 << 3), Expr.Eval("10 << 3"));
			Assert.Equal((double)(10 >> 1), Expr.Eval("10 >> 1"));
		}

		[Fact]
		public void ExprTest11()
		{
			Assert.Equal((double)(System.Math.Abs(-123)), Expr.Eval("abs(-123)"));
			Assert.Equal((double)(System.Math.Acos(1)), Expr.Eval("acos(1)"));
			Assert.Equal((double)(System.Math.Asin(1)), Expr.Eval("asin(1)"));
			Assert.Equal((double)(System.Math.Atan(1)), Expr.Eval("atan(1)"));
			Assert.Equal((double)(System.Math.Atan2(1, 1)), Expr.Eval("atan2(1, 1)"));
			Assert.Equal((double)(System.Math.Ceiling(1.23)), Expr.Eval("ceil(1.23)"));
			Assert.Equal((double)(System.Math.Pow(123, 1.0 / 3.0)), Expr.Eval("cbrt(123)"));
			Assert.Equal((double)(System.Math.Cos(1.23)), Expr.Eval("cos(1.23)"));
			Assert.Equal((double)(System.Math.Cosh(1.23)), Expr.Eval("cosh(1.23)"));
			Assert.Equal((double)(System.Math.Exp(1.23)), Expr.Eval("exp(1.23)"));
			Assert.Equal((double)(System.Math.Floor(1.23)), Expr.Eval("floor(1.23)"));
			Assert.Equal((double)(System.Math.Sqrt(1 * 1 + 2 * 2)), Expr.Eval("hypot(1, 2)"));
			Assert.Equal((double)(System.Math.Log(123, 3)), Expr.Eval("log(123, 3)"));
			Assert.Equal((double)(System.Math.Log10(123)), Expr.Eval("log10(123)"));
			Assert.Equal((double)(System.Math.Log(123, 2)), Expr.Eval("log2(123)"));
			Assert.Equal((double)(System.Math.Max(10.0, 12.3)), Expr.Eval("max(10.0, 12.3)"));
			Assert.Equal((double)(System.Math.Min(10.0, 12.3)), Expr.Eval("min(10.0, 12.3)"));
			Assert.Equal((double)(System.Math.Pow(10.0, 12.3)), Expr.Eval("pow(10.0, 12.3)"));
			Assert.Equal((double)(System.Math.Round(12.5)), Expr.Eval("round(12.5)"));
			Assert.Equal((double)(System.Math.Sign(-3)), Expr.Eval("sign(-3)"));
			Assert.Equal((double)(System.Math.Sin(1)), Expr.Eval("sin(1)"));
			Assert.Equal((double)(System.Math.Sinh(1)), Expr.Eval("sinh(1)"));
			Assert.Equal((double)(System.Math.Sqrt(123)), Expr.Eval("sqrt(123)"));
			Assert.Equal((double)(System.Math.Tan(1)), Expr.Eval("tan(1)"));
			Assert.Equal((double)(System.Math.Tanh(1)), Expr.Eval("tanh(1)"));
			Assert.Equal((double)(123*123), Expr.Eval("sq(123)"));
			Assert.Equal((double)(10), Expr.Eval("constrain(9,10,20)"));
			Assert.Equal((double)(10), Expr.Eval("constrain(10,10,20)"));
			Assert.Equal((double)(20), Expr.Eval("constrain(20,10,20)"));
			Assert.Equal((double)(20), Expr.Eval("constrain(21,10,20)"));
			Assert.Equal((double)(System.Math.Sqrt(200)), Expr.Eval("mag(10,10)"));
			Assert.Equal((double)(System.Math.Sqrt(200)), Expr.Eval("dist(1,1,11,11)"));
			Assert.Equal(12.0, Expr.Eval("lerp(10,20,0.2)"));
			Assert.Equal(0.8, Expr.Eval("norm(8, 0, 10)"));
			Assert.Equal(120.0, Expr.Eval("map(2,0,10,100,200)"));
		}
	}
}
