// NAnt - A .NET build tool
// Copyright (C) 2001 Gerry Shaw
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
// Tomas Restrepo (tomasr@mvps.org)

using System;

using NAnt.NUnit1.Types;

namespace NAnt.NUnit1.Tasks {
    /// <summary>
    /// Class for running NUint test remotely.
    /// </summary>
    public class RemoteNUnitTestRunner : MarshalByRefObject {
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteNUnitTestRunner"/> class.
        /// </summary>
        /// <param name="testData">The test data.</param>
        public RemoteNUnitTestRunner(NUnitTestData testData) {
            _runner = new NUnitTestRunner(testData);
        }

        #endregion Public Instance Constructors

        #region Public Instance Properties

        /// <summary>
        /// Gets the result code.
        /// </summary>
        /// <value>
        /// The result code.
        /// </value>
        public RunnerResult ResultCode {
            get { return _runner.ResultCode; }
        }

        /// <summary>
        /// Gets the formatters.
        /// </summary>
        /// <value>
        /// The formatters.
        /// </value>
        public IResultFormatterCollection Formatters {
            get { return _runner.Formatters; }
        }

        #endregion Public Instance Properties

        #region Public Instance Methods

        /// <summary>
        /// Runs the specified log prefix.
        /// </summary>
        /// <param name="logPrefix">The log prefix.</param>
        /// <param name="verbose">if set to <c>true</c> [verbose].</param>
        public void Run(string logPrefix, bool verbose) {
            _runner.Run(logPrefix, verbose);
        }

        #endregion Public Instance Methods

        #region Private Instance Fields

        private NUnitTestRunner _runner;

        #endregion Private Instance Fields
    }
}
