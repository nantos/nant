// NAnt - A .NET build tool
// Copyright (C) 2001-2003 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Jaroslaw Kowalski (jkowalski@users.sourceforge.net)

using System;
using System.Globalization;
using System.Text;

using NAnt.Core.Util;

namespace NAnt.Core {

    /// <summary>
    /// Splits an input string into a sequence of tokens used during parsing.
    /// </summary>
    public class ExpressionTokenizer {

        /// <summary>
        /// 
        /// </summary>
        public struct Position {
            private int _charIndex;

            /// <summary>
            /// Initializes a new instance of the <see cref="Position"/> struct.
            /// </summary>
            /// <param name="charIndex">Index of the character.</param>
            public Position(int charIndex) {
                _charIndex = charIndex;
            }

            /// <summary>
            /// Gets the index of the character.
            /// </summary>
            /// <value>
            /// The index of the character.
            /// </value>
            public int CharIndex {
                get { return _charIndex; }
            }
        }

        /// <summary>
        /// Available tokens
        /// </summary>
        public enum TokenType {
            /// <summary>
            /// The bof
            /// </summary>
            BOF,
            /// <summary>
            /// The EOF
            /// </summary>
            EOF,
            /// <summary>
            /// The number
            /// </summary>
            Number,
            /// <summary>
            /// The string
            /// </summary>
            String,
            /// <summary>
            /// The keyword
            /// </summary>
            Keyword,
            /// <summary>
            /// The eq
            /// </summary>
            EQ,
            /// <summary>
            /// The ne
            /// </summary>
            NE,
            /// <summary>
            /// The lt
            /// </summary>
            LT,
            /// <summary>
            /// The gt
            /// </summary>
            GT,
            /// <summary>
            /// The le
            /// </summary>
            LE,
            /// <summary>
            /// The ge
            /// </summary>
            GE,
            /// <summary>
            /// The plus
            /// </summary>
            Plus,
            /// <summary>
            /// The minus
            /// </summary>
            Minus,
            /// <summary>
            /// The mul
            /// </summary>
            Mul,
            /// <summary>
            /// The div
            /// </summary>
            Div,
            /// <summary>
            /// The mod
            /// </summary>
            Mod,
            /// <summary>
            /// The left paren
            /// </summary>
            LeftParen,
            /// <summary>
            /// The right paren
            /// </summary>
            RightParen,
            /// <summary>
            /// The left curly brace
            /// </summary>
            LeftCurlyBrace,
            /// <summary>
            /// The right curly brace
            /// </summary>
            RightCurlyBrace,
            /// <summary>
            /// The not
            /// </summary>
            Not,
            /// <summary>
            /// The punctuation
            /// </summary>
            Punctuation,
            /// <summary>
            /// The whitespace
            /// </summary>
            Whitespace,
            /// <summary>
            /// The dollar
            /// </summary>
            Dollar,
            /// <summary>
            /// The comma
            /// </summary>
            Comma,
            /// <summary>
            /// The dot
            /// </summary>
            Dot,
            /// <summary>
            /// The double colon
            /// </summary>
            DoubleColon,
        }

        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTokenizer"/> class.
        /// </summary>
        public ExpressionTokenizer() {
        }

        #endregion Public Instance Constructors

        #region Static Constructor

        static ExpressionTokenizer() {
            for (int i = 0; i < 128; ++i)
                charIndexToTokenType[i] = TokenType.Punctuation;

            foreach (CharToTokenType cht in charToTokenType)
                charIndexToTokenType[(int) cht.ch] = cht.tokenType;
        }

        #endregion Static Constructor

        #region Public Instance Properties

