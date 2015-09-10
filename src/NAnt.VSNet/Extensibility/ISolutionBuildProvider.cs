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
// Martin Aliger (martin_aliger@myrealbox.com)

using System.CodeDom.Compiler;
using NAnt.Core.Extensibility;
using NAnt.Core.Util;

using NAnt.VSNet.Tasks;

namespace NAnt.VSNet.Extensibility {
    /// <summary>
    /// 
    /// </summary>
    public interface ISolutionBuildProvider : IPlugin {
        /// <summary>
        /// Determines whether the specified file contents is supported.
        /// </summary>
        /// <param name="fileContents">The file contents.</param>
        /// <returns></returns>
        int IsSupported(string fileContents);
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="solutionContent">Content of the solution.</param>
        /// <param name="solutionTask">The solution task.</param>
        /// <param name="tfc">The TFC.</param>
        /// <param name="gacCache">The gac cache.</param>
        /// <param name="refResolver">The reference resolver.</param>
        /// <returns></returns>
        SolutionBase GetInstance(string solutionContent, SolutionTask solutionTask, TempFileCollection tfc, GacCache gacCache, ReferencesResolver refResolver);
    }
}
