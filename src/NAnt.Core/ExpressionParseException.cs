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
using System.Runtime.Serialization;

namespace NAnt.Core {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExpressionParseException : Exception {

        private int _startPos = -1;
        private int _endPos = -1;

        /// <summary>
        /// Gets the start position.
        /// </summary>
        /// <value>
        /// The start position.
        /// </value>
        public int StartPos {
            get { return _startPos; }
        }

        /// <summary>
        /// Gets the end position.
        /// </summary>
        /// <value>
        /// The end position.
        /// </value>
        public int EndPos {
            get { return _endPos; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        public ExpressionParseException() : base () {}
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ExpressionParseException(string message) : base(message, null) {}
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ExpressionParseException(string message, Exception inner) : base(message, inner) {}
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ExpressionParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
            _startPos = (int)info.GetValue("startPos", typeof(int));
            _endPos = (int)info.GetValue("endPos", typeof(int));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="pos">The position.</param>
        public ExpressionParseException(string message, int pos) : base(message, null) {
            _startPos = pos;
            _endPos = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="startPos">The start position.</param>
        /// <param name="endPos">The end position.</param>
        public ExpressionParseException(string message, int startPos, int endPos) : base(message, null) {
            _startPos = startPos;
            _endPos = endPos;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="startPos">The start position.</param>
        /// <param name="endPos">The end position.</param>
        /// <param name="inner">The inner.</param>
        public ExpressionParseException(string message, int startPos, int endPos, Exception inner) : base(message, inner) {
            _startPos = startPos;
            _endPos = endPos;
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("startPos", _startPos);
            info.AddValue("endPos", _endPos);

            base.GetObjectData(info, context);
        }
    }
}
