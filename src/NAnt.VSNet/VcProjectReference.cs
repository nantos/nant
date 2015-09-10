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

using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Xml;
using NAnt.Core.Util;

namespace NAnt.VSNet {
    /// <summary>
    /// Implementation of a VC project reference.
    /// </summary>
    public class VcProjectReference : ProjectReferenceBase {
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VcProjectReference"/> class.
        /// </summary>
        /// <param name="xmlDefinition">The XML definition.</param>
        /// <param name="referencesResolver">The references resolver.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="solution">The solution.</param>
        /// <param name="tfc">The TFC.</param>
        /// <param name="gacCache">The gac cache.</param>
        /// <param name="outputDir">The output dir.</param>
        /// <exception cref="System.ArgumentNullException">if <paramref name="xmlDefinition"/>, <paramref name="solution"/>, <paramref name="tfc"/> or <paramref name="gacCache"/> is null.
        /// </exception>
        public VcProjectReference(XmlElement xmlDefinition, ReferencesResolver referencesResolver, ProjectBase parent, SolutionBase solution, TempFileCollection tfc, GacCache gacCache, DirectoryInfo outputDir) : base(referencesResolver, parent) {
            if (xmlDefinition == null) {
                throw new ArgumentNullException("xmlDefinition");
            }
            if (solution == null) {
                throw new ArgumentNullException("solution");
            }
            if (tfc == null) {
                throw new ArgumentNullException("tfc");
            }
            if (gacCache == null) {
                throw new ArgumentNullException("gacCache");
            }

            XmlAttribute privateAttribute = xmlDefinition.Attributes["CopyLocal"];
            if (privateAttribute != null) {
                _isPrivateSpecified = true;
                _isPrivate = bool.Parse(privateAttribute.Value);
            }

            // determine path of project file
            string projectFile = solution.GetProjectFileFromGuid(
                xmlDefinition.GetAttribute("ReferencedProjectIdentifier"));

            // load referenced project
            _project = LoadProject(solution, tfc, gacCache, outputDir, projectFile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VcProjectReference"/> class.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="isPrivateSpecified">if set to <c>true</c> [is private specified].</param>
        /// <param name="isPrivate">if set to <c>true</c> [is private].</param>
        public VcProjectReference(ProjectBase project, ProjectBase parent, bool isPrivateSpecified, bool isPrivate) : base(project.ReferencesResolver, parent) {
            _project = project;
            _isPrivateSpecified = isPrivateSpecified;
            _isPrivate = isPrivate;
        }

        #endregion Public Instance Constructors

        #region Override implementation of ReferenceBase

        /// <summary>
        /// Gets a value indicating whether the reference is managed for the
        /// specified configuration.
        /// </summary>
        /// <param name="config">The build configuration of the reference.</param>
        /// <returns>
        /// <see langword="true" /> if the reference is managed for the
        /// specified configuration; otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsManaged(Configuration config) {
            return Project.IsManaged(config);            
        }

        #endregion Override implementation of ReferenceBase

        #region Override implementation of ProjectReferenceBase

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public override ProjectBase Project {
            get { return _project; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        protected override bool IsPrivate {
            get { return _isPrivate; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is private specified.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private specified; otherwise, <c>false</c>.
        /// </value>
        protected override bool IsPrivateSpecified {
            get { return _isPrivateSpecified; }
        }

        #endregion Override implementation of ProjectReferenceBase

        #region Private Instance Fields

        private readonly ProjectBase _project;
        private readonly bool _isPrivateSpecified;
        private readonly bool _isPrivate;

        #endregion Private Instance Fields
    }
}