        /// <summary>
        /// Gets or sets a value indicating whether [ignore whitespace].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore whitespace]; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreWhitespace {
            get { return _ignoreWhiteSpace; }
            set { _ignoreWhiteSpace = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [single character mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [single character mode]; otherwise, <c>false</c>.
        /// </value>
        public bool SingleCharacterMode {
            get { return _singleCharacterMode; }
            set { _singleCharacterMode = value; }
        }

        /// <summary>
        /// Gets the current token.
        /// </summary>
        /// <value>
        /// The current token.
        /// </value>
        public TokenType CurrentToken {
            get { return _tokenType; }
        }

        /// <summary>
        /// Gets the token text.
        /// </summary>
        /// <value>
        /// The token text.
        /// </value>
        public string TokenText {
            get { return _tokenText; }
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        public Position CurrentPosition {
            get { return _tokenStartPosition; }
        }

        #endregion Public Instance Properties

        #region Public Instance Methods

        /// <summary>
        /// Initializes the tokenizer.
        /// </summary>
        /// <param name="s">The s.</param>
        public void InitTokenizer(string s) {
            _text = s;
            _position = 0;
            _tokenType = TokenType.BOF;

            GetNextToken();
        }

        /// <summary>
        /// Gets the next token.
        /// </summary>
        /// <exception cref="ExpressionParseException">
        /// -1;-1
        /// or
        /// </exception>
        public void GetNextToken() {
            if (_tokenType == TokenType.EOF)
                throw new ExpressionParseException(ResourceUtils.GetString("String_CannotReadPastStream"), -1, -1);

            if (IgnoreWhitespace)
                SkipWhitespace();

            _tokenStartPosition = new Position(_position);

            int peek = PeekChar();
            if (peek == -1) {
                _tokenType = TokenType.EOF;
                return;
            }

            char ch = (char) peek;

            if (!SingleCharacterMode) {
                if (!IgnoreWhitespace && Char.IsWhiteSpace(ch)) {
                    StringBuilder sb = new StringBuilder();

                    while ((peek = PeekChar()) != -1) {
                        if (!Char.IsWhiteSpace((char) peek))
                            break;

                        sb.Append((char) peek);
                        ReadChar();
                    }

                    _tokenType = TokenType.Whitespace;
                    _tokenText = sb.ToString();
                    return;
                }

                if (Char.IsDigit(ch)) {
                    _tokenType = TokenType.Number;
                    string s = string.Empty;

                    s += ch;
                    ReadChar();

                    while ((peek = PeekChar()) != -1) {
                        ch = (char) peek;

                        if (Char.IsDigit(ch)) {
                            s += (char) ReadChar();
                        } else {
                            break;
                        }
                    }

                    _tokenText = s;
                    return;
                }

                if (ch == '\'') {
                    _tokenType = TokenType.String;

                    string s = "";
                    ReadChar();
                    while ((peek = ReadChar()) != -1) {
                        ch = (char) peek;

                        if (ch == '\'') {
                            if (PeekChar() == (int)'\'') {
                                ReadChar();
                            } else
                                break;
                        }
                        s += ch;
                    }

                    _tokenText = s;
                    return;
                }

                if (ch == '_' || Char.IsLetter(ch)) {
                    _tokenType = TokenType.Keyword;

                    StringBuilder sb = new StringBuilder();
                    sb.Append((char) ch);
                    ReadChar();

                    while ((peek = PeekChar()) != -1) {
                        char c = (char) peek;
                        if (c == '_' || c == '-' || c == '.' || c == '\\' || Char.IsLetterOrDigit(c)) {
                            ReadChar();
                            sb.Append(c);
                        } else {
                            break;
                        }
                    }

                    _tokenText = sb.ToString();
                    if (_tokenText.EndsWith("-") || _tokenText.EndsWith("."))
                        throw new ExpressionParseException(String.Format(CultureInfo.InvariantCulture, 
                            ResourceUtils.GetString("NA1182"), _tokenText), CurrentPosition.CharIndex);
                    return;
                }

                ReadChar();
                peek = PeekChar();

                if (ch == ':' && peek == (int) ':') {
                    _tokenType = TokenType.DoubleColon;
                    _tokenText = "::";
                    ReadChar();
                    return;
                }

                if (ch == '!' && peek == (int) '=') {
                    _tokenType = TokenType.NE;
                    _tokenText = "!=";
                    ReadChar();
                    return;
                }

                if (ch == '=' && peek == (int) '=') {
                    _tokenType = TokenType.EQ;
                    _tokenText = "==";
                    ReadChar();
                    return;
                }

                if (ch == '<' && peek == (int) '=') {
                    _tokenType = TokenType.LE;
                    _tokenText = "<=";
                    ReadChar();
                    return;
                }

                if (ch == '>' && peek == (int) '=') {
                    _tokenType = TokenType.GE;
                    _tokenText = ">=";
                    ReadChar();
                    return;
                }
            } else {
                ReadChar();
            }
            _tokenText = new String(ch, 1);
            if (ch >= 32 && ch < 128) {
                _tokenType = charIndexToTokenType[ch];
            } else {
                _tokenType = TokenType.Punctuation;
            }
        }

        /// <summary>
        /// Determines whether the specified k is keyword.
        /// </summary>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        public bool IsKeyword(string k) {
            return (_tokenType == TokenType.Keyword) && (_tokenText == k);
        }

        #endregion Public Instance Methods

        #region Private Instance Methods

        private int ReadChar() {
            if (_position < _text.Length)
                return _text[_position++];
            return -1;
        }

        private int PeekChar() {
            if (_position < _text.Length)
                return _text[_position];
            return -1;
        }

        private void SkipWhitespace() {
            int ch;

            while ((ch = PeekChar()) != -1) {
                if (!Char.IsWhiteSpace((char) ch))
                    break;
                ReadChar();
            }
        }

        #endregion Private Instance Methods

        #region Private Instance Fields

        private string _text;
        private int _position;
        private Position _tokenStartPosition;
        private TokenType _tokenType;
        private string _tokenText;
        private bool _ignoreWhiteSpace = true;
        private bool _singleCharacterMode;

        #endregion Private Instance Fields

        #region Private Static Fields

        private static CharToTokenType[] charToTokenType = {
            new CharToTokenType('+', TokenType.Plus),
            new CharToTokenType('-', TokenType.Minus),
            new CharToTokenType('*', TokenType.Mul),
            new CharToTokenType('/', TokenType.Div),
            new CharToTokenType('%', TokenType.Mod),
            new CharToTokenType('<', TokenType.LT),
            new CharToTokenType('>', TokenType.GT),
            new CharToTokenType('(', TokenType.LeftParen),
            new CharToTokenType(')', TokenType.RightParen),
            new CharToTokenType('{', TokenType.LeftCurlyBrace),
            new CharToTokenType('}', TokenType.RightCurlyBrace),
            new CharToTokenType('!', TokenType.Not),
            new CharToTokenType('$', TokenType.Dollar),
            new CharToTokenType(',', TokenType.Comma),
            new CharToTokenType('.', TokenType.Dot),
        };

        private static TokenType[] charIndexToTokenType = new TokenType[128];

        #endregion Private Static Fields

        private struct CharToTokenType {
            public readonly char ch;
            public readonly TokenType tokenType;

            public CharToTokenType(char ch, TokenType tokenType) {
                this.ch = ch;
                this.tokenType = tokenType;
            }
        }
    }
}
