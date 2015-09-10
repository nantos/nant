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
// Tomas Restrepo (tomasr@mvps.org)
// Gert Driesen (drieseng@users.sourceforge.net)

using System;

using NUnit.Framework;

using NAnt.NUnit.Types;

namespace NAnt.NUnit1.Types {
    /// <summary>
    /// Carries data specified through the test element.
    /// </summary>
    [Serializable]
    public class NUnitTestData {
        #region Public Instance Properties

        /// <summary>
        /// Gets or sets the suite.
        /// </summary>
        /// <value>
        /// The suite.
        /// </value>
        public ITest Suite {
            get { return _suite; }
            set { _suite = value; }
        }

        /// <summary>
        /// Gets or sets the out file.
        /// </summary>
        /// <value>
        /// The out file.
        /// </value>
        public string OutFile {
            get { return _outfile; }
            set {_outfile = value;}
        }

        /// <summary>
        /// Gets or sets to dir.
        /// </summary>
        /// <value>
        /// To dir.
        /// </value>
        public string ToDir {
            get { return _todir; }
            set {_todir = value;}
        }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>
        /// The class.
        /// </value>
        public string Class {
            get { return _class; }
            set { _class = value; }
        }

        /// <summary>
        /// Gets or sets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        public string Assembly {
            get { return _assembly; }
            set { _assembly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NUnitTestData"/> is fork.
        /// </summary>
        /// <value>
        ///   <c>true</c> if fork; otherwise, <c>false</c>.
        /// </value>
        public bool Fork {
            get { return _fork; }
            set { _fork = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [halt on error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [halt on error]; otherwise, <c>false</c>.
        /// </value>
        public bool HaltOnError { 
            get { return _haltonerror; }
            set { _haltonerror = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [halt on failure].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [halt on failure]; otherwise, <c>false</c>.
        /// </value>
        public bool HaltOnFailure {
            get { return _haltonfailure; }
            set { _haltonfailure = value; }
        }

        /// <summary>
        /// Gets the formatters.
        /// </summary>
        /// <value>
        /// The formatters.
        /// </value>
        public FormatterDataCollection Formatters {
            get { return _formatters; }
        }

        /// <summary>
        /// Gets or sets the application configuration file.
        /// </summary>
        /// <value>
        /// The application configuration file.
        /// </value>
        public string AppConfigFile {
            get { return _appConfigFile; }
            set { _appConfigFile = value; }
        }

        #endregion Public Instance Properties

        #region Private Instance Fields

        string _todir = null;
        string _outfile = null;
        string _class = null;
        string _assembly = null;
        bool _fork = false;
        bool _haltonerror = false;
        bool _haltonfailure = false;
        ITest _suite = null;
        FormatterDataCollection _formatters = new FormatterDataCollection();
        string _appConfigFile = null;

        #endregion Private Instance Fields
    }
}
