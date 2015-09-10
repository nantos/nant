// NAnt - A .NET build tool
// Copyright (C) 2001-2004 Gerry Shaw
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
// Gert Driesen (drieseng@users.sourceforge.net)

using System.CodeDom.Compiler;
using NAnt.Core.Util;

using NAnt.VSNet.Tasks;

namespace NAnt.VSNet {
    /// <summary>
    /// Supports grouping of individual projects, and treating them as a solution.
    /// </summary>
    public class GenericSolution : SolutionBase {
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericSolution"/> class.
        /// </summary>
        /// <param name="solutionTask">The solution task.</param>
        /// <param name="tfc">The TFC.</param>
        /// <param name="gacCache">The gac cache.</param>
        /// <param name="refResolver">The reference resolver.</param>
        public GenericSolution(SolutionTask solutionTask, TempFileCollection tfc, GacCache gacCache, ReferencesResolver refResolver) : base(solutionTask, tfc, gacCache, refResolver) {
        }

        #endregion Public Instance Constructors
    }
}
